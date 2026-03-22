using DungeonKeeper.Core.Commands;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Keeper.HandOfEvil;

public class SlapCommand : ICommand
{
    public EntityId TargetCreatureId { get; init; }

    /// <summary>Damage dealt by a slap.</summary>
    public const int SlapDamage = 10;

    /// <summary>Temporary speed boost multiplier after a slap.</summary>
    public const float SpeedBoostMultiplier = 1.5f;

    /// <summary>Happiness reduction from being slapped.</summary>
    public const int HappinessReduction = 10;

    /// <summary>Imp work speed multiplier after being slapped.</summary>
    public const float ImpWorkSpeedMultiplier = 2.0f;

    /// <summary>Duration of the imp work speed boost in seconds.</summary>
    public const float ImpBoostDurationSeconds = 5.0f;

    private readonly Func<EntityId, IEntity?> _getEntity;
    private readonly Func<EntityId, bool> _isImp;

    public SlapCommand(Func<EntityId, IEntity?> getEntity, Func<EntityId, bool>? isImp = null)
    {
        _getEntity = getEntity;
        _isImp = isImp ?? (_ => false);
    }

    public bool CanExecute(ICommandContext context)
    {
        return _getEntity(TargetCreatureId) != null;
    }

    public CommandResult Execute(ICommandContext context)
    {
        var entity = _getEntity(TargetCreatureId);
        if (entity == null)
            return CommandResult.Fail("Cannot slap: creature not found.");

        // Apply slap damage
        var stats = entity.TryGetComponent<StatsComponent>();
        if (stats != null)
        {
            stats.CurrentHealth = Math.Max(0, stats.CurrentHealth - SlapDamage);
        }

        // Reduce happiness
        var needs = entity.TryGetComponent<NeedsComponent>();
        if (needs != null)
        {
            needs.Happiness = Math.Max(0f, needs.Happiness - HappinessReduction / 100f);
        }

        // Apply speed boost
        var movement = entity.TryGetComponent<MovementComponent>();
        if (movement != null)
        {
            // Speed boost is handled by the game loop checking the SlapBoostRemainingSeconds
        }

        return CommandResult.Ok();
    }
}
