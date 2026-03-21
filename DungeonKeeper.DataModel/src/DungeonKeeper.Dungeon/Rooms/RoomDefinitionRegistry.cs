namespace DungeonKeeper.Dungeon.Rooms;

public class RoomDefinitionRegistry
{
    private readonly Dictionary<RoomType, RoomDefinition> _definitions = new();

    public void Register(RoomDefinition definition)
    {
        _definitions[definition.Type] = definition;
    }

    public RoomDefinition Get(RoomType type)
    {
        if (!_definitions.TryGetValue(type, out var definition))
            throw new KeyNotFoundException($"No room definition registered for {type}");
        return definition;
    }

    public IReadOnlyList<RoomDefinition> GetAll() => _definitions.Values.ToList();
}
