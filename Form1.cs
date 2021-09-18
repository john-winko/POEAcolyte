using System.Windows.Forms;
using PoeAcolyte.API;
using PoeAcolyte.API.Services;

namespace PoeAcolyte
{
    public partial class Form1 : Form
    {
        private Button _startButton;
        private Button _stopButton;
        private TextBox _textBox;
        private const string POEPATH = @"C:\Program Files (x86)\Grinding Gear Games\Path of Exile\logs\Client.txt";
        private FileChangeMonitor _monitor;
        
        public Form1()
        {
            InitializeComponent();
            InitControls();
            _monitor = new FileChangeMonitor(POEPATH);
            _monitor.FileChanged += FileChanged;
            
        }

        private void FileChanged(object sender, FileChangedEventArgs e)
        {
            _textBox.PerformSafely(()=> _textBox.Text += e.Changes);
        }

        private void InitControls()
        {
            _startButton = new Button()
            {
                Left = 10,
                Top = 10,
                Width = 200,
                Height = 50,
                Text = "Start"
            };
            _stopButton = new Button()
            {
                Left = 10,
                Top = 80,
                Width = 200,
                Height = 50,
                Text = "Stop"
            };
            _textBox = new TextBox()
            {
                Left = 210,
                Top = 10,
                Width = 500,
                Height = 500,
                Text = "",
                Multiline = true
            };
            Controls.Add(_startButton);
            Controls.Add(_stopButton);
            Controls.Add(_textBox);
        }
    }
}