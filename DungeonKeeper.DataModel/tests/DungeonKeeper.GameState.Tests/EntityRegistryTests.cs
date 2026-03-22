using DungeonKeeper.Core.Entities;
using DungeonKeeper.GameState;

namespace DungeonKeeper.GameState.Tests;

public class EntityRegistryTests
{
    private sealed class TestComponent : IComponent { }

    [Fact]
    public void Register_and_Get_work()
    {
        var registry = new EntityRegistry();
        var entity = new Entity();

        registry.Register(entity);
        var result = registry.Get(entity.Id);

        Assert.Same(entity, result);
    }

    [Fact]
    public void TryGet_returns_null_for_unknown()
    {
        var registry = new EntityRegistry();

        var result = registry.TryGet(EntityId.New());

        Assert.Null(result);
    }

    [Fact]
    public void GetAll_returns_all_registered()
    {
        var registry = new EntityRegistry();
        var e1 = new Entity();
        var e2 = new Entity();
        registry.Register(e1);
        registry.Register(e2);

        var all = registry.GetAll().ToList();

        Assert.Equal(2, all.Count);
    }

    [Fact]
    public void Remove_removes_entity()
    {
        var registry = new EntityRegistry();
        var entity = new Entity();
        registry.Register(entity);

        var removed = registry.Remove(entity.Id);

        Assert.True(removed);
        Assert.Null(registry.TryGet(entity.Id));
    }

    [Fact]
    public void GetWithComponent_returns_matching_entities()
    {
        var registry = new EntityRegistry();
        var withComp = new Entity();
        withComp.AddComponent(new TestComponent());
        var withoutComp = new Entity();

        registry.Register(withComp);
        registry.Register(withoutComp);

        var matches = registry.GetWithComponent<TestComponent>().ToList();

        Assert.Single(matches);
        Assert.Same(withComp, matches[0]);
    }

    [Fact]
    public void Count_reflects_registered_entities()
    {
        var registry = new EntityRegistry();
        Assert.Equal(0, registry.Count);

        registry.Register(new Entity());
        registry.Register(new Entity());

        Assert.Equal(2, registry.Count);
    }
}
