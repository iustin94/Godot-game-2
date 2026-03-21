using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Core.Presentation;

public interface IEntityPresenter
{
    void OnSpawned(EntityId id, TileCoordinate position);
    void OnMoved(EntityId id, TileCoordinate from, TileCoordinate to);
    void OnDestroyed(EntityId id);
    void OnStateChanged(EntityId id, string stateName);
}
