using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.GameState;

public class Player
{
    public EntityId Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool IsHuman { get; init; }
    public PlayerDungeon Dungeon { get; init; } = null!;
}
