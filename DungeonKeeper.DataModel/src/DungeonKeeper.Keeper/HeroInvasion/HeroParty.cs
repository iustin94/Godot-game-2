using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Keeper.HeroInvasion;

public enum PartyState
{
    Assembling,
    Advancing,
    Attacking,
    Retreating,
    Defeated
}

public class HeroParty
{
    public List<EntityId> Members { get; } = new();
    public TileCoordinate TargetLocation { get; set; }
    public PartyState State { get; set; } = PartyState.Assembling;
}
