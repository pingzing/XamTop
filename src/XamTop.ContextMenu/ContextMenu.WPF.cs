using System.Linq;
using System.Windows.Forms;
using XamTop.ContextMenu.Abstractions;

namespace XamTop.ContextMenu
{
    public partial class ContextMenu
    {
        private ToolStripMenuItem _parentItem;
        public ContextMenuStrip UnderlyingContextMenu { get; private set; } = new ContextMenuStrip();

        private void PlatformAdd(int i, IContextMenuItem newItem)
        {
            UnderlyingContextMenu.Items.Insert(i, ToConcreteContextItem(newItem));
            if (_parentItem != null)
            {
                _parentItem.DropDownItems.Insert(i, ToConcreteContextItem(newItem));
            }
        }

        private void PlatformClear()
        {
            UnderlyingContextMenu.Items.Clear();
            if (_parentItem != null)
            {
                _parentItem.DropDownItems.Clear();
            }
        }

        private void PlatformMove(int oldIndex, int newIndex)
        {
            ToolStripItem item = UnderlyingContextMenu.Items[oldIndex];
            UnderlyingContextMenu.Items.Remove(item);
            UnderlyingContextMenu.Items.Insert(newIndex, item);
            if (_parentItem != null)
            {
                ToolStripItem subItem = _parentItem.DropDownItems[oldIndex];
                _parentItem.DropDownItems.Remove(subItem);
                _parentItem.DropDownItems.Insert(newIndex, subItem);
            }
        }

        private void PlatformRemove(int i)
        {
            UnderlyingContextMenu.Items.RemoveAt(i);
            if (_parentItem != null)
            {
                _parentItem.DropDownItems.RemoveAt(i);
            }
        }

        private void PlatformSetLabel(string label)
        {
            UnderlyingContextMenu.Text = label;
        }

        private ToolStripItem ToConcreteContextItem(IContextMenuItem item)
        {
            switch (item)
            {
                case IContextMenuButton button:
                    return new ToolStripMenuItem(button.Label, null, button.Clicked);
                case IContextMenuSeparator separator:
                    return new ToolStripSeparator();
                case IContextMenu menu:
                    var convertedSubItems = menu.ItemsSource.Select(ToConcreteContextItem);                    
                    var subMenu = new ToolStripMenuItem(menu.Label);
                    subMenu.DropDownItems.AddRange(convertedSubItems.ToArray());
                    ContextMenu subMenuPlatformMenu = (ContextMenu)menu;
                    subMenuPlatformMenu._parentItem = subMenu;
                    return subMenu;
                default:
                    return null;
            }
        }
    }
}
