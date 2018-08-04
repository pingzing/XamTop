using System;
using System.Collections.Generic;
using DesktopTrayIcon.Abstractions;
using AppKit;
using ObjCRuntime;

namespace DesktopTrayIcon.Mac
{
    public class TrayIconImplementation : ITrayIcon
    {
        private NSStatusItem _trayIcon;

        public TrayIconImplementation()
        {
            NSStatusBar bar = new NSStatusBar();
            _trayIcon = bar.CreateStatusItem(NSStatusItemLength.Square);
        }

        public string IconUri 
        {
            get 
            {
                
            }
            set 
            {
                
            }
        }

        public string TrayTooltip 
        {
            get => _trayIcon.Button.ToolTip;
            set => _trayIcon.Button.ToolTip = value;
        }

        public IReadOnlyList<ITrayMenuItem> ContextMenuItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler Click 
        {
            add { _trayIcon.Button.Activated += value; }
            remove { _trayIcon.Button.Activated -= value; }
        }
        
        public void Show()
        {
            _trayIcon.Visible = true;
        }

        public void Hide()
        {
            _trayIcon.Visible = false;
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
