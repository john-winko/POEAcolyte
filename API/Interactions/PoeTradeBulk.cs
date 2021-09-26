using System;
using System.Drawing;
using System.Windows.Forms;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI.Interactions;

namespace PoeAcolyte.API.Interactions
{
    public class PoeTradeBulk : PoeTradeInteraction
    {
        private readonly BulkTradeUI _ui;

        public PoeTradeBulk(IPoeLogEntry entry) : base(entry)
        {
            _ui = new BulkTradeUI(this);

            Update_UI();
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
                    var newTooltip = $@"{_ui.ToolTipHistory.GetToolTip(_ui.InfoLabel)} {Environment.NewLine}I {(value ? "joined" : "left")}";
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
                    var newTooltip = $@"{_ui.ToolTipHistory.GetToolTip(_ui.InfoLabel)} {Environment.NewLine}They {(value ? "joined" : "left")}";
                    _ui.ToolTipHistory.SetToolTip(_ui.InfoLabel, newTooltip);
                });
            }
        }

        public override GameClientCommandTypeEnum LastChatConsoleCommand
        {
            get => base.LastChatConsoleCommand;
            set
            {
                base.LastChatConsoleCommand = value;
                _ui.PerformSafely(() =>
                {
                    var newTooltip = $@"{_ui.ToolTipHistory.GetToolTip(_ui.InfoLabel)} {Environment.NewLine} {value.ToString()}";
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

            var info = $@"{Entry.Player} {Environment.NewLine}{Entry.StashTab} {Environment.NewLine}({Entry.Top}, {Entry.Left})";

            _ui.InfoLabel.Text = info;
            _ui.ToolTipHistory.SetToolTip(_ui.InfoLabel, info);

            // TODO add picture dictionary
            _ui.PriceInLabel.Text = $@"{Entry.PriceAmount}";
            _ui.PriceOutLabel.Text = $@"{Entry.BuyPriceAmount}";

            _ui.ToolTipHistory.SetToolTip(_ui.PriceInLabel, MessageHistory);
        }

        public override bool ShouldAdd(IPoeLogEntry logEntry)
        {
            // correct type
            if (logEntry.PoeLogEntryType != PoeLogEntryTypeEnum.Whisper &&
                logEntry.PoeLogEntryType != PoeLogEntryTypeEnum.BulkTrade) return false;

            return base.ShouldAdd(logEntry);

            // TODO add logic for duplicate trade requests
        }

        public override bool ShowItemOverlay()
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return false;
            // TODO get dictionary/lookup of bulk trade locations
        }
    }
}