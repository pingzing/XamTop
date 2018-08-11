using System;
using System.Collections.Generic;
using System.Text;

namespace XamTop.ContextMenu.Abstractions
{
    public interface IContextMenu : IContextMenuItem
    {        
        string Label { get; set; }
        IEnumerable<IContextMenuItem> ItemsSource { get; set; }
    }
}
