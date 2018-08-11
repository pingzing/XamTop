using System.Linq;
using System.Windows.Forms;
using XamTop.ContextMenu.Abstractions;

namespace XamTop.ContextMenu
{
    public class PlatformContextMenu : IPlatformContextMenu
    {
        public ToolStripMenuItem ParentItem { get; set; }
        public ContextMenuStrip UnderlyingContextMenu { get; private set; } = new ContextMenuStrip();

        public void AddToUnderlying(int i, IContextMenuItem newItem)
        {
            UnderlyingContextMenu.Items.Insert(i, ToConcreteContextItem(newItem));
            if (ParentItem != null)
            {
                ParentItem.DropDownItems.Insert(i, ToConcreteContextItem(newItem));
            }
        }

        public void ClearUnderlying()
        {
            UnderlyingContextMenu.Items.Clear();
            if (ParentItem != null)
            {
                ParentItem.DropDownItems.Clear();
            }
        }

        public void MoveInUnderlying(int oldIndex, int newIndex)
        {
            ToolStripItem item = UnderlyingContextMenu.Items[oldIndex];
            UnderlyingContextMenu.Items.Remove(item);
            UnderlyingContextMenu.Items.Insert(newIndex, item);
            if (ParentItem != null)
            {
                ToolStripItem subItem = ParentItem.DropDownItems[oldIndex];
                ParentItem.DropDownItems.Remove(subItem);
                ParentItem.DropDownItems.Insert(newIndex, subItem);
            }
        }

        public void RemoveFromUnderlying(int i)
        {
            UnderlyingContextMenu.Items.RemoveAt(i);
            if (ParentItem != null)
            {
                ParentItem.DropDownItems.RemoveAt(i);
            }
        }

        public void SetLabel(string label)
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
                    PlatformContextMenu subMenuPlatformMenu = (PlatformContextMenu)((ContextMenuFacade)menu).PlatformContextMenu;
                    subMenuPlatformMenu.ParentItem = subMenu;
                    return subMenu;
                default:
                    return null;
            }
        }
    }
}
