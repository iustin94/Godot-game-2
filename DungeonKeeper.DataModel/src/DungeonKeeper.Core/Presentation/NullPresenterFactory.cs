using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Core.Presentation;

public class NullPresenterFactory : IPresentationFactory
{
    public ICreaturePresenter CreateCreaturePresenter(EntityId entityId, string assetId) => NullCreaturePresenter.Instance;
    public IRoomPresenter CreateRoomPresenter(EntityId roomId, string assetId) => NullRoomPresenter.Instance;
    public IMapPresenter CreateMapPresenter() => NullMapPresenter.Instance;
    public ISpellPresenter CreateSpellPresenter() => NullSpellPresenter.Instance;
    public ICombatPresenter CreateCombatPresenter() => NullCombatPresenter.Instance;
    public ITrapPresenter CreateTrapPresenter(EntityId entityId, string assetId) => NullTrapPresenter.Instance;
    public IAudioPresenter CreateAudioPresenter() => NullAudioPresenter.Instance;
}

internal sealed class NullCreaturePresenter : ICreaturePresenter
{
    public static readonly NullCreaturePresenter Instance = new();
    public void OnSpawned(EntityId id, TileCoordinate position) { }
    public void OnMoved(EntityId id, TileCoordinate from, TileCoordinate to) { }
    public void OnDestroyed(EntityId id) { }
    public void OnStateChanged(EntityId id, string stateName) { }
    public void OnDamageTaken(EntityId id, int amount, string damageType) { }
    public void OnHealed(EntityId id, int amount) { }
    public void OnLevelUp(EntityId id, int newLevel) { }
    public void OnAbilityUsed(EntityId id, string abilityId, TileCoordinate? target) { }
    public void OnMoraleChanged(EntityId id, string newState) { }
    public void OnSlapped(EntityId id) { }
    public void OnPickedUp(EntityId id) { }
    public void OnDropped(EntityId id, TileCoordinate position, bool stunned) { }
    public void OnDeath(EntityId id) { }
}

internal sealed class NullRoomPresenter : IRoomPresenter
{
    public static readonly NullRoomPresenter Instance = new();
    public void OnRoomPlaced(EntityId roomId, string roomType, IReadOnlyList<TileCoordinate> tiles) { }
    public void OnRoomExpanded(EntityId roomId, IReadOnlyList<TileCoordinate> newTiles) { }
    public void OnRoomSold(EntityId roomId) { }
    public void OnWorkerEntered(EntityId roomId, EntityId creatureId) { }
    public void OnWorkerExited(EntityId roomId, EntityId creatureId) { }
}

internal sealed class NullMapPresenter : IMapPresenter
{
    public static readonly NullMapPresenter Instance = new();
    public void OnTileDug(TileCoordinate coord) { }
    public void OnTileClaimed(TileCoordinate coord, EntityId ownerId) { }
    public void OnTileReinforced(TileCoordinate coord) { }
    public void OnFogOfWarRevealed(TileCoordinate coord) { }
}

internal sealed class NullSpellPresenter : ISpellPresenter
{
    public static readonly NullSpellPresenter Instance = new();
    public void OnSpellCast(string spellId, TileCoordinate? targetTile, EntityId? targetCreature) { }
    public void OnSpellResearched(string spellId) { }
}

internal sealed class NullCombatPresenter : ICombatPresenter
{
    public static readonly NullCombatPresenter Instance = new();
    public void OnAttack(EntityId attackerId, EntityId defenderId, bool hit, int damage, bool critical) { }
    public void OnStatusEffectApplied(EntityId targetId, string effectType) { }
    public void OnStatusEffectRemoved(EntityId targetId, string effectType) { }
}

internal sealed class NullTrapPresenter : ITrapPresenter
{
    public static readonly NullTrapPresenter Instance = new();
    public void OnTrapPlaced(EntityId trapId, TileCoordinate position) { }
    public void OnTrapTriggered(EntityId trapId) { }
    public void OnTrapRearmed(EntityId trapId) { }
    public void OnDoorStateChanged(EntityId doorId, bool isOpen) { }
    public void OnDoorDamaged(EntityId doorId, float healthPercent) { }
}

internal sealed class NullAudioPresenter : IAudioPresenter
{
    public static readonly NullAudioPresenter Instance = new();
    public void PlaySound(string soundId, TileCoordinate? position = null) { }
    public void PlayMusic(string trackId) { }
    public void OnMentorMessage(string messageId) { }
}
