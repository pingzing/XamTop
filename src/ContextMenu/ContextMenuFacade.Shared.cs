using System;
using System.Collections.Generic;
using XamTop.ContextMenu.Abstractions;
using System.Collections.Specialized;
using System.Linq;

namespace XamTop.ContextMenu
{
    public class ContextMenuFacade : IContextMenu
    {
        private static Lazy<IContextMenu> implementation = new Lazy<IContextMenu>(CreateContextMenu, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static IContextMenu Current
        {
            get
            {
                var ret = implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        private static IContextMenu CreateContextMenu()
        {
#if NETSTANDARD1_0 || NETSTANDARD2_0
            return null;
#else
            return new ContextMenuFacade();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the netstandard version of this assembly.  " +
                "You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }

        public IPlatformContextMenu PlatformContextMenu { get; private set; }        

        private string _label;
        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                PlatformContextMenu.SetLabel(value);
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

        public ContextMenuFacade()
        {
#if NETSTANDARD1_0 || NETSTANDARD2_0
            throw NotImplementedInReferenceAssembly();
#else
            PlatformContextMenu = new PlatformContextMenu();
#endif
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
                            PlatformContextMenu.AddToUnderlying(i, newItem);
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

                            PlatformContextMenu.MoveInUnderlying(oldIndex, newIndex);

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

                            PlatformContextMenu.RemoveFromUnderlying(i);

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
                            PlatformContextMenu.RemoveFromUnderlying(i);
                            i++;
                        }
                    }

                    if (e.NewItems != null)
                    {
                        int i = e.NewStartingIndex;
                        foreach (var added in e.NewItems.Cast<IContextMenuItem>())
                        {
                            _itemsSource.Insert(i, added);
                            PlatformContextMenu.AddToUnderlying(i, added);
                            i++;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    _itemsSource.Clear();
                    PlatformContextMenu.ClearUnderlying();
                    break;
            }
        }
    }
}
