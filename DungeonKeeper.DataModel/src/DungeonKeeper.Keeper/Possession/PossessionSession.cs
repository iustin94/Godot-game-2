namespace DungeonKeeper.Keeper.Possession;
using DungeonKeeper.Core.Entities;

public class PossessionSession
{
    public EntityId PossessedCreatureId { get; init; }
    public EntityId KeeperId { get; init; }
    public bool IsActive { get; set; }
    public int InitialManaCost { get; init; } = 500;
    public float OngoingManaDrainPerSecond { get; init; } = 25f;
    public float TotalManaDrained { get; set; }
}
