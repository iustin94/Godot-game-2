namespace DungeonKeeper.Core.Events;

public class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public void Publish<T>(T gameEvent) where T : IGameEvent
    {
        if (!_handlers.TryGetValue(typeof(T), out var handlers))
            return;

        foreach (var handler in handlers.ToList())
        {
            ((Action<T>)handler)(gameEvent);
        }
    }

    public IDisposable Subscribe<T>(Action<T> handler) where T : IGameEvent
    {
        var type = typeof(T);
        if (!_handlers.ContainsKey(type))
            _handlers[type] = new List<Delegate>();

        _handlers[type].Add(handler);
        return new Subscription(() => _handlers[type].Remove(handler));
    }

    private sealed class Subscription : IDisposable
    {
        private readonly Action _unsubscribe;
        private bool _disposed;

        public Subscription(Action unsubscribe) => _unsubscribe = unsubscribe;

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _unsubscribe();
        }
    }
}
