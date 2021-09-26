using System;
using System.Diagnostics;
using System.Linq;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.UI;

namespace PoeAcolyte.API.Services
{
    public class PoeBroker : IDisposable
    {
        private const string POEPATH = @"C:\Program Files (x86)\Grinding Gear Games\Path of Exile\logs\Client.txt";
        private readonly FileChangeMonitor _fileChangeMonitor;
        private readonly PoeClient _poeClient;
        private string _currentArea;

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

        /// <summary>
        ///     Parses changes to the client log file arrayed by new lines. Pushes those entries to <see cref="InteractionContainer"/> based on log entry type
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Changes captured</param>
        private void ClientLogFileChanged(object sender, FileChangedEventArgs e)
        {
            //var logEntries = IPoeLogEntry.ParseStrings(e.Changes);
            foreach (var entry in IPoeLogEntry.ParseStrings(e.Changes).Where(log => log.IsValid)) //logEntries)
            {
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
                        _currentArea = entry.Area;
                        InteractionContainer.YouJoined(entry);
                        break;
                    case PoeLogEntryTypeEnum.SystemMessage:
                        break;
                    default:
                        Debug.Print("out of bounds");
                        break;
                }
            }
        }

        public void ManualFire()
        {
            _fileChangeMonitor.ManualFire();
        }
    }
}