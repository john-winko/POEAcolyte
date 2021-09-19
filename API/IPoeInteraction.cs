using System.Collections;
using System.Collections.Generic;

namespace PoeAcolyte.API
{
    public interface IPoeInteraction
    {
        public string Player { get; }
        public List<IPoeInteraction> History { get; }
        public void AddInteraction(IPoeInteraction interaction);
    }
}