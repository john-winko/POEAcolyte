using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;

// ReSharper disable InconsistentNaming

namespace PoeAcolyte.API.Interactions
{
    public abstract class PoeTradeInteraction : IPoeTradeInteraction
    {
        protected ToolStripMenuItem playersToolStripMenuItems = new("Players");

        /// <summary>
        ///     Constructor, set our first <see cref="History" /> item as the incoming parameter
        /// </summary>
        /// <param name="entry"></param>
        protected PoeTradeInteraction(IPoeLogEntry entry) //: base(entry)
        {
            Entry = entry;
            History = new List<IPoeLogEntry> {entry};
            AddPlayer(entry.Player);
            AddWhisper(entry);
        }

        /// <summary>
        ///     Used for tooltip information. Aggregates <see cref="History" />.
        /// </summary>
        public string MessageHistory => History.Aggregate("", (current, logEntry) =>
            current + $"{logEntry.Player} - {logEntry.Other} \r\n");

        /// <summary>
        ///     List of interactions this tradeInteraction has ownership of
        /// </summary>
        public List<IPoeLogEntry> History { get; }

        public bool BuildContextMenu(ContextMenuStrip menuStrip)
        {
            menuStrip.Items.Add(playersToolStripMenuItems);
            menuStrip.Items.Add(new GameClientCommand(GameClientCommandTypeEnum.Wait, this));
            menuStrip.Items.Add(new GameClientCommand(GameClientCommandTypeEnum.Invite, this));
            menuStrip.Items.Add(new GameClientCommand(GameClientCommandTypeEnum.Trade, this));
            menuStrip.Items.Add(new GameClientCommand(GameClientCommandTypeEnum.TYGL, this));
            menuStrip.Items.Add(new GameClientCommand(GameClientCommandTypeEnum.NoStock, this));
            menuStrip.Items.Add(new GameClientCommand(GameClientCommandTypeEnum.Decline, this));
            menuStrip.Items.Add(new GameClientCommand(GameClientCommandTypeEnum.Kick, this));
            menuStrip.Items.Add(new GameClientCommand(GameClientCommandTypeEnum.Whois, this));
            menuStrip.Items.Add(new GameClientCommand(GameClientCommandTypeEnum.Hideout, this));

            //AddPlayer(Entry.Player);
            return true;
        }

        /// <summary>
        ///     Mediator pattern to update the associated UI
        /// </summary>
        public abstract void Update_UI();

        /// <summary>
        ///     Add player name to context menu
        /// </summary>
        /// <param name="player"></param>
        protected void AddPlayer(string player)
        {
            var menuItem = new ToolStripMenuItem(player) {Name = player};
            menuItem.Click += (sender, args) =>
            {
                SetActivePlayer(player);
                Interaction_UI.PerformSafely(Update_UI);
            };
            playersToolStripMenuItems.DropDownItems.Add(menuItem);
        }

        /// <summary>
        ///     Change the active entry
        /// </summary>
        /// <param name="player">Player name of the trade request</param>
        protected void SetActivePlayer(string player)
        {
            var result = History.Where(p => p.PoeLogEntryType == Entry.PoeLogEntryType && p.Player == player).ToArray();
            if (result.Any())
            {
                Entry = result.First();
                Interaction_UI.PerformSafely(Update_UI);
            }
            else
            {
                Debug.Print("Could not find");
            }
        }

        /// <summary>
        ///     Try to set a new active entry
        /// </summary>
        /// <returns>true if another trade request is available, false if not</returns>
        protected bool SetActivePlayer()
        {
            var result = History.Where(p => p.PoeLogEntryType == Entry.PoeLogEntryType).ToArray();
            if (!result.Any()) return false;

            Entry = result.First();
            Interaction_UI.PerformSafely(Update_UI);
            return true;
        }

        /// <summary>
        ///     Add a whisper (Custom from LogEntry) to the player showing it in the context menu
        /// </summary>
        /// <param name="logEntry">logEntry.Custom</param>
        protected void AddWhisper(IPoeLogEntry logEntry)
        {
            if (logEntry.Other == "") return;

            var found = playersToolStripMenuItems.DropDownItems.Find(logEntry.Player, false);
            if (found.Any())
            {
                var test = (ToolStripMenuItem) found.First();
                test.DropDownItems.Add(logEntry.Other);
            }
            else
            {
                Debug.Print("Could not find whisper");
            }
        }

        #region IPoeStatus

