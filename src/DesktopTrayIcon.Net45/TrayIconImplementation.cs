using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DesktopTrayIcon.Abstractions;
using DesktopTrayIcon.Net45;

// In the same namespace as the core plugin class, so the TrayIconImplementation() constructor is visible to it.
namespace DesktopTrayIcon
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

        private IImmutableList<ITrayMenuItem> _contextMenuItems = ImmutableList.Create<ITrayMenuItem>();
        public IReadOnlyList<ITrayMenuItem> ContextMenuItems
        {
            get => _contextMenuItems;
            set
            {
                _contextMenuItems = value.ToImmutableList();
                _trayIcon.ContextMenuStrip.Items.Clear();
                if (value != null && value.Count > 0)
                {
                    _trayIcon.ContextMenuStrip.Items
                        .AddRange(value.Select<ITrayMenuItem, ToolStripItem>(x => 
                        {
                            switch (x)
                            {
                                case ITrayMenuButton button:
                                    return new ToolStripMenuItem(button.Label, null, button.Clicked);
                                case ITrayMenuSeparator separator:
                                    return new ToolStripSeparator();
                                default:
                                    return null;
                            }
                        })
                        .Where(x => x != null)
                        .ToArray());
                }
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
            Win32Interop.RefreshTrayArea(); 
        }
    }
}
