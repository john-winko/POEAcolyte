using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
            LoadSettings();
        }

        protected List<IPoeTradeInteraction> Interactions { get; init; }

        public void HandleNewLogEntry(IPoeLogEntry entry)
        {
            // needs to parse if a new tradeInteraction as associated interface to be built or 
            // update previous interface with new information
            var foundInteractions = Interactions
                .Where(existingInteraction => existingInteraction.ShouldAdd(entry))
                .ToList();

            if (foundInteractions.Any())
            {
                foreach (var existingInteraction in foundInteractions) existingInteraction.AddInteraction(entry);
            }
            else
            {
                var interaction = MakeTradeInteraction(entry);
                if (interaction is null)
                {
                    NonTradeInteraction(entry);
                    return;
                }

                Interactions.Add(interaction);
                interaction.InteractionContainer = this;
                this.PerformSafely(() => Controls.Add(interaction.Interaction_UI));
            }
        }

        public void RemoveInteraction(IPoeTradeInteraction tradeInteraction)
        {
            this.PerformSafely(() => Controls.Remove(tradeInteraction.Interaction_UI));
            Interactions.Remove(tradeInteraction);
            // TODO verify UI elements are being properly disposed
        }

        /// <summary>
        ///     Non-whisper log entries that gets pushed to applicable existing <see cref="IPoeTradeInteraction" />
        /// </summary>
        /// <param name="entry"><see cref="IPoeLogEntry" /> entry</param>
        private void NonTradeInteraction(IPoeLogEntry entry)
        {
            var matches = Interactions.Where(interaction => interaction.HasPlayer(entry.Player));
            switch (entry.PoeLogEntryType)
            {
                case PoeLogEntryTypeEnum.Whisper:
                    foreach (var poeInteraction in matches) poeInteraction.AddInteraction(entry);
                    break;
                case PoeLogEntryTypeEnum.AreaJoined:
                    foreach (var poeInteraction in matches) poeInteraction.TraderInArea = true;
                    break;
                case PoeLogEntryTypeEnum.AreaLeft:
                    foreach (var poeInteraction in matches) poeInteraction.TraderInArea = false;
                    break;
                case PoeLogEntryTypeEnum.YouJoin:
                    // TODO needs to handle being in multiple trade-able areas and tracked at a higher level abstraction
                    if (!entry.Other.Contains("Hideout")) return;
                    foreach (var poeInteraction in Interactions) poeInteraction.PlayerInArea = true;
                    break;
                default:
                    Debug.Print("Unexpected value in POEBroker");
                    break;
            }
        }

        /// <summary>
        ///     Makes a new <see cref="IPoeTradeInteraction" /> if entry is valid, null if not
        /// </summary>
        /// <param name="entry"><see cref="IPoeLogEntry" /> to use</param>
        /// <returns><see cref="IPoeTradeInteraction" /> if valid, null if not</returns>
        private IPoeTradeInteraction MakeTradeInteraction(IPoeLogEntry entry)
        {
            return entry.PoeLogEntryType switch
            {
                PoeLogEntryTypeEnum.BulkTrade => new PoeTradeBulk(entry),
                PoeLogEntryTypeEnum.PricedTrade => new PoeTradeSingle(entry),
                PoeLogEntryTypeEnum.UnpricedTrade => new PoeTradeSingle(entry),
                _ => null
            };
        }

        private readonly FrameControl _frameControl = new()
        {
            Description = @"Trade UI Panel",
            Location = new Point(GameClient.Default.TradeUILeft, GameClient.Default.TradeUITop),
            Size = GameClient.Default.TradeUISize
        };
        
        public void EditSettings(ControlCollection owner)
        {
        
            _frameControl.Resize += (o, args) =>
            {
                if (o?.GetType() != typeof(FrameControl)) return;
                var frame = (FrameControl)o;
                GameClient.Default.TradeUITop = frame.Top;
                GameClient.Default.TradeUILeft = frame.Left;
                GameClient.Default.TradeUISize = frame.Size;
            };
            owner.Add(_frameControl);
            _frameControl.BringToFront();
        }
        
        public void SaveSettings(ControlCollection owner)
        {
            GameClient.Default.Save();
            LoadSettings();
            owner.Remove(_frameControl);
        }
        
        public void LoadSettings()
        {
            Location = new Point(GameClient.Default.TradeUILeft, GameClient.Default.TradeUITop);
            Size = GameClient.Default.TradeUISize;
            
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