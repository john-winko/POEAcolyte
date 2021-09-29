using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PoeAcolyte.UI.Components
{
    public class StashPanel : FlowLayoutPanel
    {
        private readonly FrameControl _frameControl = new()
        {
            Description = @"Stash Tab Panel",
            Location = new Point(GameClient.Default.StashUILeft, GameClient.Default.StashUITop),
            Size = GameClient.Default.StashUISize
        };

        public void EditSettings(Control.ControlCollection owner)
        {

            _frameControl.Resize += (o, args) =>
            {
                if (o?.GetType() != typeof(FrameControl)) return;
                var frame = (FrameControl)o;
                GameClient.Default.StashUITop = frame.Top;
                GameClient.Default.StashUILeft = frame.Left;
                GameClient.Default.StashUISize = frame.Size;
            };
            _frameControl.ClickHandler += (sender, args) =>
            {
                var ev = (MouseEventArgs) args;
                if (ev.Button == MouseButtons.Right)
                {
                    SaveSettings(owner);
                }
            };
            owner.Add(_frameControl);
            _frameControl.BringToFront();
        }

        public void SaveSettings(Control.ControlCollection owner)
        {
            GameClient.Default.Save();
            owner.Remove(_frameControl);
        }

    }

    public class HomeRibbon
    {
        private readonly FrameControl _frameControl = new()
        {
            Description = @"Home Ribbon",
            Location = new Point(GameClient.Default.HomeUILeft, GameClient.Default.HomeUITop),
            Size = GameClient.Default.StashUISize
        };

        public void EditSettings(Control.ControlCollection owner)
        {

            _frameControl.Resize += (o, args) =>
            {
                if (o?.GetType() != typeof(FrameControl)) return;
                var frame = (FrameControl)o;
                GameClient.Default.HomeUITop = frame.Top;
                GameClient.Default.HomeUILeft = frame.Left;
                GameClient.Default.HomeUISize = frame.Size;
            };
            owner.Add(_frameControl);
            _frameControl.BringToFront();
        }

        public void SaveSettings(Control.ControlCollection owner)
        {
            GameClient.Default.Save();
            owner.Remove(_frameControl);
        }
    }
}
