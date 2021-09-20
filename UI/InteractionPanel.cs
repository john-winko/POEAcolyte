using PoeAcolyte.API;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PoeAcolyte.UI
{
    public partial class InteractionPanel : FlowLayoutPanel, IInteractionContainer
    {
        public InteractionPanel()
        {
            InitializeComponent();
            Interactions = new List<IPoeInteraction>();
        }

       public void AddInteraction(IPoeInteraction interaction)
        {
            // needs to parse if a new interaction as associated interface to be built or 
            // update previous interface with new information
            var foundInteractions = Interactions
                    .Where(existingInteraction => existingInteraction.ShouldAdd(interaction))
                    .ToList();

            if (!foundInteractions.Any())
            {
                Interactions.Add(interaction);
                
                this.Controls.Add(interaction.InteractionUI);
                return;
            }

            foreach (var existingInteraction in foundInteractions)
            {
                existingInteraction.AddInteraction(interaction);
            }

        }

        public void RemoveInteraction(IPoeInteraction interaction)
        {
            Interactions.Remove(interaction);
        }

        protected List<IPoeInteraction> Interactions { get; init; }
    }
}