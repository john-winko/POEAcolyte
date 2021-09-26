using System;
using System.Drawing;
using System.Windows.Forms;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;
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
                    var newTooltip = $@"{_ui.ToolTipHistory.GetToolTip(_ui.LabelInfo)} {Environment.NewLine}I {(value ? "joined" : "left")}";
                    _ui.ToolTipHistory.SetToolTip(_ui.LabelInfo, newTooltip);
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
                    var newTooltip = $@"{_ui.ToolTipHistory.GetToolTip(_ui.LabelInfo)} {Environment.NewLine}They {(value ? "joined" : "left")}";
                    _ui.ToolTipHistory.SetToolTip(_ui.LabelInfo, newTooltip);
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
                    var newTooltip = $@"{_ui.ToolTipHistory.GetToolTip(_ui.LabelInfo)} {Environment.NewLine} {value.ToString()}";
                    _ui.ToolTipHistory.SetToolTip(_ui.LabelInfo, newTooltip);
                });
            }
        }

        public sealed override void Update_UI()
        {
            if (Entry.Incoming)
            {
                _ui.ToolTipHistory.SetToolTip(_ui.LabelInfo, @"Incoming");
                _ui.BackColor = Color.Pink;
            }
            else
            {
                _ui.ToolTipHistory.SetToolTip(_ui.LabelInfo, @"Outgoing");
                _ui.BackColor = Color.LightGreen;
            }
            var info =  $@"{Entry.Player} {Environment.NewLine}{Entry.StashTab} {Environment.NewLine}({Entry.Top}, {Entry.Left})";

            _ui.LabelInfo.Text = info;
            _ui.ToolTipHistory.SetToolTip(_ui.LabelInfo, info);

            _ui.PriceLabel.Text = Entry.PoeLogEntryType == PoeLogEntryTypeEnum.UnpricedTrade
                ? ""
                : $"{Entry.PriceAmount}";

            _ui.ToolTipHistory.SetToolTip(_ui.InviteButton, MessageHistory);
        }

        public override bool ShouldAdd(IPoeLogEntry logEntry)
        {
            // correct type
            if (logEntry.PoeLogEntryType != PoeLogEntryTypeEnum.Whisper &&
                logEntry.PoeLogEntryType != PoeLogEntryTypeEnum.PricedTrade &&
                logEntry.PoeLogEntryType != PoeLogEntryTypeEnum.UnpricedTrade) return false;

            return base.ShouldAdd(logEntry);

            // TODO add logic for duplicate trade requests
        }

        public override void Complete()
        {
            _gridOverlay.Dispose();
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