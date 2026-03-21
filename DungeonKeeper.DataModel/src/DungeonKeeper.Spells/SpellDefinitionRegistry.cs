namespace DungeonKeeper.Spells;

public class SpellDefinitionRegistry
{
    private readonly Dictionary<string, SpellDefinition> _spells = new();

    public void Register(SpellDefinition definition)
    {
        _spells[definition.Id] = definition;
    }

    public SpellDefinition Get(string id)
    {
        return _spells.TryGetValue(id, out var spell)
            ? spell
            : throw new KeyNotFoundException($"Spell '{id}' not found in registry.");
    }

    public IReadOnlyList<SpellDefinition> GetAll()
    {
        return _spells.Values.ToList().AsReadOnly();
    }
}
