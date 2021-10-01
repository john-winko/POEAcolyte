using PoeAcolyte.UI.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace PoeAcolyte
{
    [Serializable]
    public class AppSettings
    {
        public List<UiSettings> UiSettingsList { get; set; } = new();

        public string ClientPath { get; set; } = @"C:\Program Files (x86)\Grinding Gear Games\Path of Exile\logs\Client.txt";
        private static AppSettings _instance;

        public static AppSettings Instance
        {
            get
            {
                if (_instance is not null) return _instance;
                try
                {
                    // TODO add better error handling
                    _instance = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText("test.json"));

                    if (_instance is null) return new AppSettings();

                    _instance.UiSettingsList.ForEach(p=>p.GenerateFrame());
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
                return _instance;
            }
            set => _instance = value;
        }

        /// <summary>
        ///     DO NOT USE
        /// </summary>
        public AppSettings()
        {

        }

        public static void UpdateControlLocation(Control control)
        {
            var result = Instance.UiSettingsList.Find(p => p.Description == control.Name);
            if (result is null)
            {
                // create a new one if it doesn't exist
                result = new UiSettings()
                {
                    Description = control.Name,
                    Location = control.Location,
                    Size = control.Size
                };
                result.GenerateFrame();
                Instance.UiSettingsList.Add(result);
            }
            control.Location = result.Location;
            control.Size = result.Size;
        }

        public static void UpdateControlLocation(Control[] controls)
        {
            foreach (var control in controls) 
                UpdateControlLocation(control);
        }

        public static void StartEdit(Control.ControlCollection owner, Control control)
        {
            var result = Instance.UiSettingsList.Find(p => p.Description == control.Name);

            if (result is null || owner.Contains(result.Frame)) return;

            owner.Add(result.Frame);

            result.Frame.BringToFront();
            result.Frame.Visible = true;
        }

        public static void StartEdit(Control.ControlCollection owner, Control[] controls)
        {
            foreach (var control in controls)
                StartEdit(owner, control);
        }


        public static void StopEdit(Control.ControlCollection owner, Control control)
        {
            var result = Instance.UiSettingsList.Find(p => p.Description == control.Name);

            if (result is null) return;

            result.Location = result.Frame.Location;
            result.Size = result.Frame.Size;
            control.Location = result.Frame.Location;
            control.Size = result.Frame.Size;
            
            owner.Remove(result.Frame);
        }

        public static void StopEdit(Control.ControlCollection owner, Control[] controls)
        {
            foreach (var control in controls)
                StopEdit(owner, control);
        }


        public UiSettings GetUiSettings(string description)
        {
            return UiSettingsList.Find(p => p.Description == description);
        }

        public void Save()
        {
            var s = JsonSerializer.Serialize(this);
            File.WriteAllText("test.json", s);
        }
    }

    public class UiSettings
    {
        public string Description { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }
        [JsonIgnore] public FrameControl Frame { get; private set; }

        /// <summary>
        ///     Needed due to json ignore
        /// </summary>
        public void GenerateFrame()
        {
            if (Frame is not null) return;
            Frame = new FrameControl(Description, Location, Size);
            Frame.ClickHandler += (sender, args) =>
            {
                var mouseArgs = (MouseEventArgs) args;
                if (mouseArgs.Button == MouseButtons.Right)
                    Frame.Visible = false;
            };
        }
    }

}
