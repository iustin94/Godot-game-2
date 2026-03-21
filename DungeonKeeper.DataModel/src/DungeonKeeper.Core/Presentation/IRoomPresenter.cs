using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Core.Presentation;

public interface IRoomPresenter
{
    void OnRoomPlaced(EntityId roomId, string roomType, IReadOnlyList<TileCoordinate> tiles);
    void OnRoomExpanded(EntityId roomId, IReadOnlyList<TileCoordinate> newTiles);
    void OnRoomSold(EntityId roomId);
    void OnWorkerEntered(EntityId roomId, EntityId creatureId);
    void OnWorkerExited(EntityId roomId, EntityId creatureId);
}
