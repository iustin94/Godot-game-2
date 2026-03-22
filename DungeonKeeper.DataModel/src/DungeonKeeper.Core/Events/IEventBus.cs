namespace DungeonKeeper.Core.Events;

public interface IEventBus
{
    void Publish<T>(T gameEvent) where T : IGameEvent;
    IDisposable Subscribe<T>(Action<T> handler) where T : IGameEvent;
}
