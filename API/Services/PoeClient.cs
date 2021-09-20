using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace PoeAcolyte.API.Services
{
    public class PoeClient
    {
        private Process _poeProcess;
        private readonly Timer _searchTimer;
        public event EventHandler GameClientOpened;
        public event EventHandler GameClientClosed;
        public bool IsGameClientOpen => _poeProcess == null;
        public bool ActiveScan => _searchTimer.Enabled;

        public PoeClient(bool autoStart = true)
        {
            _searchTimer = new Timer(5000);
            _searchTimer.Elapsed += _searchTimer_Elapsed;
            _searchTimer.Enabled = autoStart;
        }

        private void _searchTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _poeProcess = GetPoeProcess();
            if (_poeProcess == null) return;
            _poeProcess.EnableRaisingEvents = true;

            // go back to searching for poe if it is closed
            _poeProcess.Exited += (o, args) =>
            {
                _searchTimer.Enabled = true;
                GameClientClosed?.Invoke(this, null);
                Debug.Print("Client is closed");
            };

            // stop searching if game client found
            _searchTimer.Enabled = false;
            GameClientOpened?.Invoke(this, null);
            Debug.Print("Client is opened");
        }

        /// <summary>
        /// Searches Process stack for a match of:
        /// <list type="table">
        /// <item>PathOfExile</item>
        /// <item>PathOfExile_x64</item>
        /// <item>PathOfExileSteam</item>
        /// <item>PathOfExile_x64Steam</item>
        /// <item>PathOfExile_x64_KG.exe</item>
        /// <item>PathOfExile_KG.exe</item>
        /// </list>
        /// </summary>
        /// <returns>Process if program found, null if not</returns>
        public static Process GetPoeProcess()
        {
            var result = (from proc in Process.GetProcesses() 
                where proc.ProcessName is 
                    "PathOfExile" or 
                    "PathOfExile_x64" or 
                    "PathOfExileSteam" or
                    "PathOfExile_x64Steam" or 
                    "PathOfExile_x64_KG.exe" or 
                    "PathOfExile_KG.exe"
                select proc)
                .ToList();

            return result.Any() ? result.First() : null;
        }
    }
}
