using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Core.Tests;

public class EntityTests
{
    private class TestComponent : IComponent
    {
        public string Value { get; init; } = "test";
    }

    private class OtherComponent : IComponent { }

    [Fact]
    public void AddComponent_and_GetComponent_roundtrips()
    {
        var entity = new Entity();
        var component = new TestComponent { Value = "hello" };

        entity.AddComponent(component);

        var retrieved = entity.GetComponent<TestComponent>();
        Assert.Equal("hello", retrieved.Value);
    }

    [Fact]
    public void GetComponent_throws_on_missing_component()
    {
        var entity = new Entity();

        Assert.Throws<InvalidOperationException>(() => entity.GetComponent<TestComponent>());
    }

    [Fact]
    public void TryGetComponent_returns_null_on_missing()
    {
        var entity = new Entity();

        Assert.Null(entity.TryGetComponent<TestComponent>());
    }

    [Fact]
    public void HasComponent_returns_true_when_present()
    {
        var entity = new Entity();
        entity.AddComponent(new TestComponent());

        Assert.True(entity.HasComponent<TestComponent>());
    }

    [Fact]
    public void HasComponent_returns_false_when_absent()
    {
        var entity = new Entity();

        Assert.False(entity.HasComponent<TestComponent>());
    }

    [Fact]
    public void RemoveComponent_removes_the_component()
    {
        var entity = new Entity();
        entity.AddComponent(new TestComponent());

        entity.RemoveComponent<TestComponent>();

        Assert.False(entity.HasComponent<TestComponent>());
    }

    [Fact]
    public void Each_entity_gets_unique_Id()
    {
        var a = new Entity();
        var b = new Entity();

        Assert.NotEqual(a.Id, b.Id);
    }
}
