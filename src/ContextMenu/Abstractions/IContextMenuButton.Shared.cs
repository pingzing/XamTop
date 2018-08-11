using System;
using System.Collections.Generic;
using System.Text;

namespace XamTop.ContextMenu.Abstractions
{
    public interface IContextMenuButton : IContextMenuItem
    {
        /// <summary>
        /// The text to display for the context menu item.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Occurs when the user clicks this context menu item.
        /// </summary>
        EventHandler Clicked { get; set; }
    }
}
