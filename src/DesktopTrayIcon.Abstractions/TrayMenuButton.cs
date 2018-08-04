using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopTrayIcon.Abstractions
{
    public class TrayMenuButton : ITrayMenuButton
    {
        public string Label { get; set; }
        public EventHandler Clicked { get; set; }
    }
}
