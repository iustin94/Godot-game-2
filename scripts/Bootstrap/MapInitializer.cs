using DungeonKeeper.Campaign;
using DungeonKeeper.Campaign.MapBlueprint;
using DungeonKeeper.Campaign.Waves;
using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;
using DungeonKeeper.Creatures.Definitions;
using DungeonKeeper.Dungeon.Map;
using DungeonKeeper.Dungeon.Rooms;
using DungeonKeeper.GameState;

namespace DungeonKeeper.Scripts.Bootstrap;

public static class MapInitializer
{
    private static readonly Random Rng = new(42);

    /// <summary>
    /// Initialize from a campaign level definition.
    /// </summary>
    public static Player Initialize(GameSession session, LevelDefinition level)
    {
        var map = session.Map;

        // Apply blueprint tiles (non-Earth entries overwrite defaults)
        foreach (var entry in level.MapBlueprint.Tiles)
        {
            var tile = map.GetTile(entry.Coordinate);
            if (tile == null) continue;

            tile.Type = entry.Type;
            tile.OwnerId = entry.OwnerId;
            tile.RoomType = entry.RoomType;
            tile.GoldRemaining = entry.GoldRemaining;
            tile.IsRevealed = entry.IsRevealed;
        }

        // Create human player
        var player = CreatePlayer(session, level.StartingGold, level.StartingMana);

        // Carve starting area and place dungeon heart
        CarveStartingArea(session, player.Id, level.PlayerStart);

        // Spawn starting imps
        SpawnInitialCreatures(session, player, level.PlayerStart.DungeonHeartCenter, level.StartingImpCount);

        // Schedule hero invasion waves
        if (level.HeroWaves.Count > 0 && level.HeroGates.Count > 0)
        {
            WaveScheduleBuilder.PopulateScheduler(
                player.Dungeon.InvasionScheduler,
                level.HeroWaves,
                level.HeroGates);
        }

        return player;
    }

    /// <summary>
    /// Original sandbox/free-play initialization (no campaign level).
    /// </summary>
    public static Player Initialize(GameSession session)
    {
        var player = CreatePlayer(session, 15000, 500);
        SetupBorders(session);
        CarveStartingArea(session, player.Id, new PlayerStartingPosition
        {
            DungeonHeartCenter = new TileCoordinate(session.Map.Width / 2, session.Map.Height / 2)
        });
        ScatterGoldVeins(session);
        ScatterFeatures(session);
        SpawnInitialCreatures(session, player,
            new TileCoordinate(session.Map.Width / 2, session.Map.Height / 2), 4);
        return player;
    }

    private static Player CreatePlayer(GameSession session, int startingGold, int startingMana)
    {
        var playerId = EntityId.New();
        var dungeon = new PlayerDungeon
        {
            OwnerId = playerId,
            Gold = { Current = startingGold, Capacity = 50000 },
            Mana = { Current = startingMana, Capacity = 5000, GenerationRate = 2.0f },
        };

        var player = new Player
        {
            Id = playerId,
            Name = "Keeper",
            IsHuman = true,
            Dungeon = dungeon
        };

        session.AddPlayer(player);
        return player;
    }

