using PoeAcolyte.API.Parsers;

namespace PoeAcolyte.API
{
    public class PoeWhisper : PoeInteraction
    {
        public string Message => Entry.Other;
        public PoeWhisper(IPoeLogEntry entry) : base(entry)
        {
            
        }
    }
}