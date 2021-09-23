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
            Interactions = new List<IPoeTradeInteraction>();
        }

        protected List<IPoeTradeInteraction> Interactions { get; init; }

        public void HandleNewLogEntry(IPoeLogEntry entry)
        {
            switch (entry.PoeLogEntryType)
            {
                case PoeLogEntryTypeEnum.Whisper:
                    Whisper(entry);
                    break;
                case PoeLogEntryTypeEnum.PricedTrade:
                    AddInteraction(new PoeTradeSingle(entry));
                    break;
                case PoeLogEntryTypeEnum.UnpricedTrade:
                    AddInteraction(new PoeTradeSingle(entry));
                    break;
                case PoeLogEntryTypeEnum.BulkTrade:
                    AddInteraction(new PoeTradeBulk(entry));
                    break;
                case PoeLogEntryTypeEnum.AreaJoined:
                    PlayerJoined(entry, true);
                    break;
                case PoeLogEntryTypeEnum.AreaLeft:
                    PlayerJoined(entry, false);
                    break;
                case PoeLogEntryTypeEnum.YouJoin:
                    YouJoined(entry);
                    break;
                case PoeLogEntryTypeEnum.SystemMessage:
                    // nothing yet
                    break;
                default:
                    Debug.Print("Unexpected value in POEBroker");
                    break;
            }
        }

        public void RemoveInteraction(IPoeTradeInteraction tradeInteraction)
        {
            this.PerformSafely(() => Controls.Remove(tradeInteraction.Interaction_UI));
            Interactions.Remove(tradeInteraction);
            // TODO verify UI elements are being properly disposed
        }

        private void PlayerJoined(IPoeLogEntry entry, bool joined)
        {
            var matches = Interactions.Where(interaction => interaction.HasPlayer(entry.Player));
            foreach (var poeInteraction in matches) poeInteraction.TraderInArea = joined;
        }

        private void YouJoined(IPoeLogEntry entry)
        {
            // TODO needs to handle being in multiple trade-able areas and tracked at a higher level abstraction
            if (!entry.Other.Contains("Hideout")) return;
            foreach (var poeInteraction in Interactions) poeInteraction.PlayerInArea = true;
        }

        private void Whisper(IPoeLogEntry entry)
        {
            foreach (var poeInteraction in Interactions.Where(poeInteraction => poeInteraction.HasPlayer(entry.Player)))
            {
                poeInteraction.AddInteraction(entry);
            }
        }

        private void AddInteraction(IPoeTradeInteraction tradeInteraction)
        {
            // needs to parse if a new tradeInteraction as associated interface to be built or 
            // update previous interface with new information
            var foundInteractions = Interactions
                .Where(existingInteraction => existingInteraction.ShouldAdd(tradeInteraction.Entry))
                .ToList();

            if (foundInteractions.Any())
            {
                foreach (var existingInteraction in foundInteractions) existingInteraction.AddInteraction(tradeInteraction.Entry);
            }
            else
            {
                Interactions.Add(tradeInteraction);
                tradeInteraction.InteractionContainer = this;
                this.PerformSafely(() => Controls.Add(tradeInteraction.Interaction_UI));
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
        /// <param name="tradeInteraction">TradeInteraction to be removed</param>
        public void RemoveInteraction(IPoeTradeInteraction tradeInteraction);
    }
}