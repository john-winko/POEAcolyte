using PoeAcolyte.API.Services;
using System;
using System.Windows.Forms;

namespace PoeAcolyte.UI
{
    public partial class MainOverlay : Form
    {
        private PoeBroker _broker;
        public MainOverlay()
        {
            InitializeComponent();
            Show();
            _broker = new PoeBroker(_interactionPanel);
            //var test = PoeClient.GetPoeProcess();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void _btnTest_Click(object sender, EventArgs e)
        {
            _broker.ManualFire();
        }
    }
}
