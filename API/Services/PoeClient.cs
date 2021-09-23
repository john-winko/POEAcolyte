using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace PoeAcolyte.API.Services
{
    /// <summary>
    ///     Singleton to monitor the game client and acts as mediator to focus and send keystrokes to the game
    /// </summary>
    public class PoeClient : IDisposable
    {
        private static PoeClient _instance;
        private readonly Timer _searchTimer;
        private Process _poeProcess;

        /// <summary>
        ///     Private constructor for Singleton pattern
        /// </summary>
        /// <param name="autoStart">Automatically start scanning for open game client</param>
        private PoeClient(bool autoStart = true)
        {
            _searchTimer = new Timer(5000);
            _searchTimer.Elapsed += _searchTimer_Elapsed;
            _searchTimer.Enabled = autoStart;
        }

        public bool IsGameClientOpen => _poeProcess == null;
        public bool ActiveScan => _searchTimer.Enabled;

        public void Dispose()
        {
            _poeProcess?.Dispose();
            _searchTimer?.Dispose();
        }

        /// <summary>
        ///     Factory method to get the instance of PoeClient
        /// </summary>
        /// <returns>Instance of PoeClient</returns>
        public static PoeClient GetInstance()
        {
            return _instance ??= new PoeClient();
        }

        public event EventHandler GameClientOpened;
        public event EventHandler GameClientClosed;

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
        ///     Searches Process stack for a match of:
        ///     <list type="table">
        ///         <item>PathOfExile</item>
        ///         <item>PathOfExile_x64</item>
        ///         <item>PathOfExileSteam</item>
        ///         <item>PathOfExile_x64Steam</item>
        ///         <item>PathOfExile_x64_KG.exe</item>
        ///         <item>PathOfExile_KG.exe</item>
        ///     </list>
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

        /// <summary>
        ///     Only works on windows
        /// </summary>
        /// <returns></returns>
        public bool SetFocus()
        {
            if (_poeProcess is null) return false;
            WIN32.SetForegroundWindow(_poeProcess.MainWindowHandle);
            return true;
        }

        /// <summary>
        ///     Sends string of text to game client
        ///     by setting string to clipboard then {Enter} [CTRL+V] {Enter}
        /// </summary>
        /// <param name="message">Message to be sent</param>
        /// <param name="holdSend">Do not press enter at end (useful for appending manually in game)</param>
        /// <returns></returns>
        public bool SendChatMessage(string message, bool holdSend = false)
        {
            if (!SetFocus()) return false;

            SendKeys.Flush();
            SendKeys.Send("{Enter}");
            Clipboard.SetText(message);
            SendKeys.Send("^V");
            if (!holdSend) SendKeys.Send("{Enter}");

            return true;
            // TODO add code to replace clipboard contents
        }

        /// <summary>
        ///     Sends string of text to game client
        ///     by setting string to clipboard then {Enter} [CTRL+V] {Enter}
        /// </summary>
        /// <param name="messages">Message to be sent</param>
        /// <returns></returns>
        public bool SendChatMessages(string[] messages)
        {
            if (!SetFocus()) return false;
            foreach (var message in messages)
            {
                SendKeys.Flush();
                SendKeys.Send("{Enter}");
                Clipboard.SetText(message);
                SendKeys.Send("^V");
                SendKeys.Send("{Enter}");
            }


            return true;
            // TODO add code to replace clipboard contents
        }
    }
}