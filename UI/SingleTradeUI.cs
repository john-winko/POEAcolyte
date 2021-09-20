using System.Windows.Forms;

namespace PoeAcolyte.UI
{
    public partial class SingleTradeUI : UserControl
    {
        public SingleTradeUI()
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