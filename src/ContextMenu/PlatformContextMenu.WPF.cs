using System.Windows.Forms;
using ContextMenu.Abstractions;

namespace ContextMenu
{
    public class PlatformContextMenu : IPlatformContextMenu
    {
        private ContextMenuStrip _underlyingContextItems = new ContextMenuStrip();       

        void IPlatformContextMenu.AddToUnderlying(int i, IContextMenuItem newItem)
        {
            _underlyingContextItems.Items.Insert(i, ToConcreteContextItem(newItem));
        }

        void IPlatformContextMenu.ClearUnderlying()
        {
            _underlyingContextItems.Items.Clear();
        }

        void IPlatformContextMenu.MoveInUnderlying(int oldIndex, int newIndex)
        {
            ToolStripItem item = _underlyingContextItems.Items[oldIndex];
            _underlyingContextItems.Items.Remove(item);
            _underlyingContextItems.Items.Insert(newIndex, item);
        }

        void IPlatformContextMenu.RemoveFromUnderlying(int i)
        {
            _underlyingContextItems.Items.RemoveAt(i);
        }

        private ToolStripItem ToConcreteContextItem(IContextMenuItem item)
        {
            switch (item)
            {
                case IContextMenuButton button:
                    return new ToolStripMenuItem(button.Label, null, button.Clicked);
                case IContextMenuSeparator separator:
                    return new ToolStripSeparator();
                default:
                    return null;
            }
        }
    }
}
