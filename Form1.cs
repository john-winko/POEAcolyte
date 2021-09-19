using System;
using System.Windows.Forms;
using PoeAcolyte.API;
using PoeAcolyte.API.Parsers;
using PoeAcolyte.API.Services;

namespace PoeAcolyte
{
    public partial class Form1 : Form
    {
        private Button _startButton;
        private Button _stopButton;
        private TextBox _textBox;
        private PoeBroker _broker;
        
        public Form1()
        {
            
            InitializeComponent();
            _broker = new PoeBroker();
            InitControls();
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
            _startButton.Click += (sender, args) =>
            {
                _broker.ManualFire();
            };
            _stopButton = new Button()
            {
                Left = 10,
                Top = 80,
                Width = 200,
                Height = 50,
                Text = "Stop"
            };
            _stopButton.Click += (sender, args) =>
            {
                _broker.Running = false;
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