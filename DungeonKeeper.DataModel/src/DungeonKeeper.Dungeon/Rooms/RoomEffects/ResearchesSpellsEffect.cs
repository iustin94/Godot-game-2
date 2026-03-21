using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Dungeon.Rooms.RoomEffects;

public class ResearchesSpellsEffect : IRoomEffect
{
    public float BaseResearchRate { get; init; }

    public void OnTick(RoomInstance room, GameTime gameTime) { }
    public void OnCreatureEnter(RoomInstance room, EntityId creatureId, GameTime gameTime) { }
    public void OnCreatureExit(RoomInstance room, EntityId creatureId, GameTime gameTime) { }
}
