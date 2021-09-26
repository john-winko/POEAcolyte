using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using PoeAcolyte.API;
using PoeAcolyte.API.Parsers;

namespace PoeAcolyte.UI
{
    public partial class CellHighlight : Form
    {
        private readonly IKeyboardMouseEvents _mouseEvents = Hook.GlobalEvents();
        public bool HideOnClick { get; init; }
        public CellHighlight()
        {
            InitializeComponent();
           _mouseEvents.MouseClick += OnMouseClick;
            HideOnClick = true;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (!HideOnClick && Visible) return;

            // hide if clicked inside bounds
            if (new Rectangle(Location, Size).Contains(e.Location)) Visible = false;
        }

        public CellHighlight(float left, float top, float gridX = 24, float gridY = 24) : this()
        {
            float widthPerCell = GameClient.Default.StashUISize.Width / gridX;
            float heightPerCell = GameClient.Default.StashUISize.Height / gridY;
            float x = GameClient.Default.StashUILeft + ((left - 1) * widthPerCell);
            float y = GameClient.Default.StashUITop + ((top - 1) * heightPerCell);
            Show();
            Location = new Point((int)x, (int)y);
            Size = new Size((int)widthPerCell, (int)heightPerCell);
        }

        public static CellHighlight GetCellHighlight(IPoeLogEntry entry, float gridSize)
        {
            if (entry.Top <= gridSize && entry.Left <= gridSize)
            {
                return new CellHighlight(entry.Left, entry.Top, gridSize, gridSize);
            }

            return null;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.MakeFormTransparent();
        }
    }
}
