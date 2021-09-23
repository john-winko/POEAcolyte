using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PoeAcolyte.UI
{
    public partial class CellHighlight : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;

        public CellHighlight()
        {
            InitializeComponent();
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, style | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
    }
}
