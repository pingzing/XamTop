using System;
using AppKit;
using XamTop.ContextMenu.Abstractions;

namespace XamTop.ContextMenu
{
    public partial class ContextMenu
    {
        public NSMenu UnderlyingContextMenu { get; private set; } = new NSMenu();

        public void PlatformAdd(int i, IContextMenuItem newItem)
        {
            PlatformAddSpecific(UnderlyingContextMenu, i, newItem);
        }

        private void PlatformAddSpecific(NSMenu menu, nint i, IContextMenuItem newItem)
        {
            UnderlyingContextMenu.InsertItem(ToConcreteMenuItem(newItem), i);
            if (newItem is IContextMenu subMenu)
            {
                NSMenu platformSubMenu = new NSMenu();
                foreach (var subMenuItem in subMenu.ItemsSource)
                {
                    nint subMenuIndex = 0;
                    PlatformAddSpecific(platformSubMenu, subMenuIndex, subMenuItem);
                }
            }
        }

        public void PlatformClear()
        {
            UnderlyingContextMenu.RemoveAllItems();
        }

        public void PlatformMove(int oldIndex, int newIndex)
        {
            NSMenuItem item = UnderlyingContextMenu.ItemAt(oldIndex);
            UnderlyingContextMenu.RemoveItem(item);
            UnderlyingContextMenu.InsertItem(item, newIndex);
        }

        public void PlatformRemove(int i)
        {
            UnderlyingContextMenu.RemoveItemAt(i);
        }

        public void PlatformSetLabel(string label)
        {
            UnderlyingContextMenu.Title = label;
        }

        private NSMenuItem ToConcreteMenuItem(IContextMenuItem item)
        {
            switch (item)
            {
                case IContextMenuButton button:
                    return new NSMenuItem(button.Label, button.Clicked);
                case IContextMenuSeparator separator:
                    return NSMenuItem.SeparatorItem;
                case IContextMenu subMenu:
                    return new NSMenuItem(subMenu.Label);
                default: return null;
            }
        }
    }
}
