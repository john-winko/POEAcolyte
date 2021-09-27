using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PoeAcolyte.API;
using PoeAcolyte.API.Interactions;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI.Components;

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

        public void NewTradeRequest(IPoeLogEntry entry)
        {
            // TODO check for duplicate trade requests
            var interaction = MakeTradeInteraction(entry);
            
            interaction.InteractionContainer = this;
            Interactions.Add(interaction);
            this.PerformSafely(() => Controls.Add(interaction.Interaction_UI));
        }

        public void NewWhisper(IPoeLogEntry entry)
        {
            // TODO try to handle linq query without using a method call
            foreach (var interaction in Interactions.Where(interaction => interaction.HasPlayer(entry.Player))) 
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
            this.PerformSafely(() => Controls.Remove(tradeInteraction.Interaction_UI));
            Interactions.Remove(tradeInteraction);
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

        #region Edit bounds


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


        #endregion
    }

    public interface IInteractionContainer
    {
        /// <summary>
        ///     Removes element from the <see cref="IInteractionContainer" />
        /// </summary>
        /// <param name="tradeInteraction">TradeInteraction to be removed</param>
        public void RemoveInteraction(IPoeTradeInteraction tradeInteraction);

        public void NewTradeRequest(IPoeLogEntry entry);
        public void NewWhisper(IPoeLogEntry entry);
        public void TraderInArea(IPoeLogEntry entry, bool inArea);
        public void YouJoined(IPoeLogEntry entry);
    }
}