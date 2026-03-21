namespace DungeonKeeper.Traps;

public sealed class DoorDefinitionRegistry
{
    private readonly Dictionary<string, DoorDefinition> _definitions = new();

    public void Register(DoorDefinition definition)
    {
        _definitions[definition.Id] = definition;
    }

    public DoorDefinition Get(string id)
    {
        return _definitions.TryGetValue(id, out var definition)
            ? definition
            : throw new KeyNotFoundException($"Door definition '{id}' not found.");
    }

    public IReadOnlyList<DoorDefinition> GetAll() => _definitions.Values.ToList();
}
