using System.Drawing;
using System.Windows.Forms;

namespace PoeAcolyte.UI.Components
{
    public class StashPanel
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
