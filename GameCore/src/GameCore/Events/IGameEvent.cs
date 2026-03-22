using GameCore.Common;

namespace GameCore.Events;

public interface IGameEvent
{
    GameTime Timestamp { get; }
}
