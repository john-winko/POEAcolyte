using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
// ReSharper disable InconsistentNaming

namespace PoeAcolyte.API
{
    public static class WIN32
    {
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TRANSPARENT = 0x20;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        // Activate an application window.
        [DllImport("USER32.DLL")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        public static void SetFocus(this Process process)
        {
            SetForegroundWindow(process.MainWindowHandle);
        }

        public static void MakeFormTransparent(this Form form)
        {
            var style = GetWindowLong(form.Handle, GWL_EXSTYLE);
            var change = SetWindowLong(form.Handle, GWL_EXSTYLE, style | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }

        public static (Point, Size) GetBounds(this Process process)
        {
            RECT rect;
            if (!GetWindowRect(new HandleRef(process, process.MainWindowHandle), out rect))
                return (new Point(0, 0), new Size(0, 0));
            return (new Point(rect.Left + 7, rect.Top),  new Size(rect.Right - rect.Left - 14, rect.Bottom - rect.Top - 7));

        }
    }
}