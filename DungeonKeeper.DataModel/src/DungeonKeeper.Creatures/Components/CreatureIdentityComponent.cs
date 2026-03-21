using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Definitions;

namespace DungeonKeeper.Creatures.Components;

public sealed class CreatureIdentityComponent : IComponent
{
    public CreatureType CreatureType { get; init; }
    public CreatureFaction Faction { get; init; }
    public EntityId OwnerId { get; set; }
    public string? CustomName { get; set; }
}
