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

            // adding ToolStripMenuItem[] was giving co-variant error when writing... using a for loop for now
            var collection = GameClientCommand.CreateMenuItems(_tradeInteraction);
            foreach (var toolStripMenuItem in collection)
            {
                MenuStrip.Items.Add(toolStripMenuItem);
            }

            // Make all controls use same context menu
            foreach (Control control in this.Controls)
            {
                control.ContextMenuStrip = MenuStrip;
            }
        }
        private void CloseButton_Click(object sender, System.EventArgs e)
        {
            _tradeInteraction.Complete();
        }

        private void SearchButton_Click(object sender, System.EventArgs e)
        {
            _tradeInteraction.ShowItemOverlay();
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