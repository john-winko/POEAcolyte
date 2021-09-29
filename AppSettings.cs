using System;
using System.Drawing;

namespace PoeAcolyte
{
    [Serializable]
    public class AppSettings
    {
        private UserInterfaceRegion r = new UserInterfaceRegion();

        public void Load()
        {

        }

        public void Save()
        {

        }
    }

    public struct UserInterfaceRegion
    {
        public UserInterfaceRegion(Point location, Size size)
        {
            Location = location;
            Size = size;
        }

        public Point Location { get; set; }
        public Size Size { get; set; }
        
    }


}
