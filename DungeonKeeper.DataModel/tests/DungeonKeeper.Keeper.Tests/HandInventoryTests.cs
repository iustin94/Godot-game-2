using DungeonKeeper.Core.Entities;
using DungeonKeeper.Keeper.HandOfEvil;

namespace DungeonKeeper.Keeper.Tests;

public class HandInventoryTests
{
    [Fact]
    public void Starts_empty()
    {
        var inventory = new HandInventory();
        Assert.True(inventory.IsEmpty);
        Assert.Empty(inventory.HeldEntities);
    }

    [Fact]
    public void TryAdd_succeeds_when_not_full()
    {
        var inventory = new HandInventory();
        var id = EntityId.New();

        var result = inventory.TryAdd(id);

        Assert.True(result);
        Assert.Single(inventory.HeldEntities);
        Assert.Equal(id, inventory.HeldEntities[0]);
    }

    [Fact]
    public void IsFull_after_MaxCapacity_items()
    {
        var inventory = new HandInventory();
        for (int i = 0; i < HandInventory.MaxCapacity; i++)
            inventory.TryAdd(EntityId.New());

        Assert.True(inventory.IsFull);
        Assert.Equal(8, inventory.HeldEntities.Count);
    }

    [Fact]
    public void TryAdd_returns_false_when_full()
    {
        var inventory = new HandInventory();
        for (int i = 0; i < HandInventory.MaxCapacity; i++)
            inventory.TryAdd(EntityId.New());

        var result = inventory.TryAdd(EntityId.New());

        Assert.False(result);
        Assert.Equal(HandInventory.MaxCapacity, inventory.HeldEntities.Count);
    }

    [Fact]
    public void TryRemove_removes_correct_entity()
    {
        var inventory = new HandInventory();
        var id1 = EntityId.New();
        var id2 = EntityId.New();
        inventory.TryAdd(id1);
        inventory.TryAdd(id2);

        var removed = inventory.TryRemove(id1);

        Assert.True(removed);
        Assert.Single(inventory.HeldEntities);
        Assert.Equal(id2, inventory.HeldEntities[0]);
    }

    [Fact]
    public void RemoveAt_returns_entity_at_index()
    {
        var inventory = new HandInventory();
        var id1 = EntityId.New();
        var id2 = EntityId.New();
        inventory.TryAdd(id1);
        inventory.TryAdd(id2);

        var result = inventory.RemoveAt(0);

        Assert.Equal(id1, result);
        Assert.Single(inventory.HeldEntities);
        Assert.Equal(id2, inventory.HeldEntities[0]);
    }
}
