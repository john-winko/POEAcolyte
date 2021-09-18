using System;
using System.IO;
using System.Timers;
using Timer = System.Timers.Timer;

namespace PoeAcolyte.API.Services
{
    public class FileChangeMonitor : IDisposable
    {
        private readonly Timer _timer;
        private const double DEFAULT_TIMER_INTERVAL = 2000;
        private StreamReader _streamReader;
        private string _filePath;
        public bool Running
        {
            get => _streamReader is null && _timer.Enabled;
            set => StartStreamReader(value);
        }
        
        public EventHandler<FileChangedEventArgs> FileChanged;
        
        public FileChangeMonitor(string filePath)
        {
            _filePath = filePath;
            Running = true;
            _timer = new Timer()
            {
                Interval = DEFAULT_TIMER_INTERVAL,
                Enabled = true
            };
            _timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (_streamReader.EndOfStream) return;
            var changes = _streamReader.ReadToEnd();
            FileChanged?.Invoke(this, new FileChangedEventArgs(changes));
        }

        private void StartStreamReader(bool startStream = true)
        {
            if (_streamReader is not null) return; // stream is already running
            
            if (startStream)
            {
                _streamReader = new StreamReader(File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                _streamReader.ReadToEnd();
            }
            else
            {
                _streamReader?.Dispose();
                _timer.Enabled = false;
            }
        }
        
        public void Dispose()
        {
            _timer?.Dispose();
            _streamReader?.Dispose();
        }
    }
}