using XamTop.ContextMenu.Abstractions;

namespace XamTop.ContextMenu
{
    public partial class ContextMenu
    {
        private void PlatformAdd(int i, IContextMenuItem newItem) 
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformClear()
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformMove(int oldIndex, int newIndex)
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformRemove(int i)
            => throw new NotImplementedInReferenceAssemblyException();

        private void PlatformSetLabel(string label)
            => throw new NotImplementedInReferenceAssemblyException();
    }
}

