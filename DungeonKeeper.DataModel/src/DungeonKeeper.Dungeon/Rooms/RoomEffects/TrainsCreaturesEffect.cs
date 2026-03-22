using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Dungeon.Rooms.RoomEffects;

public class TrainsCreaturesEffect : IRoomEffect
{
    public float BaseTrainingRate { get; init; }
    public float AccumulatedTraining { get; set; }

    public void OnTick(RoomInstance room, GameTime gameTime)
    {
        if (room.IsOperational)
        {
            AccumulatedTraining = BaseTrainingRate * room.AssignedCreatures.Count * gameTime.DeltaSeconds;
        }
    }

    public void OnCreatureEnter(RoomInstance room, EntityId creatureId, GameTime gameTime) { }
    public void OnCreatureExit(RoomInstance room, EntityId creatureId, GameTime gameTime) { }
}
