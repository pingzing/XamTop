using System;
using AppKit;
using XamTop.ContextMenu;
using XamTop.ContextMenu.Abstractions;
using XamTop.DesktopTrayIcon.Abstractions;

namespace XamTop.DesktopTrayIcon
{
    public class TrayIconImplementation : ITrayIcon
    {
        private NSStatusItem _trayIcon;

        public TrayIconImplementation()
        {
            NSStatusBar bar = new NSStatusBar();
            _trayIcon = bar.CreateStatusItem(NSStatusItemLength.Square);
            _trayIcon.Behavior = NSStatusItemBehavior.RemovalAllowed;
        }

        private string _iconPath;
        public string IconPath
        {
            get => _iconPath;
            set
            {
                _iconPath = value;
                NSImage icon = NSImage.ImageNamed(value);
                icon.Template = true;
                _trayIcon.Image = icon;
                _trayIcon.HighlightMode = true;
            }
        }

        public string TrayTooltip
        {
            get => _trayIcon.Button.ToolTip;
            set => _trayIcon.Button.ToolTip = value;
        }
        
        private ContextMenuFacade _contextMenu;
        public IContextMenu ContextMenu
        {
            get => _contextMenu;
            set
            {
                _contextMenu = (ContextMenuFacade)value;
                _trayIcon.Menu = ((PlatformContextMenu)_contextMenu.PlatformContextMenu).UnderlyingMenu;
            }
        }

        public event EventHandler Click
        {
            add { _trayIcon.Button.Activated += value; }
            remove { _trayIcon.Button.Activated -= value; }
        }

        public void Show()
        {
            _trayIcon.Visible = true;
            _trayIcon.Length = new nfloat((int)NSStatusItemLength.Square);
        }

        public void Hide()
        {
            _trayIcon.Visible = false;
            _trayIcon.Length = 0;
        }

        public void ShowContextMenu()
        {
            _trayIcon.PopUpStatusItemMenu(_trayIcon.Menu);
        }

        public void HideContextMenu()
        {
            _trayIcon.Menu.CancelTracking();
        }

    }
}
