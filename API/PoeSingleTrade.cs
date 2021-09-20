using System.Drawing;
using System.Windows.Forms;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;

namespace PoeAcolyte.API
{
    public class PoeSingleTrade : PoeWhisper
    {
        private SingleTradeUI _ui;
        public override UserControl InteractionUI => _ui;

        public PoeSingleTrade(IPoeLogEntry entry) : base(entry)
        {
            _ui = new SingleTradeUI();
            
            UpdateUI();
        }

        public void UpdateUI()
        {
            _ui.TestBox.Text = Entry.ToString();
        }

        public override bool ShouldAdd(IPoeInteraction interaction)
        {
            if (interaction.Entry.PoeLogEntryType != IPoeLogEntry.PoeLogEntryTypeEnum.Whisper && 
                interaction.Entry.PoeLogEntryType != IPoeLogEntry.PoeLogEntryTypeEnum.PricedTrade) return false;

            // whisper from same player
            if (interaction.Entry.Player == Entry.Player &&
                interaction.Entry.PoeLogEntryType == IPoeLogEntry.PoeLogEntryTypeEnum.Whisper)
            {
                History.Add(interaction);
                return true;
            }

            // TODO add logic for duplicate trade requests
            return false;
        }
    }
}