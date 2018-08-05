using System;
using System.Collections.Generic;
using DesktopTrayIcon.Abstractions;
using AppKit;
using System.Collections.Immutable;
using System.Linq;

namespace DesktopTrayIcon
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

        private ImmutableList<ITrayMenuItem> _contextMenuItems = ImmutableList.Create<ITrayMenuItem>();
        public IReadOnlyList<ITrayMenuItem> ContextMenuItems 
        {
            get => _contextMenuItems;
            set 
            {
                _contextMenuItems = value.ToImmutableList();
                if (value == null || value.Count == 0)
                {
                    _trayIcon.Menu = null;
                    return;
                }

                NSMenu menu = new NSMenu();
                var nsMenuItems = value.Select(x =>
                {
                    switch (x)
                    {
                        case ITrayMenuButton button:
                            return new NSMenuItem(button.Label, button.Clicked);
                        case ITrayMenuSeparator separator:
                            return NSMenuItem.SeparatorItem;
                        default: return null;
                    }
                })
                .Where(x => x != null);
                foreach (var item in nsMenuItems)
                {
                    menu.AddItem(item);
                }

                _trayIcon.Menu = menu;
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
