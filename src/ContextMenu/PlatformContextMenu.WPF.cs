using System.Linq;
using System.Windows.Forms;
using ContextMenu.Abstractions;

namespace ContextMenu
{
    internal class PlatformContextMenu : IPlatformContextMenu
    {
        private ContextMenuStrip _underlyingContextMenu = new ContextMenuStrip();        

        void IPlatformContextMenu.AddToUnderlying(int i, IContextMenuItem newItem)
        {            
            _underlyingContextMenu.Items.Insert(i, ToConcreteContextItem(newItem));
        }

        void IPlatformContextMenu.ClearUnderlying()
        {
            _underlyingContextMenu.Items.Clear();            
        }

        void IPlatformContextMenu.MoveInUnderlying(int oldIndex, int newIndex)
        {
            ToolStripItem item = _underlyingContextMenu.Items[oldIndex];
            _underlyingContextMenu.Items.Remove(item);
            _underlyingContextMenu.Items.Insert(newIndex, item);
        }

        void IPlatformContextMenu.RemoveFromUnderlying(int i)
        {
            _underlyingContextMenu.Items.RemoveAt(i);
        }

        public void SetLabel(string label)
        {
            _underlyingContextMenu.Text = label;
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
                    var subMenu = new ToolStripMenuItem();
                    subMenu.DropDownItems.AddRange(convertedSubItems.ToArray());
                    return subMenu;
                default:
                    return null;
            }
        }
    }
}
