using DungeonKeeper.Spells.Effects;

namespace DungeonKeeper.Spells;

public class SpellDefinition
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string AssetId { get; init; }
    public required SpellType Type { get; init; }
    public required int ManaCost { get; init; }
    public required float Cooldown { get; init; }
    public required SpellTargetType TargetType { get; init; }
    public float Range { get; init; }
    public float AreaRadius { get; init; }
    public int ResearchPointsRequired { get; init; }
    public IReadOnlyList<string> Prerequisites { get; init; } = Array.Empty<string>();
    public bool AvailableByDefault { get; init; }
    public required ISpellEffect Effect { get; init; }
}
