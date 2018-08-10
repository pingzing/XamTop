namespace ContextMenu.Abstractions
{
    internal interface IPlatformContextMenu
    {        
        void AddToUnderlying(int i, IContextMenuItem newItem);
        void ClearUnderlying();
        void MoveInUnderlying(int oldIndex, int newIndex);
        void RemoveFromUnderlying(int i);
        void SetLabel(string label);
    }
}
