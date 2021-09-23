using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI.Interactions;
using System.Drawing;
using System.Windows.Forms;

namespace PoeAcolyte.API.Interactions
{
    public class PoeTradeBulk : PoeTradeInteraction
    {
        private readonly BulkTradeUI _ui;
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
                base.LastChatConsoleCommand = value;
                _ui.PerformSafely(() => _ui.LabelStatus.Text = value.ToString());
            }
        }

        public PoeTradeBulk(IPoeLogEntry entry) : base(entry)
        {
            _ui = new BulkTradeUI(this);

            Update_UI();
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
            _ui.PriceIn.Text = $@"{Entry.PriceAmount} {Entry.PriceUnits}";
            _ui.PriceOut.Text = $@"{Entry.BuyPriceAmount} {Entry.BuyPriceUnits}";

            _ui.ToolTipHistory.SetToolTip(_ui.QuickButton, MessageHistory);

        }

        public override bool ShouldAdd(IPoeLogEntry logEntry)
        {
            // correct type
            if (logEntry.PoeLogEntryType != PoeLogEntryTypeEnum.Whisper &&
                logEntry.PoeLogEntryType != PoeLogEntryTypeEnum.BulkTrade) return false;

            return base.ShouldAdd(logEntry);

            // TODO add logic for duplicate trade requests
        }


    }
}