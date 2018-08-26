using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using XamTop.ContextMenu.Abstractions;

namespace XamTop.ContextMenu
{
    public partial class ContextMenu : IContextMenu
    {
        private string _label;
        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                PlatformSetLabel(value);
            }
        }

        private IList<IContextMenuItem> _itemsSource;
        public IEnumerable<IContextMenuItem> ItemsSource
        {
            get => _itemsSource;
            set
            {
                _itemsSource = new List<IContextMenuItem>(value);

                INotifyCollectionChanged observableCollection = value as INotifyCollectionChanged;
                if (observableCollection != null)
                {
                    HookupListEvents(observableCollection);
                }
            }
        }

        private void HookupListEvents(INotifyCollectionChanged itemsSource)
        {
            itemsSource.CollectionChanged -= ContextItems_CollectionChanged;
            itemsSource.CollectionChanged += ContextItems_CollectionChanged;
        }

        private void ContextItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        int i = e.NewStartingIndex;
                        foreach (IContextMenuItem newItem in e.NewItems.Cast<IContextMenuItem>())
                        {
                            _itemsSource.Insert(i, newItem);
                            PlatformAdd(i, newItem);
                            i++;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.NewItems != null)
                    {
                        int oldIndex = e.OldStartingIndex;
                        int newIndex = e.NewStartingIndex;
                        foreach (var _ in e.NewItems)
                        {
                            var abstractionItem = _itemsSource[oldIndex];
                            _itemsSource.Remove(abstractionItem);
                            _itemsSource.Insert(newIndex, abstractionItem);

                            PlatformMove(oldIndex, newIndex);

                            oldIndex++;
                            newIndex++;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        int i = e.OldStartingIndex;
                        foreach (var removedItem in e.OldItems)
                        {
                            _itemsSource.RemoveAt(i);

                            PlatformRemove(i);

                            i++;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldItems != null)
                    {
                        int i = e.OldStartingIndex;
                        foreach (var removed in e.OldItems)
                        {
                            _itemsSource.RemoveAt(i);
                            PlatformRemove(i);
                            i++;
                        }
                    }

                    if (e.NewItems != null)
                    {
                        int i = e.NewStartingIndex;
                        foreach (var added in e.NewItems.Cast<IContextMenuItem>())
                        {
                            _itemsSource.Insert(i, added);
                            PlatformAdd(i, added);
                            i++;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    _itemsSource.Clear();
                    PlatformClear();
                    break;
            }
        }
    }
}
