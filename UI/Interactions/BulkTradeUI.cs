using System.Windows.Forms;
using PoeAcolyte.API.Interactions;
// ReSharper disable InconsistentNaming

namespace PoeAcolyte.UI.Interactions
{
    public partial class BulkTradeUI : UserControl
    {
        private readonly IPoeTradeInteraction _tradeInteraction;

        public BulkTradeUI()
        {
            InitializeComponent();

        }

        public BulkTradeUI(IPoeTradeInteraction tradeInteraction) : this()
        {
            _tradeInteraction = tradeInteraction;
            _tradeInteraction.BuildContextMenu(MenuStrip);

            // Make all controls use same context menu
            foreach (Control control in this.Controls)
            {
                control.ContextMenuStrip = MenuStrip;
                if (control.GetType() == typeof(Button)) continue;

                control.Click += (sender, args) => { GameClientCommand.QuickAction(_tradeInteraction); };
            }
        }
        private void CloseButton_Click(object sender, System.EventArgs e)
        {
            _tradeInteraction.Complete();
        }

        private void HideoutButton_Click(object sender, System.EventArgs e)
        {
            GameClientCommand.Hideout(_tradeInteraction);
        }

        private void InviteButton_Click(object sender, System.EventArgs e)
        {
            GameClientCommand.Invite(_tradeInteraction);
        }

        private void TradeButton_Click(object sender, System.EventArgs e)
        {
            GameClientCommand.Trade(_tradeInteraction);
        }

        private void KickButton_Click(object sender, System.EventArgs e)
        {
            GameClientCommand.Tygl(_tradeInteraction);
        }
    }
}