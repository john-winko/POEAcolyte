using System.Windows.Forms;
using PoeAcolyte.API.Interactions;

namespace PoeAcolyte.UI.Interactions
{
    public partial class BulkTradeUI : UserControl
    {
        private readonly IPoeInteraction _interaction;

        public BulkTradeUI()
        {
            InitializeComponent();

        }

        public BulkTradeUI(IPoeInteraction interaction) : this()
        {
            _interaction = interaction;
        }

        private void CloseButton_Click(object sender, System.EventArgs e)
        {
            _interaction.Complete();
        }
    }
}