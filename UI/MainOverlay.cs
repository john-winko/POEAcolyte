using PoeAcolyte.API.Services;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using PoeAcolyte.API;
using PoeAcolyte.API.Interactions;
using PoeAcolyte.UI.Components;

namespace PoeAcolyte.UI
{
    public partial class MainOverlay : Form
    {
        private PoeBroker _broker;
        private readonly InteractionPanel _interactionPanel=new();
        private readonly StashPanel _stashPanel = new();
        private readonly HomeRibbon _ribbon = new();

        public MainOverlay()
        {
            InitializeComponent();
            Show();
            InitCustom();
        }

        private void InitCustom()
        {
            Controls.Add(_interactionPanel);
            _broker = PoeBroker.Start(_interactionPanel);
            HomePanel.Location = new Point(GameClient.Default.HomeUILeft, GameClient.Default.HomeUITop);
            HomePanel.Size = GameClient.Default.HomeUISize;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonTest3_Click(object sender, EventArgs e)
        {
            _broker.ManualFire();

        }

        private void editBoundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _interactionPanel.EditSettings(Controls);
            _stashPanel.EditSettings(Controls);
            _ribbon.EditSettings(Controls);
        }

        private void saveBoundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _interactionPanel.SaveSettings(Controls);
            _stashPanel.SaveSettings(Controls);
            _ribbon.SaveSettings(Controls);
            HomePanel.Location = new Point(GameClient.Default.HomeUILeft, GameClient.Default.HomeUITop);
            HomePanel.Size = GameClient.Default.HomeUISize;
        }

        private void HideoutButton_Click(object sender, EventArgs e)
        {
            GameClientCommand.MyHideout();
        }
    }
}
