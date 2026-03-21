using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Creatures.Components;

public sealed class InventoryComponent : IComponent
{
    public List<string> Items { get; init; } = new();
}
