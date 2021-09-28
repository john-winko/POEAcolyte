using PoeAcolyte.API;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PoeAcolyte.UI.Components
{
    public partial class GridOverlayCell : Form
    {
        public GridOverlayCell()
        {
            InitializeComponent();
        }

        public GridOverlayCell(float left, float top, float gridX = 24, float gridY = 24) : this()
        {
            float widthPerCell = GameClient.Default.StashUISize.Width / gridX;
            float heightPerCell = GameClient.Default.StashUISize.Height / gridY;
            float x = GameClient.Default.StashUILeft + ((left - 1) * widthPerCell);
            float y = GameClient.Default.StashUITop + ((top - 1) * heightPerCell);
            Show();
            Location = new Point((int)x, (int)y);
            Size = new Size((int)widthPerCell, (int)heightPerCell);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.MakeFormTransparent(); // WIN 32 extension
        }

        public bool CheckMouseClick(MouseEventArgs e)
        {
            // hide if clicked inside bounds
            if (new Rectangle(Location, Size).Contains(e.Location)) Visible = false;
            return Visible;
        }
    }
}
