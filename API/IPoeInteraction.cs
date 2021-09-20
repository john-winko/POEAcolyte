using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;

namespace PoeAcolyte.API
{
    public interface IPoeInteraction
    {
        public IPoeLogEntry Entry { get; set; }
        public List<IPoeInteraction> History { get; }
        public IInteractionContainer InteractionContainer { get; set; }
        public void AddInteraction(IPoeInteraction interaction);
        public UserControl InteractionUI { get; set; }

        /// <summary>
        /// Whether or not this interaction should be added to the history of this interaction (i.e. whispers, duplicates)
        /// </summary>
        /// <param name="interaction">Interaction to check</param>
        /// <returns></returns>
        public bool ShouldAdd(IPoeInteraction interaction);
    }
}