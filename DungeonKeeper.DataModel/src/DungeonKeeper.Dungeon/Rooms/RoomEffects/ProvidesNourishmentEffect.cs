using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;

namespace DungeonKeeper.Dungeon.Rooms.RoomEffects;

public class ProvidesNourishmentEffect : IRoomEffect
{
    public float ChickenSpawnRate { get; init; }
    public int MaxChickensPerTile { get; init; }
    public int AvailableChickens { get; set; }

    private float _spawnAccumulator;

    public void OnTick(RoomInstance room, GameTime gameTime)
    {
        if (!room.IsOperational) return;

        int maxChickens = MaxChickensPerTile * room.TileCount;
        if (AvailableChickens < maxChickens)
        {
            _spawnAccumulator += ChickenSpawnRate * gameTime.DeltaSeconds;
            if (_spawnAccumulator >= 1f)
            {
                int toSpawn = (int)_spawnAccumulator;
                AvailableChickens = Math.Min(AvailableChickens + toSpawn, maxChickens);
                _spawnAccumulator -= toSpawn;
            }
        }
    }

    public void OnCreatureEnter(RoomInstance room, EntityId creatureId, GameTime gameTime)
    {
        if (AvailableChickens > 0)
        {
            AvailableChickens--;
        }
    }

    public void OnCreatureExit(RoomInstance room, EntityId creatureId, GameTime gameTime) { }
}
