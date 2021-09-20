using System;
using System.Collections.Generic;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;

namespace PoeAcolyte.API.Services
{
    public class PoeBroker : IDisposable
    {
        private readonly FileChangeMonitor _fileChangeMonitor;
        private const string POEPATH = @"C:\Program Files (x86)\Grinding Gear Games\Path of Exile\logs\Client.txt";
        public readonly List<IPoeInteraction> Interactions = new();
        public IInteractionContainer InteractionContainer { get; init; }
        
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
                case IPoeLogEntry.PoeLogEntryTypeEnum.YouJoin:
                    //DispatchYouJoin(entry);
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
                    //DispatchAreaJoined(entry);
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.AreaLeft:
                    // DispatchAreaLeft(entry);
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.SystemMessage:
                    //DispatchSystemMessage(entry);
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