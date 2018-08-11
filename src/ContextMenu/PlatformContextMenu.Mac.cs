using System;
using AppKit;
using XamTop.ContextMenu.Abstractions;

namespace XamTop.ContextMenu
{
    public class PlatformContextMenu : IPlatformContextMenu
    {
        public NSMenu UnderlyingMenu { get; private set; } = new NSMenu();

        public void AddToUnderlying(int i, IContextMenuItem newItem)
        {
            AddToUnderlyingSpecific(UnderlyingMenu, i, newItem);
        }

        private void AddToUnderlyingSpecific(NSMenu menu, nint i, IContextMenuItem newItem)
        {
            UnderlyingMenu.InsertItem(ToConcreteMenuItem(newItem), i);
            if (newItem is IContextMenu subMenu)
            {
                NSMenu platformSubMenu = new NSMenu();
                foreach (var subMenuItem in subMenu.ItemsSource)
                {
                    nint subMenuIndex = 0;
                    AddToUnderlyingSpecific(platformSubMenu, subMenuIndex, subMenuItem);
                }
            }
        }

        public void ClearUnderlying()
        {
            UnderlyingMenu.RemoveAllItems();
        }

        public void MoveInUnderlying(int oldIndex, int newIndex)
        {
            NSMenuItem item = UnderlyingMenu.ItemAt(oldIndex);
            UnderlyingMenu.RemoveItem(item);
            UnderlyingMenu.InsertItem(item, newIndex);
        }

        public void RemoveFromUnderlying(int i)
        {
            UnderlyingMenu.RemoveItemAt(i);
        }

        public void SetLabel(string label)
        {
            UnderlyingMenu.Title = label;
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
