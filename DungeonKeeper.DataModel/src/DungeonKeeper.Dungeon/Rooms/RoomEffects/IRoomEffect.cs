using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Dungeon.Rooms.RoomEffects;

public interface IRoomEffect
{
    void OnTick(RoomInstance room, GameTime gameTime);
    void OnCreatureEnter(RoomInstance room, EntityId creatureId, GameTime gameTime);
    void OnCreatureExit(RoomInstance room, EntityId creatureId, GameTime gameTime);
}
