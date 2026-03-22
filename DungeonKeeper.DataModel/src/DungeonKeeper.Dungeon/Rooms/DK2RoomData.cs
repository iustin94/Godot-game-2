using DungeonKeeper.Core.Common;
using DungeonKeeper.Dungeon.Rooms.RoomEffects;

namespace DungeonKeeper.Dungeon.Rooms;

public static class DK2RoomData
{
    public static void RegisterAll(RoomDefinitionRegistry registry)
    {
        registry.Register(new RoomDefinition
        {
            Type = RoomType.DungeonHeart,
            Name = "Dungeon Heart",
            AssetId = "DungeonHeart",
            GoldCostPerTile = 0,
            MinimumSize = 9,
            MaxPerDungeon = 1,
            AvailableByDefault = true,
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.Lair,
            Name = "Lair",
            AssetId = "Lair",
            GoldCostPerTile = 150,
            MinimumSize = 1,
            AvailableByDefault = true,
            Effects = new IRoomEffect[]
            {
                new AttractsCreatureEffect { CreatureAssetId = "generic", RequiredRoomSize = 1 },
            }
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.Hatchery,
            Name = "Hatchery",
            AssetId = "Hatchery",
            GoldCostPerTile = 150,
            MinimumSize = 9,
            AvailableByDefault = true,
            Effects = new IRoomEffect[]
            {
                new ProvidesNourishmentEffect { ChickenSpawnRate = 0.1f, MaxChickensPerTile = 2 },
            }
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.Library,
            Name = "Library",
            AssetId = "Library",
            GoldCostPerTile = 200,
            MinimumSize = 9,
            AvailableByDefault = true,
            Effects = new IRoomEffect[]
            {
                new ResearchesSpellsEffect { BaseResearchRate = 1.0f },
            }
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.TrainingRoom,
            Name = "Training Room",
            AssetId = "TrainingRoom",
            GoldCostPerTile = 200,
            MinimumSize = 9,
            AvailableByDefault = true,
            Effects = new IRoomEffect[]
            {
                new TrainsCreaturesEffect { BaseTrainingRate = 1.0f },
            }
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.CombatPit,
            Name = "Combat Pit",
            AssetId = "CombatPit",
            GoldCostPerTile = 350,
            MinimumSize = 25,
            ResearchPointsToUnlock = 500,
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.Treasury,
            Name = "Treasury",
            AssetId = "Treasury",
            GoldCostPerTile = 50,
            MinimumSize = 1,
            AvailableByDefault = true,
            Effects = new IRoomEffect[]
            {
                new StoresResourceEffect { ResourceType = ResourceType.Gold, CapacityPerTile = 3000 },
            }
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.Workshop,
            Name = "Workshop",
            AssetId = "Workshop",
            GoldCostPerTile = 200,
            MinimumSize = 9,
            ResearchPointsToUnlock = 300,
            Effects = new IRoomEffect[]
            {
                new ManufacturesItemsEffect { BaseManufactureRate = 1.0f },
            }
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.GuardRoom,
            Name = "Guard Room",
            AssetId = "GuardRoom",
            GoldCostPerTile = 150,
            MinimumSize = 1,
            ResearchPointsToUnlock = 200,
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.Casino,
            Name = "Casino",
            AssetId = "Casino",
            GoldCostPerTile = 250,
            MinimumSize = 9,
            ResearchPointsToUnlock = 600,
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.Prison,
            Name = "Prison",
            AssetId = "Prison",
            GoldCostPerTile = 225,
            MinimumSize = 1,
            ResearchPointsToUnlock = 400,
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.TortureChamber,
            Name = "Torture Chamber",
            AssetId = "TortureChamber",
            GoldCostPerTile = 350,
            MinimumSize = 9,
            ResearchPointsToUnlock = 800,
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.Graveyard,
            Name = "Graveyard",
            AssetId = "Graveyard",
            GoldCostPerTile = 200,
            MinimumSize = 9,
            ResearchPointsToUnlock = 500,
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.Temple,
            Name = "Temple",
            AssetId = "Temple",
            GoldCostPerTile = 350,
            MinimumSize = 9,
            ResearchPointsToUnlock = 1000,
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.WoodenBridge,
            Name = "Wooden Bridge",
            AssetId = "WoodenBridge",
            GoldCostPerTile = 100,
            MinimumSize = 1,
            RequiresClaimedFloor = false,
            CanBuildOnWater = true,
            AvailableByDefault = true,
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.StoneBridge,
            Name = "Stone Bridge",
            AssetId = "StoneBridge",
            GoldCostPerTile = 200,
            MinimumSize = 1,
            RequiresClaimedFloor = false,
            CanBuildOnWater = true,
            CanBuildOnLava = true,
            ResearchPointsToUnlock = 400,
        });

        registry.Register(new RoomDefinition
        {
            Type = RoomType.Portal,
            Name = "Portal",
            AssetId = "Portal",
            GoldCostPerTile = 0,
            MinimumSize = 9,
            MaxPerDungeon = 3,
            AvailableByDefault = true,
            Effects = new IRoomEffect[]
            {
                new AttractsCreatureEffect { CreatureAssetId = "portal", RequiredRoomSize = 9 },
            }
        });
    }
}
