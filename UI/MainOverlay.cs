using PoeAcolyte.API.Services;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PoeAcolyte.UI
{
    public partial class MainOverlay : Form
    {
        private PoeBroker _broker;
        private readonly InteractionPanel _interactionPanel=new();
        private readonly StashPanel _stashPanel = new();

        public MainOverlay()
        {
            InitializeComponent();
            Show();
            InitCustom();
        }

        private void InitCustom()
        {
            Controls.Add(_interactionPanel);
            _broker = new PoeBroker(_interactionPanel);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonTest3_Click(object sender, EventArgs e)
        {
            _broker.ManualFire();
            CellHighlight cell = new(24,24);
            cell.Show();
        }

        private void editBoundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _interactionPanel.EditSettings(Controls);
            _stashPanel.EditSettings(Controls);
        }

        private void saveBoundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _interactionPanel.SaveSettings(Controls);
            _stashPanel.SaveSettings(Controls);
        }
    }
}
