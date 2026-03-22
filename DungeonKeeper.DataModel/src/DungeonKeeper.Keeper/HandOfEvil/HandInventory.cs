namespace DungeonKeeper.Keeper.HandOfEvil;
using DungeonKeeper.Core.Entities;

public class HandInventory
{
    public const int MaxCapacity = 8;
    private readonly List<EntityId> _heldEntities = new();
    public IReadOnlyList<EntityId> HeldEntities => _heldEntities;
    public bool IsFull => _heldEntities.Count >= MaxCapacity;
    public bool IsEmpty => _heldEntities.Count == 0;

    public bool TryAdd(EntityId entityId)
    {
        if (IsFull) return false;
        _heldEntities.Add(entityId);
        return true;
    }

    public bool TryRemove(EntityId entityId) => _heldEntities.Remove(entityId);

    public EntityId? RemoveAt(int index)
    {
        if (index < 0 || index >= _heldEntities.Count) return null;
        var id = _heldEntities[index];
        _heldEntities.RemoveAt(index);
        return id;
    }
}
