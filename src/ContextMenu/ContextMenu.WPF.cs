using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using ContextMenu.Abstractions;

namespace ContextMenu
{
    public class ContextMenu : IContextMenu
    {
        private ContextMenuStrip _underlyingContextItems = new ContextMenuStrip();

        private ObservableCollection<IContextMenuItem> _itemsSource = new ObservableCollection<IContextMenuItem>();
        public IEnumerable<IContextMenuItem> ItemsSource
        {
            get => _itemsSource;
            set
            {
                _itemsSource = new ObservableCollection<IContextMenuItem>(value);
                HookupListEvents(_itemsSource);
                //_underlyingContextItems.Items.Clear();
                //_underlyingContextItems.Items.AddRange(
                //    value.Select(ToConcreteContextItem)
                //    .Where(x => x != null)
                //    .ToArray()
                //);
            }
        }

        public ContextMenu()
        {
            HookupListEvents(_itemsSource);
        }


        private void HookupListEvents(ObservableCollection<IContextMenuItem> itemsSource)
        {
            _itemsSource.CollectionChanged -= ContextItems_CollectionChanged;
            _itemsSource.CollectionChanged += ContextItems_CollectionChanged;
        }

        private void ContextItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        int i = e.NewStartingIndex;
                        foreach(IContextMenuItem newItem in e.NewItems.Cast<IContextMenuItem>())
                        {
                            _underlyingContextItems.Items.Insert(i, ToConcreteContextItem(newItem));
                            i++;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:                    

                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    _underlyingContextItems.Items.Clear();
                    break;
            }
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
