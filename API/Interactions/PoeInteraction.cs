using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;
using PoeAcolyte.UI.Interactions;

namespace PoeAcolyte.API.Interactions
{
    public class PoeInteraction : PoeEvent, IPoeInteraction
    {
        public virtual UserControl InteractionUI { get; set; }
        public List<IPoeInteraction> History { get; }
        public IInteractionContainer InteractionContainer { get; set ; }
        public virtual bool TraderInArea { get; set; }
        public virtual bool PlayerInArea { get; set; }
        public string MessageHistory => History.Aggregate("", (current, poeInteraction) =>
            current + $"{poeInteraction.Entry.Player} - {poeInteraction.Entry.Other} \r\n");

        public virtual void AddInteraction(IPoeInteraction interaction)
        {
            History.Add(interaction);
        }

        public virtual bool ShouldAdd(IPoeInteraction interaction)
        {
            //throw new System.NotImplementedException();
            Debug.Print("PoeInteraction should add error");
            return false;
        }

        public virtual void Complete()
        {
            InteractionContainer.RemoveInteraction(this);
            // TODO disposal pattern needed?
        }

        public bool HasPlayer(string playerName)
        {
            return Entry.Player != null && string.Equals(Entry.Player, playerName, StringComparison.CurrentCultureIgnoreCase);
        }

        public PoeInteraction(IPoeLogEntry entry) : base(entry)
        {
            History = new List<IPoeInteraction> {this};
        }
    }

    public interface IPoeInteraction : IPoeEvent
    {
        public List<IPoeInteraction> History { get; }
        public IInteractionContainer InteractionContainer { get; set; }
        public void AddInteraction(IPoeInteraction interaction);
        public UserControl InteractionUI { get; set; }
        public bool TraderInArea { get; set; }
        public bool PlayerInArea { get; set; }

        /// <summary>
        /// Whether or not this interaction should be added to the history of this interaction (i.e. whispers, duplicates)
        /// </summary>
        /// <param name="interaction">Interaction to check</param>
        /// <returns></returns>
        public bool ShouldAdd(IPoeInteraction interaction);

        public void Complete();
        public bool HasPlayer(string playerName);
    }
}