using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Core.Presentation;

public interface IMapPresenter
{
    void OnTileDug(TileCoordinate coord);
    void OnTileClaimed(TileCoordinate coord, EntityId ownerId);
    void OnTileReinforced(TileCoordinate coord);
    void OnFogOfWarRevealed(TileCoordinate coord);
}
