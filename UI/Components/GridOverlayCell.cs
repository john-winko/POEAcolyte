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
            var result = AppSettings.Instance.GetUiSettings("StashPanel");
            
            float widthPerCell = result.Size.Width / gridX;
            float heightPerCell = result.Size.Height / gridY;
            float x = result.Location.X + ((left - 1) * widthPerCell);
            float y = result.Location.Y + ((top - 1) * heightPerCell);
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
