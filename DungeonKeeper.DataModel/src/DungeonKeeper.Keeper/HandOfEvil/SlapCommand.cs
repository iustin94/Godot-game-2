using DungeonKeeper.Core.Commands;
using DungeonKeeper.Core.Entities;

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

    private readonly Func<EntityId, bool> _creatureExists;
    private readonly Func<EntityId, bool> _isImp;

    public SlapCommand(Func<EntityId, bool> creatureExists, Func<EntityId, bool>? isImp = null)
    {
        _creatureExists = creatureExists;
        _isImp = isImp ?? (_ => false);
    }

    public bool CanExecute(ICommandContext context)
    {
        return _creatureExists(TargetCreatureId);
    }

    public CommandResult Execute(ICommandContext context)
    {
        if (!CanExecute(context))
            return CommandResult.Fail("Cannot slap: creature not found.");

        // Effects applied externally based on constants:
        // - SlapDamage (10) to the creature
        // - Temporary speed boost (1.5x)
        // - Reduced happiness (-10)
        // - If imp: 2x work speed for 5 seconds
        var isImpCreature = _isImp(TargetCreatureId);

        return CommandResult.Ok();
    }
}
