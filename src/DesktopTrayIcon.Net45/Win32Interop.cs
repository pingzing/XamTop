using System;
using System.Runtime.InteropServices;

namespace DesktopTrayIcon.Net45
{
    // Keeping this around, because I'd really like to figure out a way to hook into the host application's OnClose somehow, and run RefreshTrayArea.
    internal static class Win32Interop
    {
        private const uint WM_MOUSEMOVE = 0x0200;

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        internal static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hwnd, uint msg, int character, IntPtr lpsText);

        /// <summary>
        /// Forces the tray area to refresh, to clear out stale icons.
        /// </summary>
        internal static void RefreshTrayArea()
        {
            // TODO: Can window names we search for with the lpszWindow parameter change per-locale?
            IntPtr systemTrayContainerHandle = FindWindow("Shell_TrayWnd", null);
            IntPtr systemTrayHandle = FindWindowEx(systemTrayContainerHandle, IntPtr.Zero, "TrayNotifyWnd", null);
            IntPtr sysPagerHandle = FindWindowEx(systemTrayHandle, IntPtr.Zero, "SysPager", null);
            IntPtr notificationAreaHandle = FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", "Notification Area");
            if (notificationAreaHandle == IntPtr.Zero)
            {
                notificationAreaHandle = FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", "User Promoted Notification Area");
                IntPtr notifyIconOverflowWindowHandle = FindWindow("NotifyIconOverflowWindow", null);
                IntPtr overflowNotificationAreaHandle = FindWindowEx(notifyIconOverflowWindowHandle, IntPtr.Zero, "ToolbarWindow32", "Overflow Notification Area");
                RefreshTray(overflowNotificationAreaHandle);
            }
            RefreshTray(notificationAreaHandle);
        }

        // Sends mousemove message to every spot in the systray and its overflow window, forcing stale icons to clear out.
        private static void RefreshTray(IntPtr windowHandle)
        {
            GetClientRect(windowHandle, out RECT rect);
            for (var x = 0; x < rect.right; x += 5)
            {
                for (var y = 0; y < rect.bottom; y += 5)
                {
                    SendMessage(windowHandle, WM_MOUSEMOVE, 0, new IntPtr((y << 16) + x));
                }
            }
        }
    }
}
