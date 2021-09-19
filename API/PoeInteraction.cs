using System.Collections.Generic;
using PoeAcolyte.API.Parsers;

namespace PoeAcolyte.API
{
    public class PoeInteraction : IPoeInteraction
    {
        protected readonly IPoeLogEntry Entry;

        public string Player => Entry.Player;
        public List<IPoeInteraction> History { get; }
        
        public void AddInteraction(IPoeInteraction interaction)
        {
            History.Add(interaction);
        }

        public PoeInteraction(IPoeLogEntry entry)
        {
            Entry = entry;
            History = new List<IPoeInteraction> {this};
        }
    }
}