namespace GameCore.Entities;

public class Entity : IEntity
{
    private readonly Dictionary<Type, IComponent> _components = new();

    public EntityId Id { get; }

    public Entity() : this(EntityId.New()) { }

    public Entity(EntityId id)
    {
        Id = id;
    }

    public T GetComponent<T>() where T : class, IComponent
    {
        return TryGetComponent<T>()
            ?? throw new InvalidOperationException(
                $"Entity {Id} does not have component {typeof(T).Name}");
    }

    public T? TryGetComponent<T>() where T : class, IComponent
    {
        return _components.TryGetValue(typeof(T), out var component)
            ? (T)component
            : null;
    }

    public bool HasComponent<T>() where T : class, IComponent
    {
        return _components.ContainsKey(typeof(T));
    }

    public void AddComponent<T>(T component) where T : class, IComponent
    {
        _components[typeof(T)] = component;
    }

    public void RemoveComponent<T>() where T : class, IComponent
    {
        _components.Remove(typeof(T));
    }

    public IEnumerable<IComponent> GetAllComponents() => _components.Values;
}
