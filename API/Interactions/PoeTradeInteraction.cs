using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

// ReSharper disable InconsistentNaming

namespace PoeAcolyte.API.Interactions
{
    public abstract class PoeTradeInteraction : IPoeTradeInteraction /*PoeLogMessage,*/
    {
        /// <summary>
        ///     Constructor, set our first <see cref="History" /> item as the incoming parameter
        /// </summary>
        /// <param name="entry"></param>
        protected PoeTradeInteraction(IPoeLogEntry entry) //: base(entry)
        {
            Entry = entry;
            History = new List<IPoeLogEntry> {entry};
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

        /// <summary>
        ///     Mediator pattern to update the associated UI
        /// </summary>
        public abstract void Update_UI();
        


        public ContextMenuStrip ContextMenu { get; set; }
        protected ToolStripMenuItem playersToolStripMenuItems = new ("Players");
        public bool BuildContextMenu(ContextMenuStrip menuStrip)
        {
            // Interaction_UI.PerformSafely(() =>
            // {
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
           // });
           AddPlayer(Entry.Player);
            return true;
        }

        protected void AddPlayer(string player)
        {
            var menuItem = new ToolStripMenuItem(player){Name = player};
            menuItem.Click += (sender, args) =>
            {
                SetActivePlayer(player);
                Interaction_UI.PerformSafely(Update_UI);
            };
            playersToolStripMenuItems.DropDownItems.Add(menuItem);
           
        }
        
        protected void RemovePlayer(string player)
        {
            // var menuItems = playersToolStripMenuItems.DropDownItems.Find(player, false);
            // if (menuItems.Any())
            // {
            //     playersToolStripMenuItems.DropDownItems.Remove(menuItems.First());
            //     
            // }
            playersToolStripMenuItems.DropDownItems.RemoveByKey(player);
            if (playersToolStripMenuItems.DropDownItems.Count < 1)
            {
                Complete();
                return;
            }
            Interaction_UI.PerformSafely(Update_UI);
        }

        public void SetActivePlayer(string player)
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



        #region IPoeStatus

        public virtual GameClientCommandTypeEnum LastChatConsoleCommand { get; set; }
        public virtual bool TraderInArea { get; set; }
        public virtual bool PlayerInArea { get; set; }

        #endregion

        #region IPoeTradeInteraction

        public IInteractionContainer InteractionContainer { get; set; }
        public virtual UserControl Interaction_UI { get; set; }

        public IPoeLogEntry Entry { get; set; }

        public virtual void AddInteraction(IPoeLogEntry logEntry)
        {
            // TODO need to add some guard statements and logic checks
            if (logEntry.PoeLogEntryType != PoeLogEntryTypeEnum.Whisper)
            {
                AddPlayer(logEntry.Player);
            }
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
            InteractionContainer.RemoveInteraction(this);
            // TODO disposal pattern needed?
        }

        public bool HasPlayer(string playerName)
        {
            return Entry.Player != null &&
                   string.Equals(Entry.Player, playerName, StringComparison.CurrentCultureIgnoreCase);
        }

        public virtual bool ShowItemOverlay() {
            // By default, the overlay is not shown... this is adjusted in child overrides
            return false;
        }

        #endregion
    }

    public interface IPoeTradeInteraction : IPoeStatus //IPoeLogMessage,
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