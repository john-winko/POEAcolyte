using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PoeAcolyte.API
{
    public class PoeInteraction : IPoeInteraction
    {
        public IPoeLogEntry Entry { get; set; }
        public virtual UserControl InteractionUI { get; set; }
        public List<IPoeInteraction> History { get; }
        public IInteractionContainer InteractionContainer { get; set ; }

        public void AddInteraction(IPoeInteraction interaction)
        {
            History.Add(interaction);
        }

        public virtual bool ShouldAdd(IPoeInteraction interaction)
        {
            throw new System.NotImplementedException();
        }

        public PoeInteraction(IPoeLogEntry entry)
        {
            Entry = entry;
            History = new List<IPoeInteraction> {this};
        }
    }
}