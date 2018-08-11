using System;
using System.Reflection;
using System.Windows.Forms;
using XamTop.ContextMenu;
using XamTop.ContextMenu.Abstractions;
using XamTop.DesktopTrayIcon.Abstractions;

namespace XamTop.DesktopTrayIcon
{
    public class TrayIconImplementation : ITrayIcon
    {
        private NotifyIcon _trayIcon = new NotifyIcon
        {
            ContextMenuStrip = new ContextMenuStrip()
        };

        public event EventHandler Click
        {
            add { _trayIcon.Click += value; }
            remove { _trayIcon.Click -= value; }
        }

        public string TrayTooltip
        {
            get => _trayIcon.Text;
            set => _trayIcon.Text = value;
        }

        private string _iconUri;
        public string IconPath
        {
            get => _iconUri;
            set
            {
                _iconUri = value;
                _trayIcon.Icon = new System.Drawing.Icon(value);
            }
        }

        private ContextMenuFacade _contextMenu;
        public IContextMenu ContextMenu
        {
            get => _contextMenu;
            set
            {
                _contextMenu = (ContextMenu.ContextMenuFacade)value;
                _trayIcon.ContextMenuStrip = ((PlatformContextMenu)_contextMenu.PlatformContextMenu).UnderlyingContextMenu;
            }
        }

        public void Hide()
        {
            _trayIcon.Visible = false;
        }

        public void Show()
        {
            _trayIcon.Visible = true;
        }

        // We're sneaking in and using the internal show method, because the public methods require more ceremony than is really necessary for our use case
        private MethodInfo _internalShowMethod;

        public void ShowContextMenu()
        {
            if (_internalShowMethod == null)
            {
                _internalShowMethod = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            }
            _internalShowMethod.Invoke(_trayIcon, null);
        }

        public void HideContextMenu()
        {
            _trayIcon.ContextMenuStrip.Hide();

            // The status tray sometimes holds onto stale icons until they're moused over, so let's force it to clear.
            PlatformInterop.RefreshTrayArea();
        }
    }
}
