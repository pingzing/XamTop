using System;
using System.Collections.Generic;
using System.Text;

namespace ContextMenu.Abstractions
{
    public interface IContextMenu
    {
        IEnumerable<IContextMenuItem> ItemsSource { get; set; }
    }
}
