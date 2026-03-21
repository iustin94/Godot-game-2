using DungeonKeeper.Core.Commands;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Keeper.HandOfEvil;

public class PickUpCommand : ICommand
{
    public EntityId TargetCreatureId { get; init; }

    private readonly HandInventory _handInventory;
    private readonly Func<EntityId, bool> _creatureExists;

    public PickUpCommand(HandInventory handInventory, Func<EntityId, bool> creatureExists)
    {
        _handInventory = handInventory;
        _creatureExists = creatureExists;
    }

    public bool CanExecute(ICommandContext context)
    {
        return _creatureExists(TargetCreatureId) && !_handInventory.IsFull;
    }

    public CommandResult Execute(ICommandContext context)
    {
        if (!CanExecute(context))
            return CommandResult.Fail("Cannot pick up creature: creature not found or hand is full.");

        _handInventory.TryAdd(TargetCreatureId);
        return CommandResult.Ok();
    }
}
