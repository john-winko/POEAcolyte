using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoeAcolyte.API;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.API.Services;

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
