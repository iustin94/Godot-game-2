using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Core.Presentation;

public interface ISpellPresenter
{
    void OnSpellCast(string spellId, TileCoordinate? targetTile, EntityId? targetCreature);
    void OnSpellResearched(string spellId);
}
