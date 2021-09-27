using System;
using System.Drawing;
using System.Windows.Forms;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI.Components;
using PoeAcolyte.UI.Interactions;

namespace PoeAcolyte.API.Interactions
{
    public class PoeTradeSingle : PoeTradeInteraction
    {
        private readonly SingleTradeUI _ui;
        private GridOverlay _gridOverlay;

        public PoeTradeSingle(IPoeLogEntry entry) : base(entry)
        {
            _ui = new SingleTradeUI(this);

            _ui.PerformSafely(Update_UI);
        }

        public override UserControl Interaction_UI => _ui;

        public override bool PlayerInArea
        {
            get => base.PlayerInArea;
            set
            {
                base.PlayerInArea = value;
                _ui.PerformSafely(() =>
                {
                    var newTooltip =
                        $@"{_ui.ToolTipHistory.GetToolTip(_ui.InfoLabel)} {Environment.NewLine}I {(value ? "joined" : "left")}";
                    _ui.ToolTipHistory.SetToolTip(_ui.InfoLabel, newTooltip);
                });
            }
        }

        public override bool TraderInArea
        {
            get => base.TraderInArea;
            set
            {
                base.TraderInArea = value;
                _ui.PerformSafely(() =>
                {
                    var newTooltip =
                        $@"{_ui.ToolTipHistory.GetToolTip(_ui.InfoLabel)} {Environment.NewLine}They {(value ? "joined" : "left")}";
                    _ui.ToolTipHistory.SetToolTip(_ui.InfoLabel, newTooltip);
                });
            }
        }

        public override GameClientCommandTypeEnum LastChatConsoleCommand
        {
            get => base.LastChatConsoleCommand;
            set
            {
                // TODO update quick action button for what will occur next
                base.LastChatConsoleCommand = value;
                _ui.PerformSafely(() =>
                {
                    var newTooltip =
                        $@"{_ui.ToolTipHistory.GetToolTip(_ui.InfoLabel)} {Environment.NewLine} {value.ToString()}";
                    _ui.ToolTipHistory.SetToolTip(_ui.InfoLabel, newTooltip);
                });
            }
        }

        public sealed override void Update_UI()
        {
            if (Entry.Incoming)
            {
                _ui.ToolTipHistory.SetToolTip(_ui.InfoLabel, @"Incoming");
                _ui.BackColor = Color.LightSteelBlue;
            }
            else
            {
                _ui.ToolTipHistory.SetToolTip(_ui.InfoLabel, @"Outgoing");
                _ui.BackColor = Color.Beige;
            }

            _ui.InfoLabel.Text =
                $@"{Entry.Player} {Environment.NewLine}{Entry.StashTab}{Environment.NewLine}{Entry.Item}";
            ;
            _ui.LocationLabel.Text = $@"({Entry.Top}, {Entry.Left})";
            _ui.CurrencyPicture.BackgroundImage = CurrencyConverter.GetFromString(Entry.PriceUnits);
            _ui.PriceLabel.Text = Entry.PoeLogEntryType == PoeLogEntryTypeEnum.UnpricedTrade
                ? ""
                : $"{Entry.PriceAmount}";

            _ui.ToolTipHistory.SetToolTip(_ui.PriceLabel, MessageHistory);
        }

        public override bool ShouldAdd(IPoeLogEntry logEntry)
        {
            // Only proceed if logEntry is correct type
            switch (logEntry.PoeLogEntryType)
            {
                case PoeLogEntryTypeEnum.Whisper:
                case PoeLogEntryTypeEnum.PricedTrade:
                case PoeLogEntryTypeEnum.UnpricedTrade:
                    break;
                case PoeLogEntryTypeEnum.BulkTrade:
                case PoeLogEntryTypeEnum.AreaJoined:
                case PoeLogEntryTypeEnum.AreaLeft:
                case PoeLogEntryTypeEnum.YouJoin:
                case PoeLogEntryTypeEnum.SystemMessage:
                    return false;
                default:
                    return false;
            }

            return Entry.IsSameItem(logEntry) || base.ShouldAdd(logEntry);
        }

        public override void Complete()
        {
            _gridOverlay?.Dispose();
            base.Complete();
        }

        public override bool ShowItemOverlay()
        {
            // only init if none exists
            _gridOverlay ??= new GridOverlay(Entry);
            _gridOverlay.Show();
            return true;
        }
    }
}