using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;

// ReSharper disable InconsistentNaming

namespace PoeAcolyte.API.Interactions
{
    public abstract class PoeTradeInteraction : IPoeTradeInteraction/*PoeLogMessage,*/ 
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


        #region IPoeStatus

        public virtual GameClientCommandTypeEnum LastChatConsoleCommand { get; set; }
        public virtual bool TraderInArea { get; set; }
        public virtual bool PlayerInArea { get; set; }

        #endregion

        #region IPoeTradeInteraction

        public IInteractionContainer InteractionContainer { get; set; }
        public virtual UserControl Interaction_UI { get; set; }

        public IPoeLogEntry Entry { get; init; }
        public virtual void AddInteraction(IPoeLogEntry logEntry)
        {
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

        #endregion
    }

    public interface IPoeTradeInteraction :  IPoeStatus //IPoeLogMessage,
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
        public IPoeLogEntry Entry { get; }
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