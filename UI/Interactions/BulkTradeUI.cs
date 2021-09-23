using System.Windows.Forms;
using PoeAcolyte.API.Interactions;

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

            foreach (Control control in this.Controls)
            {
                control.ContextMenuStrip = MenuStrip;
            }
        }

        private void CloseButton_Click(object sender, System.EventArgs e)
        {
            _tradeInteraction.Complete();
        }
    }
}