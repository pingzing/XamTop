namespace ContextMenu.Abstractions
{
    internal interface IPlatformContextMenu
    {        
        void AddToUnderlying(int i, IContextMenuItem newItem);
        void MoveInUnderlying(int oldIndex, int newIndex);
        void RemoveFromUnderlying(int i);
        void ClearUnderlying();
    }
}
