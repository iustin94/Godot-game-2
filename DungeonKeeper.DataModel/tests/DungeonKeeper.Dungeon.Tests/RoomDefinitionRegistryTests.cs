using DungeonKeeper.Dungeon.Rooms;

namespace DungeonKeeper.Dungeon.Tests;

public class RoomDefinitionRegistryTests
{
    [Fact]
    public void Register_and_Get_work_correctly()
    {
        var registry = new RoomDefinitionRegistry();
        var definition = new RoomDefinition
        {
            Type = RoomType.Lair,
            Name = "Lair",
            AssetId = "lair_01"
        };

        registry.Register(definition);

        var retrieved = registry.Get(RoomType.Lair);
        Assert.Same(definition, retrieved);
    }

    [Fact]
    public void Get_throws_on_unknown_room_type()
    {
        var registry = new RoomDefinitionRegistry();

        Assert.Throws<KeyNotFoundException>(() => registry.Get(RoomType.Lair));
    }
}
