using System;
using System.IO;
using System.Timers;
using Timer = System.Timers.Timer;

namespace PoeAcolyte.API.Services
{
    /// <summary>
    /// Class to monitor a given file for changes (additions). Triggers <see cref="FileChanged"/> if changes are found.
    /// </summary>
    public class FileChangeMonitor : IDisposable
    {
        private readonly Timer _timer;
        private const double DEFAULT_TIMER_INTERVAL = 2000;
        private StreamReader _streamReader;
        private readonly string _filePath;
        
        /// <summary>
        /// Get/Set if monitor is running 
        /// </summary>
        public bool Running
        {
            get => _streamReader is not null && _timer.Enabled;
            set => StartStreamReader(value);
        }
        /// <summary>
        /// Event handler when file is changed
        /// </summary>
        public EventHandler<FileChangedEventArgs> FileChanged;
        
        /// <summary>
        /// Constructor for class
        /// </summary>
        /// <param name="filePath">Complete path including filename of file to monitor</param>
        public FileChangeMonitor(string filePath)
        {
            _filePath = filePath;
            _timer = new Timer(DEFAULT_TIMER_INTERVAL);
            _timer.Elapsed += TimerOnElapsed;
            StartStreamReader();
        }

       private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (_streamReader.EndOfStream) return;
            try
            {
                var changes = _streamReader.ReadToEnd();
                FileChanged?.Invoke(this, new FileChangedEventArgs(changes));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void StartStreamReader(bool startStream = true)
        {
            _timer.Enabled = false;
           _streamReader?.Dispose();
            if (!startStream) return;

           try
           {
               _streamReader = new StreamReader(File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
               _streamReader.ReadToEnd();

               _timer.Enabled = true;
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
           }
        }

        private long _manualLocation = 0;
        private StreamReader _stream;
        public void ManualFire()
        {
            if (_stream is null || _stream.EndOfStream)
            {
                _stream?.Close();
                string testFilePath = @"C:\Program Files (x86)\Grinding Gear Games\Path of Exile\logs\test.txt";
                _stream = new StreamReader(File.Open(testFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            }

            var changes = _stream.ReadLine();
            FileChanged?.Invoke(this, new FileChangedEventArgs(changes));
        }
        
        /// <summary>
        /// Implementing disposal pattern
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
            _stream?.Dispose();
            _streamReader?.Dispose();
        }
    }
}