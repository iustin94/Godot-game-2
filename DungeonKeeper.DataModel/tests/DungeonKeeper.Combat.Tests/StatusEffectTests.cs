using DungeonKeeper.Combat.Effects;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Combat.Tests;

public class StatusEffectTests
{
    private static IEntity CreateEntityWithStats(float speed = 40f, int health = 100)
    {
        var entity = new Entity();
        var stats = new StatsComponent
        {
            CurrentHealth = health,
            MaxHealth = health,
            Speed = speed
        };
        entity.AddComponent(stats);
        return entity;
    }

    [Fact]
    public void StunnedEffect_SetsSpeedToZero()
    {
        var entity = CreateEntityWithStats(speed: 40f);
        var effect = new StunnedEffect(duration: 3f);

        effect.Apply(entity);

        var stats = entity.GetComponent<StatsComponent>();
        Assert.Equal(0f, stats.Speed);
    }

    [Fact]
    public void StunnedEffect_RestoresSpeed_OnRemove()
    {
        var entity = CreateEntityWithStats(speed: 40f);
        var effect = new StunnedEffect(duration: 3f);

        effect.Apply(entity);
        effect.Remove(entity);

        var stats = entity.GetComponent<StatsComponent>();
        Assert.Equal(40f, stats.Speed);
    }

    [Fact]
    public void PoisonedEffect_DealsDamageOverTime()
    {
        var entity = CreateEntityWithStats(health: 100);
        var effect = new PoisonedEffect(duration: 10f, damagePerSecond: 5f);

        effect.Apply(entity);
        effect.Tick(entity, deltaTime: 2f);

        var stats = entity.GetComponent<StatsComponent>();
        // 5 dps * 2 seconds = 10 damage -> 100 - 10 = 90
        Assert.Equal(90, stats.CurrentHealth);
    }

    [Fact]
    public void SpeedBoostEffect_MultipliesSpeed()
    {
        var entity = CreateEntityWithStats(speed: 40f);
        var effect = new SpeedBoostEffect(duration: 5f, speedMultiplier: 2f);

        effect.Apply(entity);

        var stats = entity.GetComponent<StatsComponent>();
        Assert.Equal(80f, stats.Speed);
    }

    [Fact]
    public void SpeedBoostEffect_RestoresSpeed_OnRemove()
    {
        var entity = CreateEntityWithStats(speed: 40f);
        var effect = new SpeedBoostEffect(duration: 5f, speedMultiplier: 2f);

        effect.Apply(entity);
        effect.Remove(entity);

        var stats = entity.GetComponent<StatsComponent>();
        Assert.Equal(40f, stats.Speed);
    }

    [Fact]
    public void FrozenEffect_SetsSpeedToZero()
    {
        var entity = CreateEntityWithStats(speed: 40f);
        var effect = new FrozenEffect(duration: 3f);

        effect.Apply(entity);

        var stats = entity.GetComponent<StatsComponent>();
        Assert.Equal(0f, stats.Speed);
    }

    [Fact]
    public void FrozenEffect_RestoresSpeed_OnRemove()
    {
        var entity = CreateEntityWithStats(speed: 40f);
        var effect = new FrozenEffect(duration: 3f);

        effect.Apply(entity);
        effect.Remove(entity);

        var stats = entity.GetComponent<StatsComponent>();
        Assert.Equal(40f, stats.Speed);
    }
}
