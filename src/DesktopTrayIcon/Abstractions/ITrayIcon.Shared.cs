using System;
using XamTop.ContextMenu.Abstractions;

namespace XamTop.DesktopTrayIcon.Abstractions
{
    public interface ITrayIcon
    {
        /// <summary>
        /// URI to icon to display in the system tray. Should lead to a .ico file on Windows, or
        /// an image name in an Asset Bundle on macOS.
        /// </summary>
        string IconPath { get; set; }

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
        /// Context menu that appears when the tray icon is right-clicked.
        /// </summary>
        IContextMenu ContextMenu { get; set; }
    }
}
