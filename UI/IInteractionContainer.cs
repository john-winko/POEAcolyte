using PoeAcolyte.API;
using System.Collections.Generic;

namespace PoeAcolyte.UI
{
    public interface IInteractionContainer
    {
        //protected IEnumerable<IPoeInteraction> Interactions { get; init; }
        public void AddInteraction(IPoeInteraction interaction);
        public void RemoveInteraction(IPoeInteraction interaction);
    }
}
