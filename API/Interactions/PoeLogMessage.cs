using System.Diagnostics;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;

namespace PoeAcolyte.API.Interactions
{
    public class PoeLogMessage : IPoeLogMessage
    {
        public IPoeLogEntry Entry { get; }

        public PoeLogMessage(IPoeLogEntry entry) 
        {
            Entry = entry;
        }


    }

    public interface IPoeLogMessage
    {
        public IPoeLogEntry Entry { get;  }

    }


}
