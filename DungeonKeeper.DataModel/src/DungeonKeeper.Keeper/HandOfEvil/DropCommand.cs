using DungeonKeeper.Core.Commands;
using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;

namespace DungeonKeeper.Keeper.HandOfEvil;

public class DropCommand : ICommand
{
    public int HandSlotIndex { get; init; }
    public TileCoordinate TargetTile { get; init; }

    private readonly HandInventory _handInventory;
    private readonly Func<TileCoordinate, bool> _isValidTile;
    private readonly Func<TileCoordinate, bool> _isCombatArea;
    private readonly Func<EntityId, IEntity?>? _getEntity;

    public DropCommand(
        HandInventory handInventory,
        Func<TileCoordinate, bool> isValidTile,
        Func<TileCoordinate, bool>? isCombatArea = null,
        Func<EntityId, IEntity?>? getEntity = null)
    {
        _handInventory = handInventory;
        _isValidTile = isValidTile;
        _isCombatArea = isCombatArea ?? (_ => false);
        _getEntity = getEntity;
    }

    public bool CanExecute(ICommandContext context)
    {
        if (HandSlotIndex < 0 || HandSlotIndex >= _handInventory.HeldEntities.Count)
            return false;

        return _isValidTile(TargetTile);
    }

    public CommandResult Execute(ICommandContext context)
    {
        if (!CanExecute(context))
            return CommandResult.Fail("Cannot drop: invalid slot or tile.");

        var entityId = _handInventory.RemoveAt(HandSlotIndex);
        if (entityId == null)
            return CommandResult.Fail("No entity at the specified slot.");

        var stunned = _isCombatArea(TargetTile);

        // Apply position and stun state to the creature
        var entity = _getEntity?.Invoke(entityId.Value);
        if (entity != null)
        {
            var movement = entity.TryGetComponent<MovementComponent>();
            if (movement != null)
            {
                movement.CurrentPosition = TargetTile;
                movement.TargetPosition = null;
                movement.CurrentPath.Clear();
            }
        }

        return CommandResult.Ok();
    }
}
