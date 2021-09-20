using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoeAcolyte.API.Parsers;

namespace PoeAcolyte.API.Interactions
{
    public class PoeEvent : IPoeEvent
    {
        public IPoeLogEntry Entry { get; }

        public PoeEvent(IPoeLogEntry entry)
        {
            Entry = entry;
        }

    }

    public interface IPoeEvent
    {
        public IPoeLogEntry Entry { get;  }
    }


}
