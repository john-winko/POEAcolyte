using PoeAcolyte.API.Parsers;

namespace PoeAcolyte.API.Interactions
{
    public class PoeWhisper : PoeInteraction
    {
        public string Message => Entry.Other;
        public PoeWhisper(IPoeLogEntry entry) : base(entry)
        {
            
        }


        public override void Update_UI()
        {
            // Do nothing
            throw new System.NotImplementedException();
        }
    }
}