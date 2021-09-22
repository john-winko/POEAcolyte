using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;

// ReSharper disable InconsistentNaming

namespace PoeAcolyte.API.Interactions
{
    public abstract class PoeInteraction : PoeLogMessage, IPoeInteraction
    {
        /// <summary>
        /// Constructor, set our first <see cref="History" /> item as the incoming parameter
        /// </summary>
        /// <param name="entry"></param>
        protected PoeInteraction(IPoeLogEntry entry) : base(entry)
        {
            History = new List<IPoeInteraction> {this};
        }

        /// <summary>
        /// Used for tooltip information. Aggregates <see cref="History"/>.
        /// </summary>
        public string MessageHistory => History.Aggregate("", (current, poeInteraction) =>
            current + $"{poeInteraction.Entry.Player} - {poeInteraction.Entry.Other} \r\n");

        /// <summary>
        ///     Mediator pattern to update the associated UI
        /// </summary>
        public abstract void Update_UI();

        /// <summary>
        /// List of interactions this interaction has ownership of
        /// </summary>
        public List<IPoeInteraction> History { get; }

        #region IPoeInteraction
        public GameClientCommandType LastGameClientCommand { get; set; }
        public IInteractionContainer InteractionContainer { get; set; }
        public virtual UserControl Interaction_UI { get; set; }

        public virtual bool TraderInArea { get; set; }
        public virtual bool PlayerInArea { get; set; }

        public virtual void AddInteraction(IPoeInteraction interaction)
        {
            History.Add(interaction);
            Interaction_UI.PerformSafely(Update_UI);
        }

        public virtual bool ShouldAdd(IPoeInteraction interaction)
        {
            // whisper from same player
            return interaction.Entry.Player == Entry.Player;
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

    public interface IPoeInteraction : IPoeLogMessage
    {
        /// <summary>
        ///     Last game command sent (invited/kicked etc) as state information
        /// </summary>
        public GameClientCommandType LastGameClientCommand { get; set; }
        public IInteractionContainer InteractionContainer { get; set; }
        /// <summary>
        /// UserControl associated with this interaction
        /// </summary>
        public UserControl Interaction_UI { get; set; }
        /// <summary>
        /// Is trading player in your area
        /// </summary>
        public bool TraderInArea { get; set; }
        /// <summary>
        /// Are "you" in the area (hideout)
        /// </summary>
        public bool PlayerInArea { get; set; }

        /// <summary>
        ///     Add <see cref="IPoeInteraction" /> (most often a whisper)
        /// </summary>
        /// <param name="interaction">Interaction to be added</param>
        public void AddInteraction(IPoeInteraction interaction);

        /// <summary>
        ///     Used for LINQ queries if this interaction should own another (most often whispers)
        /// </summary>
        /// <param name="interaction"></param>
        /// <returns>true if this should own the interaction, false otherwise</returns>
        public bool ShouldAdd(IPoeInteraction interaction);

        /// <summary>
        ///     Interaction has run its course and needs to be disposed
        /// </summary>
        public void Complete();

        /// <summary>
        ///     Required for LINQ comparisons - string operation was not working correctly
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        public bool HasPlayer(string playerName);
    }
}