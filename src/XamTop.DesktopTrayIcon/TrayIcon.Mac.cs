using System;
using AppKit;
using XamTop.ContextMenu.Abstractions;

namespace XamTop.DesktopTrayIcon
{
    public partial class TrayIcon
    {
        private NSStatusItem _trayIcon;

        public TrayIcon()
        {
            NSStatusBar bar = new NSStatusBar();
            _trayIcon = bar.CreateStatusItem(NSStatusItemLength.Square);
            _trayIcon.Behavior = NSStatusItemBehavior.RemovalAllowed;
        }

        private string _iconPath;
        public string PlatformIconPath
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

        public string PlatformTrayTooltip
        {
            get => _trayIcon.Button.ToolTip;
            set => _trayIcon.Button.ToolTip = value;
        }
        
        private ContextMenu.ContextMenu _contextMenu;
        public IContextMenu PlatformContextMenu
        {
            get => _contextMenu;
            set
            {
                _contextMenu = (ContextMenu.ContextMenu)value;
                _trayIcon.Menu = _contextMenu.UnderlyingContextMenu;
            }
        }

        public event EventHandler PlatformClick
        {
            add { _trayIcon.Button.Activated += value; }
            remove { _trayIcon.Button.Activated -= value; }
        }

        public void PlatformShow()
        {
            _trayIcon.Visible = true;
            _trayIcon.Length = new nfloat((int)NSStatusItemLength.Square);
        }

        public void PlatformHide()
        {
            _trayIcon.Visible = false;
            _trayIcon.Length = 0;
        }

        public void PlatformShowContextMenu()
        {
            _trayIcon.PopUpStatusItemMenu(_trayIcon.Menu);
        }

        public void PlatformHideContextMenu()
        {
            _trayIcon.Menu.CancelTracking();
        }

    }
}
