using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Core.Events;

public interface IGameEvent
{
    GameTime Timestamp { get; }
}
