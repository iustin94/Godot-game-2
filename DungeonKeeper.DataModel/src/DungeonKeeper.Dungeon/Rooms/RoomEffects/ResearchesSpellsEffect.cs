using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Dungeon.Rooms.RoomEffects;

public class ResearchesSpellsEffect : IRoomEffect
{
    public float BaseResearchRate { get; init; }
    public float AccumulatedPoints { get; set; }

    public void OnTick(RoomInstance room, GameTime gameTime)
    {
        if (room.IsOperational && room.AssignedCreatures.Count > 0)
        {
            AccumulatedPoints += BaseResearchRate * room.AssignedCreatures.Count * gameTime.DeltaSeconds;
        }
    }

    public void OnCreatureEnter(RoomInstance room, EntityId creatureId, GameTime gameTime) { }
    public void OnCreatureExit(RoomInstance room, EntityId creatureId, GameTime gameTime) { }
}
