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
            // AppSettings.UpdateControlLocation(HomePanel);// HomePanel.LoadUiBounds("HomeRibbon");
            // AppSettings.UpdateControlLocation(StashPanel);//StashPanel.LoadUiBounds("StashTab");
            // AppSettings.UpdateControlLocation(_interactionPanel);//_interactionPanel.LoadUiBounds("TradeInterface");
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
            // AppSettings.Instance.GetUiSettings("TradeInterface").Edit(Controls);
            // AppSettings.Instance.GetUiSettings("StashTab").Edit(Controls);
            AppSettings.StartEdit(Controls, new Control[]{HomePanel, StashPanel, _interactionPanel});
        }

        private void saveBoundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AppSettings.Instance.GetUiSettings("TradeInterface").StopEdit(Controls);
            // AppSettings.Instance.GetUiSettings("StashTab").StopEdit(Controls);
            //
            // HomePanel.LoadUiBounds("HomeRibbon");
            // StashPanel.LoadUiBounds("StashTab");
            AppSettings.StopEdit(Controls, new Control[] { HomePanel, StashPanel, _interactionPanel });
            // AppSettings.StopEdit(Controls, HomePanel);
            // AppSettings.StopEdit(Controls, StashPanel);
            // AppSettings.StopEdit(Controls, _interactionPanel);
            AppSettings.Instance.Save();
        }

        private void HideoutButton_Click(object sender, EventArgs e)
        {
            GameClientCommand.MyHideout();
        }
    }

}
