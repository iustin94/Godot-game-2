using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Core.Presentation;

public interface ICreaturePresenter : IEntityPresenter
{
    void OnDamageTaken(EntityId id, int amount, string damageType);
    void OnHealed(EntityId id, int amount);
    void OnLevelUp(EntityId id, int newLevel);
    void OnAbilityUsed(EntityId id, string abilityId, TileCoordinate? target);
    void OnMoraleChanged(EntityId id, string newState);
    void OnSlapped(EntityId id);
    void OnPickedUp(EntityId id);
    void OnDropped(EntityId id, TileCoordinate position, bool stunned);
    void OnDeath(EntityId id);
}
