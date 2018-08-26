using System;
using System.Reflection;
using System.Windows.Forms;
using XamTop.ContextMenu.Abstractions;

namespace XamTop.DesktopTrayIcon
{
    public partial class TrayIcon
    {
        private NotifyIcon _trayIcon = new NotifyIcon
        {
            ContextMenuStrip = new ContextMenuStrip()
        };

        public event EventHandler PlatformClick
        {
            add { _trayIcon.Click += value; }
            remove { _trayIcon.Click -= value; }
        }

        public string PlatformTrayTooltip
        {
            get => _trayIcon.Text;
            set => _trayIcon.Text = value;
        }

        private string _iconUri;
        public string PlatformIconPath
        {
            get => _iconUri;
            set
            {
                _iconUri = value;
                _trayIcon.Icon = new System.Drawing.Icon(value);
            }
        }

        private ContextMenu.ContextMenu _contextMenu;
        public IContextMenu PlatformContextMenu
        {
            get => _contextMenu;
            set
            {
                _contextMenu = (ContextMenu.ContextMenu)value;
                _trayIcon.ContextMenuStrip = _contextMenu.UnderlyingContextMenu;
            }
        }

        public void PlatformHide()
        {
            _trayIcon.Visible = false;
        }

        public void PlatformShow()
        {
            _trayIcon.Visible = true;
        }

        // We're sneaking in and using the internal show method, because the public methods require more ceremony than is really necessary for our use case
        private MethodInfo _internalShowMethod;
        public void PlatformShowContextMenu()
        {
            if (_internalShowMethod == null)
            {
                _internalShowMethod = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            }
            _internalShowMethod.Invoke(_trayIcon, null);
        }

        public void PlatformHideContextMenu()
        {
            _trayIcon.ContextMenuStrip.Hide();

            // The status tray sometimes holds onto stale icons until they're moused over, so let's force it to clear.
            PlatformInterop.RefreshTrayArea();
        }
    }
}
