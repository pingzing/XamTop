using System;
using System.Collections.Generic;

namespace DesktopTrayIcon.Abstractions
{
    public interface ITrayIcon
    {
        /// <summary>
        /// URI to icon to display in the system tray. Should lead to a .ico file on Windows, and a .png file on macOS.
        /// </summary>
        string IconUri { get; set; }

        /// <summary>
        /// Tooltip to display when the user hovers over the tray icon.
        /// </summary>
        string TrayTooltip { get; set; }

        /// <summary>
        /// Occurs when the user clicks the tray icon.
        /// </summary>
        event EventHandler Click;

        /// <summary>
        /// Show the tray icon.
        /// </summary>
        void Show();

        /// <summary>
        /// Hide the tray icon.
        /// </summary>
        void Hide();

        /// <summary>
        /// Display the tray icon's context menu.
        /// </summary>
        void ShowContextMenu();

        /// <summary>
        /// Hide the tray icon's context menu.
        /// </summary>
        void HideContextMenu();

        /// <summary>
        /// List of context menu items that appear when the tray icon is right-clicked.
        /// Replace the list with a new instance to change the items.
        /// </summary>
        IReadOnlyList<ITrayMenuItem> ContextMenuItems { get; set; }
    }
}
