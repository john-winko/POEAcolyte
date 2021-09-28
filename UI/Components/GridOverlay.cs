using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using PoeAcolyte.API.Parsers;

namespace PoeAcolyte.UI.Components
{
    public class GridOverlay : IDisposable
    {
        public bool HideOnClick { get; init; } = true;
        public List<GridOverlayCell> CellHighlights { get; set; } = new();
        private readonly IKeyboardMouseEvents _mouseEvents = Hook.GlobalEvents();

        private void OnMouseClick(object? sender, MouseEventArgs e)
        {
            if (!HideOnClick) return;
            
            CellHighlights.ForEach(cell => cell.CheckMouseClick(e));
        }

        public GridOverlay(IPoeLogEntry entry)
        {
            if (entry.Top is < 1 or > 24 || entry.Left is < 1 or > 24)
                throw new Exception("entry location invalid");

            // all valid values can be a in a 24x24
            CellHighlights.Add(new GridOverlayCell(entry.Left, entry.Top, 24,24));

            // only low values can be in 12x12 grid
            if (entry.Top > 12 || entry.Left > 12) return;
            CellHighlights.Add(new GridOverlayCell(entry.Left, entry.Top, 12,12));

            HideAll();
            //HideOnClick = true;
        }

        public void Show()
        {
            _mouseEvents.MouseClick += OnMouseClick;
            CellHighlights.ForEach(cell => cell.Visible = true);
        }

        public void HideAll()
        {
            // TODO possible buggy behavior with subscriptions so using disposal instead
            // do i need to change back?
            _mouseEvents.Dispose();
            CellHighlights.ForEach(cell => cell.Visible = false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _mouseEvents.Dispose();
            CellHighlights.ForEach(cell => cell.Dispose());
        }
    }
}
