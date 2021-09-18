using System;

namespace PoeAcolyte.API.Services
{
    public class FileChangedEventArgs : EventArgs
    {
        public string Changes { get; }

        public FileChangedEventArgs(string changes)
        {
            Changes = changes;
        }
    }
}