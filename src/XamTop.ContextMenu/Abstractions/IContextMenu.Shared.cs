using System.Collections.Generic;

namespace XamTop.ContextMenu.Abstractions
{
    public interface IContextMenu : IContextMenuItem
    {        
        string Label { get; set; }
        IEnumerable<IContextMenuItem> ItemsSource { get; set; }
    }
}
