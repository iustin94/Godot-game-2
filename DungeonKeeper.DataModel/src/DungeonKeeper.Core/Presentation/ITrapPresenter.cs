using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Core.Presentation;

public interface ITrapPresenter
{
    void OnTrapPlaced(EntityId trapId, TileCoordinate position);
    void OnTrapTriggered(EntityId trapId);
    void OnTrapRearmed(EntityId trapId);
    void OnDoorStateChanged(EntityId doorId, bool isOpen);
    void OnDoorDamaged(EntityId doorId, float healthPercent);
}
