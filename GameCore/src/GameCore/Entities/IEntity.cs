namespace GameCore.Entities;

public interface IEntity
{
    EntityId Id { get; }
    T GetComponent<T>() where T : class, IComponent;
    T? TryGetComponent<T>() where T : class, IComponent;
    bool HasComponent<T>() where T : class, IComponent;
    void AddComponent<T>(T component) where T : class, IComponent;
    void RemoveComponent<T>() where T : class, IComponent;
    IEnumerable<IComponent> GetAllComponents();
}