    private static void SetupBorders(GameSession session)
    {
        var map = session.Map;
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                if (x < 2 || x >= map.Width - 2 || y < 2 || y >= map.Height - 2)
                {
                    var tile = map.GetTile(new TileCoordinate(x, y));
                    if (tile != null)
                    {
                        tile.Type = TileType.Impenetrable;
                    }
                }
            }
        }
    }

    private static void CarveStartingArea(GameSession session, EntityId playerId, PlayerStartingPosition start)
    {
        var map = session.Map;
        int cx = start.DungeonHeartCenter.X;
        int cy = start.DungeonHeartCenter.Y;
        int radius = start.ClaimedAreaRadius;

        // Carve claimed path
        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = -radius; dy <= radius; dy++)
            {
                var tile = map.GetTile(new TileCoordinate(cx + dx, cy + dy));
                if (tile != null)
                {
                    tile.Type = TileType.ClaimedPath;
                    tile.OwnerId = playerId;
                    tile.IsRevealed = true;
                }
            }
        }

        // Place dungeon heart at center 3x3
        var heartId = EntityId.New();
        var heartTiles = new List<TileCoordinate>();

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                var coord = new TileCoordinate(cx + dx, cy + dy);
                var tile = map.GetTile(coord);
                if (tile != null)
                {
                    tile.Type = TileType.Room;
                    tile.RoomType = RoomType.DungeonHeart;
                    tile.RoomInstanceId = heartId;
                    heartTiles.Add(coord);
                }
            }
        }

        var heartRoom = new RoomInstance
        {
            Id = heartId,
            Type = RoomType.DungeonHeart,
            OwnerId = playerId,
            MinimumSize = 1,
            Health = 5000,
            Capacity = 0
        };
        foreach (var t in heartTiles) heartRoom.Tiles.Add(t);
        session.Players[^1].Dungeon.AddRoom(heartRoom);

        var roomPresenter = session.PresentationFactory.CreateRoomPresenter(heartId, "DungeonHeart");
        roomPresenter.OnRoomPlaced(heartId, "DungeonHeart", heartTiles);
    }

    private static void SpawnInitialCreatures(GameSession session, Player player, TileCoordinate center, int count)
    {
        var offsets = new TileCoordinate[]
        {
            new(center.X - 1, center.Y + 2),
            new(center.X, center.Y + 2),
            new(center.X + 1, center.Y + 2),
            new(center.X, center.Y - 2),
            new(center.X - 2, center.Y),
            new(center.X + 2, center.Y),
        };

        for (int i = 0; i < Math.Min(count, offsets.Length); i++)
        {
            var pos = offsets[i];
            var imp = new Entity();
            imp.AddComponent(new CreatureIdentityComponent
            {
                CreatureType = CreatureType.Imp,
                Faction = CreatureFaction.Keeper,
                OwnerId = player.Id
            });
            imp.AddComponent(new StatsComponent
            {
                CurrentHealth = 50,
                MaxHealth = 50,
                Speed = 2f,
                MeleeAttack = 5,
                MeleeDamage = 3,
                Defense = 2
            });
            imp.AddComponent(new MovementComponent
            {
                CurrentPosition = pos
            });

            session.Entities.Register(imp);
            player.Dungeon.AddCreature(imp.Id);

            var presenter = session.PresentationFactory.CreateCreaturePresenter(imp.Id, "Imp");
            presenter.OnSpawned(imp.Id, pos);
        }
    }

    private static void ScatterGoldVeins(GameSession session)
    {
        var map = session.Map;
        int cx = map.Width / 2;
        int cy = map.Height / 2;

        var clusterCenters = new TileCoordinate[]
        {
            new(cx - 8, cy - 6), new(cx + 8, cy - 6),
            new(cx - 8, cy + 6), new(cx + 8, cy + 6),
            new(cx - 5, cy - 10), new(cx + 5, cy - 10),
            new(cx - 5, cy + 10), new(cx + 5, cy + 10),
        };

        foreach (var center in clusterCenters)
        {
            PlaceGoldCluster(map, center, 8 + Rng.Next(6));
        }

        var gemCenters = new TileCoordinate[]
        {
            new(cx - 15, cy), new(cx + 15, cy),
            new(cx, cy - 15), new(cx, cy + 15),
        };

        foreach (var center in gemCenters)
        {
            PlaceGemCluster(map, center, 3 + Rng.Next(3));
        }
    }

    private static void PlaceGoldCluster(DungeonMap map, TileCoordinate center, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int dx = Rng.Next(-2, 3);
            int dy = Rng.Next(-2, 3);
            var coord = new TileCoordinate(center.X + dx, center.Y + dy);
            var tile = map.GetTile(coord);
            if (tile != null && tile.Type == TileType.Earth)
            {
                tile.Type = TileType.Gold;
                tile.GoldRemaining = 800 + Rng.Next(400);
            }
        }
    }

    private static void PlaceGemCluster(DungeonMap map, TileCoordinate center, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int dx = Rng.Next(-1, 2);
            int dy = Rng.Next(-1, 2);
            var coord = new TileCoordinate(center.X + dx, center.Y + dy);
            var tile = map.GetTile(coord);
            if (tile != null && tile.Type == TileType.Earth)
            {
                tile.Type = TileType.Gem;
            }
        }
    }

    private static void ScatterFeatures(GameSession session)
    {
        var map = session.Map;

        PlaceFeature(map, new TileCoordinate(20, 20), TileType.Water, 3);
        PlaceFeature(map, new TileCoordinate(65, 65), TileType.Water, 4);
        PlaceFeature(map, new TileCoordinate(25, 60), TileType.Water, 2);

        PlaceFeature(map, new TileCoordinate(60, 20), TileType.Lava, 3);
        PlaceFeature(map, new TileCoordinate(30, 40), TileType.Lava, 2);
    }

    private static void PlaceFeature(DungeonMap map, TileCoordinate center, TileType type, int radius)
    {
        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = -radius; dy <= radius; dy++)
            {
                if (dx * dx + dy * dy > radius * radius) continue;
                var coord = new TileCoordinate(center.X + dx, center.Y + dy);
                var tile = map.GetTile(coord);
                if (tile != null && tile.Type == TileType.Earth)
                {
                    tile.Type = type;
                }
            }
        }
    }
}
