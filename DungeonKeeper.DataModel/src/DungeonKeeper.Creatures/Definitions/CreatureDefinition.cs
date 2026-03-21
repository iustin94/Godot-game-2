using DungeonKeeper.Dungeon.Rooms;

namespace DungeonKeeper.Creatures.Definitions;

/// <summary>
/// Flyweight definition containing immutable data shared by all creatures of a given type.
/// </summary>
public sealed class CreatureDefinition
{
    public CreatureType Type { get; }
    public CreatureFaction Faction { get; }
    public string Name { get; }
    public string AssetId { get; }
    public bool IsElite { get; }

    public CreatureBaseStats BaseStats { get; }
    public LevelProgression LevelProgression { get; }

    public IReadOnlyDictionary<int, IReadOnlyList<string>> AbilitiesByLevel { get; }
    public IReadOnlyList<RoomRequirement> AttractionRequirements { get; }
    public IReadOnlyList<CreatureType> Antipathies { get; }

    public float DropStunDuration { get; }

    public IReadOnlyList<int> WageByLevel { get; }
    public IReadOnlyList<RoomType> JobPreferences { get; }

    public int TrainingRoomMaxLevel { get; }
    public int CombatPitMaxLevel { get; }

    public bool ImmuneToPoison { get; }
    public bool CanFly { get; }
    public bool IsUndead { get; }
    public bool CannotBeAttractedViaPortal { get; }

    public float ManaDrainPerSecond { get; }

    internal CreatureDefinition(
        CreatureType type,
        CreatureFaction faction,
        string name,
        string assetId,
        bool isElite,
        CreatureBaseStats baseStats,
        LevelProgression levelProgression,
        IReadOnlyDictionary<int, IReadOnlyList<string>> abilitiesByLevel,
        IReadOnlyList<RoomRequirement> attractionRequirements,
        IReadOnlyList<CreatureType> antipathies,
        float dropStunDuration,
        IReadOnlyList<int> wageByLevel,
        IReadOnlyList<RoomType> jobPreferences,
        int trainingRoomMaxLevel,
        int combatPitMaxLevel,
        bool immuneToPoison,
        bool canFly,
        bool isUndead,
        bool cannotBeAttractedViaPortal,
        float manaDrainPerSecond)
    {
        Type = type;
        Faction = faction;
        Name = name;
        AssetId = assetId;
        IsElite = isElite;
        BaseStats = baseStats;
        LevelProgression = levelProgression;
        AbilitiesByLevel = abilitiesByLevel;
        AttractionRequirements = attractionRequirements;
        Antipathies = antipathies;
        DropStunDuration = dropStunDuration;
        WageByLevel = wageByLevel;
        JobPreferences = jobPreferences;
        TrainingRoomMaxLevel = trainingRoomMaxLevel;
        CombatPitMaxLevel = combatPitMaxLevel;
        ImmuneToPoison = immuneToPoison;
        CanFly = canFly;
        IsUndead = isUndead;
        CannotBeAttractedViaPortal = cannotBeAttractedViaPortal;
        ManaDrainPerSecond = manaDrainPerSecond;
    }
}
