using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;
using System;
using System.Diagnostics;

namespace PoeAcolyte.API.Services
{
    public class PoeBroker : IDisposable
    {
        private const string POEPATH = @"C:\Program Files (x86)\Grinding Gear Games\Path of Exile\logs\Client.txt";
        private readonly FileChangeMonitor _fileChangeMonitor;
        private readonly PoeClient _poeClient;

        public PoeBroker(IInteractionContainer interactionContainer, string clientLogPath = POEPATH)
        {
            InteractionContainer = interactionContainer;
            _fileChangeMonitor = new FileChangeMonitor(clientLogPath);
            _fileChangeMonitor.FileChanged += ClientLogFileChanged;
            _poeClient = PoeClient.GetInstance();
            _poeClient.GameClientOpened += OnGameClientOpened;
            _poeClient.GameClientClosed += OnGameClientClosed;
        }

        public IInteractionContainer InteractionContainer { get; init; }

        public bool Running
        {
            get => _fileChangeMonitor.Running;
            set => _fileChangeMonitor.Running = value;
        }

        public void Dispose()
        {
            _fileChangeMonitor?.Dispose();
            _poeClient?.Dispose();
        }

        private void OnGameClientClosed(object sender, EventArgs e)
        {
            Running = false;
            Debug.Print("not searching log");
        }

        private void OnGameClientOpened(object sender, EventArgs e)
        {
            Running = true;
            Debug.Print("started searching log");
        }

        private void ClientLogFileChanged(object sender, FileChangedEventArgs e)
        {
            //var logEntries = IPoeLogEntry.ParseStrings(e.Changes);
            foreach (var entry in IPoeLogEntry.ParseStrings(e.Changes)) //logEntries)
                if (entry.IsValid)
                    InteractionContainer.HandleNewLogEntry(entry);
        }

        public void ManualFire()
        {
            _fileChangeMonitor.ManualFire();
        }
    }
}