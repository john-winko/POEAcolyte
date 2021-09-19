using System.Drawing;
using System.Windows.Forms;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;

namespace PoeAcolyte.API
{
    public class PoeSingleTrade : PoeWhisper
    {
        public PoeSingleTrade(IPoeLogEntry entry) : base(entry)
        {
            var form = new Form()
            {
                Visible = true,
                Size = new Size(500,500)
            };
            form.Controls.Add(new IncomingTradeControl());
            form.Show();
        }
    }
}