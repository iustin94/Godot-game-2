namespace DungeonKeeper.Campaign;

public class CampaignDefinition
{
    public required string Name { get; init; }
    public required IReadOnlyList<LevelDefinition> Levels { get; init; }

    public LevelDefinition? GetLevel(int levelNumber) =>
        Levels.FirstOrDefault(l => l.LevelNumber == levelNumber);
}
