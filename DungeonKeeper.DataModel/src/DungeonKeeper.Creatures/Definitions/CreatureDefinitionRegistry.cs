namespace DungeonKeeper.Creatures.Definitions;

public sealed class CreatureDefinitionRegistry
{
    private readonly Dictionary<CreatureType, CreatureDefinition> _definitions = new();

    public void Register(CreatureDefinition definition)
    {
        _definitions[definition.Type] = definition;
    }

    public CreatureDefinition Get(CreatureType type)
    {
        if (!_definitions.TryGetValue(type, out var definition))
            throw new KeyNotFoundException($"No creature definition registered for {type}.");
        return definition;
    }

    public IReadOnlyCollection<CreatureDefinition> GetAll() => _definitions.Values;

    public IEnumerable<CreatureDefinition> GetByFaction(CreatureFaction faction) =>
        _definitions.Values.Where(d => d.Faction == faction);
}
