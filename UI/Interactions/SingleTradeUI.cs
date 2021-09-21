using System.Windows.Forms;
using PoeAcolyte.API.Interactions;

namespace PoeAcolyte.UI.Interactions
{
    public partial class SingleTradeUI : UserControl
    {
        private readonly IPoeInteraction _interaction;

        public SingleTradeUI()
        {
            InitializeComponent();

        }

        public SingleTradeUI(IPoeInteraction interaction) : this()
        {
            _interaction = interaction;
        }

        private void CloseButton_Click(object sender, System.EventArgs e)
        {
            _interaction.Complete();
        }
    }
}