namespace DungeonKeeper.Traps;

public sealed class TrapDefinitionRegistry
{
    private readonly Dictionary<string, TrapDefinition> _definitions = new();

    public void Register(TrapDefinition definition)
    {
        _definitions[definition.Id] = definition;
    }

    public TrapDefinition Get(string id)
    {
        return _definitions.TryGetValue(id, out var definition)
            ? definition
            : throw new KeyNotFoundException($"Trap definition '{id}' not found.");
    }

    public IReadOnlyList<TrapDefinition> GetAll() => _definitions.Values.ToList();
}
