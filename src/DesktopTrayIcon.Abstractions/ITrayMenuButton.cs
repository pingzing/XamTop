using System;

namespace DesktopTrayIcon.Abstractions
{
    public interface ITrayMenuButton : ITrayMenuItem
    {
        /// <summary>
        /// The text to display for the context menu item.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Occurs when the user clicks this tray menu item.
        /// </summary>
        EventHandler Clicked { get; set; }
    }
}
