using System.Drawing;
using System.Windows.Forms;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI.Interactions;

namespace PoeAcolyte.API.Interactions
{
    public class PoeTradeSingle : PoeTradeInteraction
    {
        private readonly SingleTradeUI _ui;

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
                _ui.PerformSafely(() => _ui.LabelStatus.Text = $@"I {(value ? "joined" : "left")}");
            }
        }

        public override bool TraderInArea
        {
            get => base.TraderInArea;
            set
            {
                base.TraderInArea = value;
                _ui.PerformSafely(() => _ui.LabelStatus.Text = $@"They {(value ? "joined" : "left")}");
            }
        }

        public override GameClientCommandTypeEnum LastChatConsoleCommand
        {
            get => base.LastChatConsoleCommand;
            set
            {
                // TODO update quick action button for what will occur next
                base.LastChatConsoleCommand = value;
                _ui.PerformSafely(() => _ui.LabelStatus.Text = value.ToString());
            }
        }

        public sealed override void Update_UI()
        {
            if (Entry.Incoming)
            {
                _ui.IncomingLabel.Text = @"Incoming";
                _ui.IncomingLabel.BackColor = Color.LightBlue;
                _ui.BackColor = Color.Pink;
            }
            else
            {
                _ui.IncomingLabel.Text = @"Outgoing";
                _ui.IncomingLabel.BackColor = Color.LightYellow;
                _ui.BackColor = Color.LightGreen;
            }

            _ui.PlayerLabel.Text = Entry.Player;

            _ui.PriceLabel.Text = Entry.PoeLogEntryType == PoeLogEntryTypeEnum.UnpricedTrade
                ? "Unpriced"
                : $"{Entry.PriceAmount} {Entry.PriceUnits}";

            _ui.LocationLabel.Text = $@"({Entry.Top}, {Entry.Left}) {Entry.StashTab}";

            _ui.ToolTipHistory.SetToolTip(_ui.QuickButton, MessageHistory);
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
    }
}