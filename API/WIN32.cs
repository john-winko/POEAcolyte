using System;
using System.Runtime.InteropServices;

namespace PoeAcolyte.API
{
    public static class WIN32
    {
        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}