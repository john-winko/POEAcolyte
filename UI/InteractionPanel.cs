using PoeAcolyte.API;
using PoeAcolyte.API.Interactions;
using PoeAcolyte.API.Parsers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

        public void NewTradeRequest(IPoeLogEntry entry)
        {
            // add to existing control if item request already exists (only works for single trades)
            foreach (var tradeInteraction in Interactions.Where(p => p.Entry.IsSameItem(entry)))
            {
                tradeInteraction.AddInteraction(entry);
                return;
            }

            var interaction = MakeTradeInteraction(entry);

            interaction.InteractionContainer = this;
            Interactions.Add(interaction);
            this.PerformSafely(() => Controls.Add(interaction.Interaction_UI));
        }

        public void NewWhisper(IPoeLogEntry entry)
        {
            
            // TODO try to handle linq query without using a method call
            var matches = Interactions.Where(interaction => interaction.HasPlayer(entry.Player)).ToList();

            // TODO collection modified error... have to null check or adding to tolist() enough?
            if (!matches.Any()) return;
            foreach (var interaction in matches)
                interaction.AddInteraction(entry);
        }

        public void TraderInArea(IPoeLogEntry entry, bool inArea)
        {
            foreach (var interaction in Interactions.Where(interaction => interaction.HasPlayer(entry.Player)))
                interaction.TraderInArea = inArea;
        }

        public void YouJoined(IPoeLogEntry entry)
        {
            // TODO needs to handle being in multiple trade-able areas and tracked at a higher level abstraction
            if (!entry.Other.Contains("Hideout")) return;
            foreach (var poeInteraction in Interactions) poeInteraction.PlayerInArea = true;
        }

        public void RemoveInteraction(IPoeTradeInteraction tradeInteraction)
        {
            this.PerformSafely(() =>
            {
                Controls.Remove(tradeInteraction.Interaction_UI);
                Interactions.Remove(tradeInteraction);
                
            });
            
            // TODO verify UI elements are being properly disposed
        }

        /// <summary>
        ///     Makes a new <see cref="IPoeTradeInteraction" /> if entry is valid, null if not
        /// </summary>
        /// <param name="entry"><see cref="IPoeLogEntry" /> to use</param>
        /// <returns><see cref="IPoeTradeInteraction" /> if valid, null if not</returns>
        private static IPoeTradeInteraction MakeTradeInteraction(IPoeLogEntry entry)
        {
            return entry.PoeLogEntryType switch
            {
                PoeLogEntryTypeEnum.BulkTrade => new PoeTradeBulk(entry),
                PoeLogEntryTypeEnum.PricedTrade => new PoeTradeSingle(entry),
                PoeLogEntryTypeEnum.UnpricedTrade => new PoeTradeSingle(entry),
                _ => null
            };
        }
    }

    public interface IInteractionContainer
    {
        /// <summary>
        ///     Removes element from the <see cref="IInteractionContainer" />
        /// </summary>
        /// <param name="tradeInteraction">TradeInteraction to be removed</param>
        public void RemoveInteraction(IPoeTradeInteraction tradeInteraction);

        /// <summary>
        ///     New trade request
        /// </summary>
        /// <param name="entry">entry to add</param>
        public void NewTradeRequest(IPoeLogEntry entry);

        /// <summary>
        ///     Searches through all open trade requests for a matching player then pushes the whisper to that interaction
        /// </summary>
        /// <param name="entry">LogEntry containing whisper (Custom)</param>
        public void NewWhisper(IPoeLogEntry entry);

        /// <summary>
        ///     Pushes whether a player has joined the area to all trade requests containing that player
        /// </summary>
        /// <param name="entry">entry specifying the player name</param>
        /// <param name="inArea">bool if they are in the area</param>
        public void TraderInArea(IPoeLogEntry entry, bool inArea);

        /// <summary>
        ///     The user has joined an area
        /// </summary>
        /// <param name="entry">entry containing the area which they joined</param>
        public void YouJoined(IPoeLogEntry entry);
    }
}