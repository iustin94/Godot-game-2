using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.GameState;

public class EntityRegistry
{
    private readonly Dictionary<EntityId, IEntity> _entities = new();

    public IEntity Get(EntityId id) => _entities.TryGetValue(id, out var e)
        ? e : throw new KeyNotFoundException($"Entity {id} not found");
    public IEntity? TryGet(EntityId id) => _entities.TryGetValue(id, out var e) ? e : null;
    public IEnumerable<IEntity> GetAll() => _entities.Values;
    public IEnumerable<IEntity> GetWithComponent<T>() where T : class, IComponent
        => _entities.Values.Where(e => e.HasComponent<T>());
    public void Register(IEntity entity) => _entities[entity.Id] = entity;
    public bool Remove(EntityId id) => _entities.Remove(id);
    public int Count => _entities.Count;
}
