using System;
using System.Collections.Generic;
using System.Diagnostics;
using PoeAcolyte.API.Interactions;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;
using PoeAcolyte.UI.Interactions;

namespace PoeAcolyte.API.Services
{
    public class PoeBroker : IDisposable
    {
        private readonly FileChangeMonitor _fileChangeMonitor;
        private const string POEPATH = @"C:\Program Files (x86)\Grinding Gear Games\Path of Exile\logs\Client.txt";
        public readonly List<IPoeInteraction> Interactions = new();
        public IInteractionContainer InteractionContainer { get; init; }
        private PoeClient _poeClient;
        public bool Running
        {
            get => _fileChangeMonitor.Running;
            set => _fileChangeMonitor.Running = value;
        }

        public PoeBroker(IInteractionContainer interactionContainer, string clientLogPath = POEPATH)
        {
            InteractionContainer = interactionContainer;
            _fileChangeMonitor = new FileChangeMonitor(clientLogPath);
            _fileChangeMonitor.FileChanged += ClientLogFileChanged;
            _poeClient = new PoeClient();
            _poeClient.GameClientOpened += OnGameClientOpened;
            _poeClient.GameClientClosed += OnGameClientClosed;
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
            var logEntries = IPoeLogEntry.ParseStrings(e.Changes);
            foreach (var entry in logEntries)
            {
                DispatchLogEvent(entry);
            }
            
        }
        
        private void DispatchLogEvent(IPoeLogEntry entry)
        {
            switch (entry.PoeLogEntryType)
            {
                case IPoeLogEntry.PoeLogEntryTypeEnum.Whisper:
                    InteractionContainer.AddInteraction(new PoeWhisper(entry));
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.PricedTrade:
                    InteractionContainer.AddInteraction(new PoeSingleTrade(entry));
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.UnpricedTrade:
                    InteractionContainer.AddInteraction(new PoeSingleTrade(entry));
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.BulkTrade:
                    InteractionContainer.AddInteraction(new PoeBulkTrade(entry));
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.AreaJoined:
                case IPoeLogEntry.PoeLogEntryTypeEnum.YouJoin:    
                case IPoeLogEntry.PoeLogEntryTypeEnum.AreaLeft:
                case IPoeLogEntry.PoeLogEntryTypeEnum.SystemMessage:
                    InteractionContainer.AddEvent(new PoeEvent(entry));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ManualFire()
        {
            _fileChangeMonitor.ManualFire();
        }

        public void Dispose()
        {
            _fileChangeMonitor?.Dispose();
        }
    }
}