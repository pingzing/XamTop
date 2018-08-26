using System;
using XamTop.ContextMenu.Abstractions;

namespace XamTop.DesktopTrayIcon
{
    public partial class TrayIcon
    {
        /// <summary>
        /// URI to icon to display in the system tray. Should lead to a .ico file on Windows, or
        /// an image name in an Asset Bundle on macOS.
        /// </summary>
        public string IconPath
        {
            get => PlatformIconPath;
            set => PlatformIconPath = value;
        }

        /// <summary>
        /// Tooltip to display when the user hovers over the tray icon.
        /// </summary>
        public string TrayTooltip
        {
            get => PlatformTrayTooltip;
            set => PlatformTrayTooltip = value;
        }

        /// <summary>
        /// Occurs when the user clicks the tray icon.
        /// </summary>
        public event EventHandler Click
        {
            add => PlatformClick += value;
            remove => PlatformClick -= value;
        }

        /// <summary>
        /// Show the tray icon.
        /// </summary>
        public void Show() => PlatformShow();

        /// <summary>
        /// Hide the tray icon.
        /// </summary>
        public void Hide() => PlatformHide();

        /// <summary>
        /// Display the tray icon's context menu.
        /// </summary>
        public void ShowContextMenu() => PlatformShowContextMenu();

        /// <summary>
        /// Hide the tray icon's context menu.
        /// </summary>
        public void HideContextMenu() => PlatformHideContextMenu();

        /// <summary>
        /// Context menu that appears when the tray icon is right-clicked.
        /// </summary>
        IContextMenu ContextMenu
        {
            get => PlatformContextMenu;
            set => PlatformContextMenu = value;
        }
    }
}
