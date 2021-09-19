using System;
using System.Collections.Generic;
using PoeAcolyte.API.Parsers;

namespace PoeAcolyte.API.Services
{
    public partial class PoeBroker : IDisposable
    {
        private FileChangeMonitor _fileChangeMonitor;
        private const string POEPATH = @"C:\Program Files (x86)\Grinding Gear Games\Path of Exile\logs\Client.txt";
        public readonly List<IPoeInteraction> Interactions = new();
        
        public bool Running
        {
            get => _fileChangeMonitor.Running;
            set => _fileChangeMonitor.Running = value;
        }

        public PoeBroker(string clientLogPath = POEPATH)
        {
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
                    AddInteraction(new PoeWhisper(entry));
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.YouJoin:
                    //DispatchYouJoin(entry);
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.PricedTrade:
                    AddInteraction(new PoeSingleTrade(entry));
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.UnpricedTrade:
                    AddInteraction(new PoeSingleTrade(entry));
                    break;
                case IPoeLogEntry.PoeLogEntryTypeEnum.BulkTrade:
                    AddInteraction(new PoeBulkTrade(entry));
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

        private void AddInteraction(IPoeInteraction newInteraction)
        {
            var results = Interactions.FindAll(entry => newInteraction.Player == entry.Player);
            if (results.Count == 0)
            {
                Interactions.Add(newInteraction);
                return;
            }

            foreach (var result in results)
            {
                result.AddInteraction(newInteraction);
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