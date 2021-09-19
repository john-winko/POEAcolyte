using System.Windows.Forms;

namespace PoeAcolyte.UI
{
    public partial class IncomingTradeControl : UserControl
    {
        public IncomingTradeControl()
        {
            InitializeComponent();
            Controls.Add(new Button()
            {
                Name = "Quick button",
                Text = "click me"
            });
        }
    }
}