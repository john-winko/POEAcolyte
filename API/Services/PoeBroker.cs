﻿using System;
using System.Diagnostics;
using System.Linq;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;

namespace PoeAcolyte.API.Services
{
    public class PoeBroker : IDisposable
    {
        private readonly FileChangeMonitor _fileChangeMonitor;
        private readonly PoeClient _poeClient;

        private PoeBroker(IInteractionContainer interactionContainer) 
        {
            InteractionContainer = interactionContainer;
            _fileChangeMonitor = new FileChangeMonitor(AppSettings.Instance.ClientPath);
            _fileChangeMonitor.FileChanged += ClientLogFileChanged;
            _poeClient = PoeClient.GetInstance();
            _poeClient.GameClientOpened += OnGameClientOpened;
            _poeClient.GameClientClosed += OnGameClientClosed;
        }

        public static PoeBroker Instance { get; set; }

        public IInteractionContainer InteractionContainer { get; init; }
        public string PlayerArea { get; set; } = "";
        public bool PlayerBusy => PlayerArea.Contains("Hideout") ;

        public bool Running
        {
            get => _fileChangeMonitor.Running;
            set => _fileChangeMonitor.Running = value;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _fileChangeMonitor?.Dispose();
            _poeClient?.Dispose();
        }

        public static PoeBroker Start(IInteractionContainer interactionContainer)
        {
            Instance = new PoeBroker(interactionContainer);
            return Instance;
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

        /// <summary>
        ///     Parses changes to the client log file arrayed by new lines. Pushes those entries to
        ///     <see cref="InteractionContainer" /> based on log entry type
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Changes captured</param>
        private void ClientLogFileChanged(object sender, FileChangedEventArgs e)
        {
            //var logEntries = IPoeLogEntry.ParseStrings(e.Changes);
            foreach (var entry in IPoeLogEntry.ParseStrings(e.Changes).Where(log => log.IsValid)) //logEntries)
                switch (entry.PoeLogEntryType)
                {
                    case PoeLogEntryTypeEnum.Whisper:
                        InteractionContainer.NewWhisper(entry);
                        break;
                    case PoeLogEntryTypeEnum.PricedTrade:
                    case PoeLogEntryTypeEnum.UnpricedTrade:
                    case PoeLogEntryTypeEnum.BulkTrade:
                        InteractionContainer.NewTradeRequest(entry);
                        break;
                    case PoeLogEntryTypeEnum.AreaJoined:
                        InteractionContainer.TraderInArea(entry, true);
                        break;
                    case PoeLogEntryTypeEnum.AreaLeft:
                        InteractionContainer.TraderInArea(entry, false);
                        break;
                    case PoeLogEntryTypeEnum.YouJoin:
                        PlayerArea = entry.Area;
                        InteractionContainer.YouJoined(entry);
                        break;
                    case PoeLogEntryTypeEnum.SystemMessage:
                        break;
                    default:
                        Debug.Print("out of bounds");
                        break;
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iterations">Number of times to manually fire the file change monitoring</param>
        public void ManualFire(int iterations = 1)
        {
            if (iterations < 1) return;

            while (iterations > 0)
            {
                iterations--;
                _fileChangeMonitor.ManualFire();
            }
            
        }
    }
}