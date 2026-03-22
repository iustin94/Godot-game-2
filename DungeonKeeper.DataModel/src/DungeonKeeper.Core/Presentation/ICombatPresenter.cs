using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Core.Presentation;

public interface ICombatPresenter
{
    void OnAttack(EntityId attackerId, EntityId defenderId, bool hit, int damage, bool critical);
    void OnStatusEffectApplied(EntityId targetId, string effectType);
    void OnStatusEffectRemoved(EntityId targetId, string effectType);
}
