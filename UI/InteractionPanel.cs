using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using PoeAcolyte.API;
using PoeAcolyte.API.Interactions;
using PoeAcolyte.API.Parsers;

namespace PoeAcolyte.UI
{
    /// <summary>
    ///     Collection of UI elements to represent trade interactions with the game client
    /// </summary>
    public partial class InteractionPanel : FlowLayoutPanel, IInteractionContainer
    {
        /// <summary>
        ///     Constructor initializes a list of interactions
        /// </summary>
        public InteractionPanel()
        {
            InitializeComponent();
            Interactions = new List<IPoeInteraction>();
        }

        protected List<IPoeInteraction> Interactions { get; init; }

        public void HandleNewLogEntry(IPoeLogEntry entry)
        {
            switch (entry.PoeLogEntryType)
            {
                case IPoeLogEntry.PoeLogEntryTypeEnum.Whisper:
                    AddInteraction(new PoeWhisper(entry));
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.PricedTrade:
                    AddInteraction(new PoeSingleTrade(entry));
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.UnpricedTrade:
                    AddInteraction(new PoeSingleTrade(entry));
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.BulkTrade:
                    AddInteraction(new PoeBulkTrade(entry));
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.AreaJoined:
                    PlayerJoined(entry, true);
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.AreaLeft:
                    PlayerJoined(entry, false);
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.YouJoin:
                    YouJoined(entry);
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.SystemMessage:
                    // nothing yet
                    break;
                default:
                    Debug.Print("Unexpected value in POEBroker");
                    break;
            }
        }

        public void RemoveInteraction(IPoeInteraction interaction)
        {
            this.PerformSafely(() => Controls.Remove(interaction.Interaction_UI));
            Interactions.Remove(interaction);
            // TODO verify UI elements are being properly disposed
        }

        private void PlayerJoined(IPoeLogEntry entry, bool joined)
        {
            var matches = Interactions.Where(interaction => interaction.HasPlayer(entry.Player));
            foreach (var poeInteraction in matches) poeInteraction.TraderInArea = joined;
        }

        private void YouJoined(IPoeLogEntry entry)
        {
            // maybe this should be held more abstract or event driven?
            // TODO needs to handle being in multiple trade-able areas
            if (!entry.Other.Contains("Hideout")) return;
            foreach (var poeInteraction in Interactions) poeInteraction.PlayerInArea = true;
        }

        private void AddInteraction(IPoeInteraction interaction)
        {
            // needs to parse if a new interaction as associated interface to be built or 
            // update previous interface with new information
            var foundInteractions = Interactions
                .Where(existingInteraction => existingInteraction.ShouldAdd(interaction))
                .ToList();

            if (foundInteractions.Any())
            {
                foreach (var existingInteraction in foundInteractions) existingInteraction.AddInteraction(interaction);
            }
            else
            {
                Interactions.Add(interaction);
                interaction.InteractionContainer = this;
                this.PerformSafely(() => Controls.Add(interaction.Interaction_UI));
            }
        }
    }

    public interface IInteractionContainer
    {
        /// <summary>
        ///     Creates a new UI element or pushes the <see cref="IPoeLogEntry" /> information to an existing UI element
        /// </summary>
        /// <param name="entry">Log entry to be managed</param>
        public void HandleNewLogEntry(IPoeLogEntry entry);

        /// <summary>
        ///     Removes element from the <see cref="IInteractionContainer" />
        /// </summary>
        /// <param name="interaction">Interaction to be removed</param>
        public void RemoveInteraction(IPoeInteraction interaction);
    }
}