        public virtual GameClientCommandTypeEnum LastChatConsoleCommand { get; set; }
        public virtual bool TraderInArea { get; set; }
        public virtual bool PlayerInArea { get; set; }

        #endregion

        #region IPoeTradeInteraction

        public IInteractionContainer InteractionContainer { get; set; }
        public virtual UserControl Interaction_UI { get; set; }

        public IPoeLogEntry Entry { get; set; }

        public void AddInteraction(IPoeLogEntry logEntry)
        {
            // TODO need to add some guard statements and logic checks
            if (logEntry.PoeLogEntryType != PoeLogEntryTypeEnum.Whisper)
            {
                var matches = History.Where(p => p.PoeLogEntryType == Entry.PoeLogEntryType).ToArray();
                if (matches.Any(match => match.IsDuplicate(logEntry))) return;

                Interaction_UI.PerformSafely(() => AddPlayer(logEntry.Player));
            }

            Interaction_UI.PerformSafely(() => AddWhisper(logEntry));


            History.Add(logEntry);
            Interaction_UI.PerformSafely(Update_UI);
        }

        public virtual bool ShouldAdd(IPoeLogEntry logEntry)
        {
            // whisper from same player
            return logEntry.Player == Entry.Player;
        }

        public virtual void Complete()
        {
            // Remove active entry
            Interaction_UI.PerformSafely(() => playersToolStripMenuItems.DropDownItems.RemoveByKey(Entry.Player));
            History.Remove(Entry);

            // Try to set a new one
            if (SetActivePlayer()) return;

            // TODO disposal pattern needed?
            // Dispose if nothing left
            playersToolStripMenuItems = null;
            
            InteractionContainer.RemoveInteraction(this);
            Interaction_UI.Dispose();
            //GC.Collect();
        }

        public bool HasPlayer(string playerName)
        {
            var test = History.Where(p => p.Player == playerName);
            return test.Any();
        }

        public virtual bool ShowItemOverlay()
        {
            // By default, the overlay is not shown... this is adjusted in child overrides
            return false;
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            playersToolStripMenuItems?.Dispose();
            Interaction_UI?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface IPoeTradeInteraction : IPoeStatus, IDisposable //IPoeLogMessage,
    {
        /// <summary>
        ///     Container that holds this object
        /// </summary>
        public IInteractionContainer InteractionContainer { get; set; }

        /// <summary>
        ///     UserControl associated with this tradeInteraction
        /// </summary>
        public UserControl Interaction_UI { get; set; }

        /// <summary>
        ///     Log entry that triggered this TradeInteraction
        /// </summary>
        public IPoeLogEntry Entry { get; set; }

        /// <summary>
        ///     Add <see cref="IPoeTradeInteraction" /> (most often a whisper)
        /// </summary>
        /// <param name="interaction">TradeInteraction to be added</param>
        public void AddInteraction(IPoeLogEntry interaction);

        /// <summary>
        ///     Used for LINQ queries if this tradeInteraction should own another (most often whispers)
        /// </summary>
        /// <param name="interaction"></param>
        /// <returns>true if this should own the tradeInteraction, false otherwise</returns>
        public bool ShouldAdd(IPoeLogEntry interaction);

        /// <summary>
        ///     TradeInteraction has run its course and needs to be disposed
        /// </summary>
        public void Complete();

        /// <summary>
        ///     Required for LINQ comparisons - string operation was not working correctly
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        public bool HasPlayer(string playerName);

        /// <summary>
        ///     Toggles showing semi-transparent overlay of where item is located
        /// </summary>
        /// <returns></returns>
        public bool ShowItemOverlay();

        /// <summary>
        ///     Builds a context menu of actions to be performed in game (invite/trade/kick etc)
        /// </summary>
        /// <param name="menuStrip">The menustrip to build</param>
        /// <returns>true if successful, false if not</returns>
        public bool BuildContextMenu(ContextMenuStrip menuStrip);
    }

    public interface IPoeStatus
    {
        /// <summary>
        ///     Last game command sent (invited/kicked etc) as state information
        /// </summary>
        public GameClientCommandTypeEnum LastChatConsoleCommand { get; set; }

        /// <summary>
        ///     Is trading player in your area
        /// </summary>
        public bool TraderInArea { get; set; }

        /// <summary>
        ///     Are "you" in the area (hideout)
        /// </summary>
        public bool PlayerInArea { get; set; }
    }
}