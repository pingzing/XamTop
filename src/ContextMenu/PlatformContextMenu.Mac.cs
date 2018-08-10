using System;
using AppKit;
using ContextMenu.Abstractions;

namespace ContextMenu
{
    public class PlatformContextMenu : IPlatformContextMenu
    {
        private NSMenu _underlyingMenu = new NSMenu();

        public void AddToUnderlying(int i, IContextMenuItem newItem)
        {
            AddToUnderlyingSpecific(_underlyingMenu, i, newItem);
        }

        private void AddToUnderlyingSpecific(NSMenu menu, nint i, IContextMenuItem newItem)
        {
            _underlyingMenu.InsertItem(ToConcreteMenuItem(newItem), i);
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
            _underlyingMenu.RemoveAllItems();
        }

        public void MoveInUnderlying(int oldIndex, int newIndex)
        {
            NSMenuItem item = _underlyingMenu.ItemAt(oldIndex);
            _underlyingMenu.RemoveItem(item);
            _underlyingMenu.InsertItem(item, newIndex);
        }

        public void RemoveFromUnderlying(int i)
        {
            _underlyingMenu.RemoveItemAt(i);
        }

        public void SetLabel(string label)
        {
            _underlyingMenu.Title = label;
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
