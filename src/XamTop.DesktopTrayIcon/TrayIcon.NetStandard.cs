using System;
using XamTop.ContextMenu.Abstractions;

namespace XamTop.DesktopTrayIcon
{
    public partial class TrayIcon
    {
        private string PlatformIconPath
        {
            get => throw new NotImplementedInReferenceAssemblyException();
            set => throw new NotImplementedInReferenceAssemblyException();
        }

        private string PlatformTrayTooltip
        {
            get => throw new NotImplementedInReferenceAssemblyException();
            set => throw new NotImplementedInReferenceAssemblyException();
        }
        private event EventHandler PlatformClick
        {
            add => throw new NotImplementedInReferenceAssemblyException();
            remove => throw new NotImplementedInReferenceAssemblyException();
        }

        private void PlatformShow() 
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformHide() 
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformShowContextMenu() 
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformHideContextMenu() 
            => throw new NotImplementedInReferenceAssemblyException();

        private IContextMenu PlatformContextMenu
        {
            get => throw new NotImplementedInReferenceAssemblyException();
            set => throw new NotImplementedInReferenceAssemblyException();
        }
    }
}
