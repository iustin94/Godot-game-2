using DungeonKeeper.Dungeon.Rooms;

namespace DungeonKeeper.Creatures.Definitions;

public sealed class CreatureDefinitionBuilder
{
    private CreatureType _type;
    private CreatureFaction _faction = CreatureFaction.Keeper;
    private string _name = string.Empty;
    private string _assetId = string.Empty;
    private bool _isElite;
    private CreatureBaseStats _baseStats = new(100, 10, 5, 10, 0, 50, 32f, 0, 0, 50);
    private LevelProgression _levelProgression = new(50, 5, 2, 3, 1, 1f);
    private readonly Dictionary<int, IReadOnlyList<string>> _abilitiesByLevel = new();
    private readonly List<RoomRequirement> _attractionRequirements = new();
    private readonly List<CreatureType> _antipathies = new();
    private float _dropStunDuration = 2f;
    private List<int> _wageByLevel = new() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private readonly List<RoomType> _jobPreferences = new();
    private int _trainingRoomMaxLevel = 4;
    private int _combatPitMaxLevel = 8;
    private bool _immuneToPoison;
    private bool _canFly;
    private bool _isUndead;
    private bool _cannotBeAttractedViaPortal;
    private float _manaDrainPerSecond;

    public CreatureDefinitionBuilder WithType(CreatureType type)
    {
        _type = type;
        return this;
    }

    public CreatureDefinitionBuilder WithFaction(CreatureFaction faction)
    {
        _faction = faction;
        return this;
    }

    public CreatureDefinitionBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CreatureDefinitionBuilder WithAssetId(string assetId)
    {
        _assetId = assetId;
        return this;
    }

    public CreatureDefinitionBuilder IsElite(bool elite = true)
    {
        _isElite = elite;
        return this;
    }

    public CreatureDefinitionBuilder WithBaseStats(CreatureBaseStats stats)
    {
        _baseStats = stats;
        return this;
    }

    public CreatureDefinitionBuilder WithLevelProgression(LevelProgression progression)
    {
        _levelProgression = progression;
        return this;
    }

    public CreatureDefinitionBuilder WithAbilitiesAtLevel(int level, params string[] abilities)
    {
        _abilitiesByLevel[level] = abilities.ToList().AsReadOnly();
        return this;
    }

    public CreatureDefinitionBuilder WithAttractionRequirement(RoomType roomType, int minimumSize)
    {
        _attractionRequirements.Add(new RoomRequirement(roomType, minimumSize));
        return this;
    }

    public CreatureDefinitionBuilder WithAntipathy(CreatureType type)
    {
        _antipathies.Add(type);
        return this;
    }

    public CreatureDefinitionBuilder WithDropStunDuration(float duration)
    {
        _dropStunDuration = duration;
        return this;
    }

    public CreatureDefinitionBuilder WithWages(params int[] wageByLevel)
    {
        _wageByLevel = wageByLevel.ToList();
        return this;
    }

    public CreatureDefinitionBuilder WithJobPreference(RoomType roomType)
    {
        _jobPreferences.Add(roomType);
        return this;
    }

    public CreatureDefinitionBuilder WithTrainingRoomMaxLevel(int level)
    {
        _trainingRoomMaxLevel = level;
        return this;
    }

    public CreatureDefinitionBuilder WithCombatPitMaxLevel(int level)
    {
        _combatPitMaxLevel = level;
        return this;
    }

    public CreatureDefinitionBuilder IsImmuneToPoison(bool immune = true)
    {
        _immuneToPoison = immune;
        return this;
    }

    public CreatureDefinitionBuilder HasFlight(bool canFly = true)
    {
        _canFly = canFly;
        return this;
    }

    public CreatureDefinitionBuilder IsUndead(bool undead = true)
    {
        _isUndead = undead;
        return this;
    }

    public CreatureDefinitionBuilder CannotBeAttractedViaPortal(bool cannotAttract = true)
    {
        _cannotBeAttractedViaPortal = cannotAttract;
        return this;
    }

    public CreatureDefinitionBuilder WithManaDrain(float drainPerSecond)
    {
        _manaDrainPerSecond = drainPerSecond;
        return this;
    }

    public CreatureDefinition Build()
    {
        if (string.IsNullOrEmpty(_name))
            _name = _type.ToString();
        if (string.IsNullOrEmpty(_assetId))
            _assetId = _type.ToString().ToLowerInvariant();

        return new CreatureDefinition(
            _type,
            _faction,
            _name,
            _assetId,
            _isElite,
            _baseStats,
            _levelProgression,
            _abilitiesByLevel.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value),
            _attractionRequirements.AsReadOnly(),
            _antipathies.AsReadOnly(),
            _dropStunDuration,
            _wageByLevel.AsReadOnly(),
            _jobPreferences.AsReadOnly(),
            _trainingRoomMaxLevel,
            _combatPitMaxLevel,
            _immuneToPoison,
            _canFly,
            _isUndead,
            _cannotBeAttractedViaPortal,
            _manaDrainPerSecond);
    }
}
