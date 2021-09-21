using System;
using PoeAcolyte.API;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using PoeAcolyte.API.Interactions;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI.Interactions;

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
                interaction.InteractionContainer = this;

                this.PerformSafely(()=> Controls.Add(interaction.InteractionUI));
                // if (InvokeRequired)
                // {
                //     Invoke(new Action(() => Controls.Add(interaction.InteractionUI)));
                // }
                // else
                // {
                //     Controls.Add(interaction.InteractionUI);
                // }
                
                return;
            }

            foreach (var existingInteraction in foundInteractions)
            {
                existingInteraction.AddInteraction(interaction);
            }

        }
       public void AddEvent(IPoeEvent poeEvent)
       {
          
           var matches =  Interactions.Where(interaction => interaction.HasPlayer(poeEvent.Entry.Player));

           switch (poeEvent.Entry.PoeLogEntryType)
           {
               case IPoeLogEntry.PoeLogEntryTypeEnum.AreaJoined:
               {
                   foreach (var poeInteraction in matches)
                   {
                       poeInteraction.TraderInArea = true;
                   }

                   break;
               }
               case IPoeLogEntry.PoeLogEntryTypeEnum.AreaLeft:
               {
                   foreach (var poeInteraction in matches)
                   {
                       poeInteraction.TraderInArea = false;
                   }

                   break;
               }
               case IPoeLogEntry.PoeLogEntryTypeEnum.YouJoin:
               {
                   // maybe this should be held more abstract or event driven?
                   if (poeEvent.Entry.Other.Contains("Hideout"))
                   {
                       foreach (var poeInteraction in Interactions)
                       {
                           poeInteraction.PlayerInArea = true;
                       }
                   }

                   break;
               }
               default:
                   break;
           }
       }

        public void RemoveInteraction(IPoeInteraction interaction)
        {
            this.PerformSafely(() => Controls.Remove(interaction.InteractionUI));
            Interactions.Remove(interaction);
        }


        protected List<IPoeInteraction> Interactions { get; init; }
    }

    public interface IInteractionContainer
    {
        //protected IEnumerable<IPoeInteraction> Interactions { get; init; }
        public void AddInteraction(IPoeInteraction interaction);
        public void RemoveInteraction(IPoeInteraction interaction);
        public void AddEvent(IPoeEvent poeEvent);
    }
}