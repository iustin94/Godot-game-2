using DungeonKeeper.Core.Entities;
using DungeonKeeper.GameState;

namespace DungeonKeeper.GameState.Tests;

public class GameSessionTests
{
    [Fact]
    public void Can_create_GameSession_with_defaults()
    {
        var session = new GameSession();

        Assert.NotNull(session.Clock);
        Assert.NotNull(session.Map);
        Assert.NotNull(session.Entities);
        Assert.NotNull(session.CommandDispatcher);
    }

    [Fact]
    public void Players_list_works()
    {
        var session = new GameSession();
        var player = new Player
        {
            Id = EntityId.New(),
            Name = "Keeper1",
            IsHuman = true,
            Dungeon = new PlayerDungeon { OwnerId = EntityId.New() }
        };

        session.AddPlayer(player);

        Assert.Single(session.Players);
        Assert.Equal("Keeper1", session.Players[0].Name);
    }

    [Fact]
    public void EventBus_is_available()
    {
        var session = new GameSession();
        Assert.NotNull(session.EventBus);
    }
}
