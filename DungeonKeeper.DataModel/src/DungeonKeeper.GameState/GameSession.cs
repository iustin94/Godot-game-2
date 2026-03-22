using DungeonKeeper.Core.Commands;
using DungeonKeeper.Core.Events;
using DungeonKeeper.Core.Presentation;
using DungeonKeeper.Dungeon.Map;

namespace DungeonKeeper.GameState;

public class GameSession
{
    public GameClock Clock { get; }
    public DungeonMap Map { get; }
    public IReadOnlyList<Player> Players => _players;
    public IEventBus EventBus { get; }
    public CommandDispatcher CommandDispatcher { get; }
    public EntityRegistry Entities { get; }
    public IPresentationFactory PresentationFactory { get; }

    private readonly List<Player> _players = new();

    public GameSession(
        DungeonMap? map = null,
        IEventBus? eventBus = null,
        IPresentationFactory? presentationFactory = null)
    {
        Clock = new GameClock();
        Map = map ?? new DungeonMap(85, 85);
        EventBus = eventBus ?? new EventBus();
        CommandDispatcher = new CommandDispatcher();
        Entities = new EntityRegistry();
        PresentationFactory = presentationFactory ?? new NullPresenterFactory();
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }
}
