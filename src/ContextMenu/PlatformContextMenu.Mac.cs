using AppKit;
using ContextMenu.Abstractions;

namespace ContextMenu
{
    public class PlatformContextMenu : IPlatformContextMenu
    {
        private NSMenu _underlyingMenu = new NSMenu();

        void IPlatformContextMenu.AddToUnderlying(int i, IContextMenuItem newItem)
        {
            _underlyingMenu.InsertItem(ToConcreteMenuItem(newItem), i);
        }

        void IPlatformContextMenu.ClearUnderlying()
        {
            _underlyingMenu.RemoveAllItems();
        }

        void IPlatformContextMenu.MoveInUnderlying(int oldIndex, int newIndex)
        {
            NSMenuItem item = _underlyingMenu.ItemAt(oldIndex);
            _underlyingMenu.RemoveItem(item);
            _underlyingMenu.InsertItem(item, newIndex);
        }

        void IPlatformContextMenu.RemoveFromUnderlying(int i)
        {
            _underlyingMenu.RemoveItemAt(i);
        }

        private NSMenuItem ToConcreteMenuItem(IContextMenuItem item)
        {
            switch (item)
            {
                case IContextMenuButton button:
                    return new NSMenuItem(button.Label, button.Clicked);
                case IContextMenuSeparator separator:
                    return NSMenuItem.SeparatorItem;
                default: return null;
            }
        }
    }
}
