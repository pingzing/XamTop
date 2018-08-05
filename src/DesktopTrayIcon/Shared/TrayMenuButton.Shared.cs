using System;

namespace DesktopTrayIcon.Abstractions
{
    public class TrayMenuButton : ITrayMenuButton
    {
        public string Label { get; set; }
        public EventHandler Clicked { get; set; }
    }
}
