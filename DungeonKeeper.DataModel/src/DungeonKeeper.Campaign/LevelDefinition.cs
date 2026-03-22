using DungeonKeeper.Campaign.Availability;
using DungeonKeeper.Campaign.Conditions;
using DungeonKeeper.Campaign.Waves;

namespace DungeonKeeper.Campaign;

public class LevelDefinition
{
    public required int LevelNumber { get; init; }
    public required string Name { get; init; }
    public required string BriefingText { get; init; }
    public string? DebriefingText { get; init; }

    public required MapBlueprint.MapBlueprint MapBlueprint { get; init; }
    public required MapBlueprint.PlayerStartingPosition PlayerStart { get; init; }

    public int StartingGold { get; init; } = 8000;
    public int StartingMana { get; init; } = 200;
    public int StartingImpCount { get; init; } = 4;

    public required LevelAvailability Availability { get; init; }

    public required IReadOnlyList<IVictoryCondition> VictoryConditions { get; init; }
    public required IReadOnlyList<IDefeatCondition> DefeatConditions { get; init; }

    public IReadOnlyList<WaveDefinition> HeroWaves { get; init; } = Array.Empty<WaveDefinition>();
    public IReadOnlyList<MapBlueprint.HeroGateDefinition> HeroGates { get; init; } = Array.Empty<MapBlueprint.HeroGateDefinition>();
    public IReadOnlyList<MapBlueprint.EnemyKeeperDefinition> EnemyKeepers { get; init; } = Array.Empty<MapBlueprint.EnemyKeeperDefinition>();
}
