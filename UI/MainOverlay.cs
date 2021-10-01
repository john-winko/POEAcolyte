using PoeAcolyte.API.Interactions;
using PoeAcolyte.API.Services;
using System;
using System.Windows.Forms;

namespace PoeAcolyte.UI
{
    public partial class MainOverlay : Form
    {
        private PoeBroker _broker;
        private readonly InteractionPanel _interactionPanel = new(){Name = "TradePanel"};

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

            AppSettings.UpdateControlLocation(new Control[]{HomePanel, StashPanel, _interactionPanel});
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
            AppSettings.StartEdit(Controls, new Control[]{HomePanel, StashPanel, _interactionPanel});
        }

        private void saveBoundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppSettings.StopEdit(Controls, new Control[] { HomePanel, StashPanel, _interactionPanel });
            AppSettings.Instance.Save();
        }

        private void HideoutButton_Click(object sender, EventArgs e)
        {
            GameClientCommand.MyHideout();
        }
    }

}
