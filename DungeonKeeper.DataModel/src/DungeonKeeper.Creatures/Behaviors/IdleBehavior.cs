using DungeonKeeper.Core.Common;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Creatures.Behaviors;

public sealed class IdleBehavior : ICreatureBehavior
{
    private static readonly Random Rng = new();

    public void Execute(CreatureBehaviorContext context, GameTime time)
    {
        var needs = context.Entity.TryGetComponent<NeedsComponent>();
        if (needs is not null)
        {
            needs.Hunger = Math.Min(1f, needs.Hunger + 0.001f * time.DeltaSeconds);
            needs.Tiredness = Math.Min(1f, needs.Tiredness + 0.0005f * time.DeltaSeconds);
        }

        var movement = context.Entity.TryGetComponent<MovementComponent>();
        if (movement is not null && movement.TargetPosition is null)
        {
            var current = movement.CurrentPosition;
            var neighbors = current.GetNeighbors().ToArray();
            movement.TargetPosition = neighbors[Rng.Next(neighbors.Length)];
        }
    }
}
