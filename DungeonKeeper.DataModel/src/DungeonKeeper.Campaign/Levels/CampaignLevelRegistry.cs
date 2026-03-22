using DungeonKeeper.Campaign.Availability;
using DungeonKeeper.Campaign.Conditions;
using DungeonKeeper.Campaign.MapBlueprint;
using DungeonKeeper.Core.Common;
using DungeonKeeper.Creatures.Definitions;
using DungeonKeeper.Dungeon.Map;
using DungeonKeeper.Dungeon.Rooms;
using DungeonKeeper.Spells;
using DungeonKeeper.Traps;

namespace DungeonKeeper.Campaign.Levels;

public static class CampaignLevelRegistry
{
    public static CampaignDefinition CreateDefaultCampaign()
    {
        return new CampaignDefinition
        {
            Name = "Dungeon Keeper Campaign",
            Levels = new List<LevelDefinition>
            {
                BuildLevel1_Eversmile(),
            }
        };
    }

    private static LevelDefinition BuildLevel1_Eversmile()
    {
        var rng = new Random(101);
        var blueprint = new MapBlueprint.MapBlueprint { Width = 50, Height = 50 };

        // Impenetrable border
        blueprint.AddImpenetrableBorder(2);

        // Gold clusters — abundant, close to start for tutorial ease
        var center = new TileCoordinate(25, 25);
        blueprint.AddGoldCluster(new TileCoordinate(20, 20), 10, rng, 1000);
        blueprint.AddGoldCluster(new TileCoordinate(30, 20), 10, rng, 1000);
        blueprint.AddGoldCluster(new TileCoordinate(20, 30), 10, rng, 1000);
        blueprint.AddGoldCluster(new TileCoordinate(30, 30), 10, rng, 1000);
        blueprint.AddGoldCluster(new TileCoordinate(25, 15), 8, rng, 1200);
        blueprint.AddGoldCluster(new TileCoordinate(25, 35), 8, rng, 1200);

        // Gem cluster farther out
        blueprint.AddGemCluster(new TileCoordinate(10, 25), 4, rng);
        blueprint.AddGemCluster(new TileCoordinate(40, 25), 4, rng);

        // Small water pool
        blueprint.AddPool(new TileCoordinate(38, 38), TileType.Water, 3);

        return new LevelDefinition
        {
            LevelNumber = 1,
            Name = "Eversmile",
            BriefingText =
                "Welcome to Eversmile, Keeper.\n\n" +
                "This disgustingly cheerful land is ripe for corruption. " +
                "Dig out your dungeon, build rooms to attract creatures, and begin amassing your dark army.\n\n" +
                "Build a Lair for creatures to sleep in, a Hatchery to feed them, " +
                "and a Treasury to store your gold. Once you have attracted 4 creatures, " +
                "this realm will be yours.",
            DebriefingText = "Eversmile has fallen. The land trembles at your name, Keeper.",

            MapBlueprint = blueprint,
            PlayerStart = new PlayerStartingPosition
            {
                DungeonHeartCenter = center,
                ClaimedAreaRadius = 2
            },

            StartingGold = 15000,
            StartingMana = 500,
            StartingImpCount = 4,

            Availability = new LevelAvailability
            {
                AvailableRooms = new[]
                {
                    RoomType.Lair,
                    RoomType.Hatchery,
                    RoomType.Treasury,
                    RoomType.Portal,
                },
                AvailableSpells = new[]
                {
                    SpellType.CreateImp,
                },
                AvailableCreatures = new[]
                {
                    CreatureType.Goblin,
                    CreatureType.Firefly,
                },
                AvailableTraps = Array.Empty<TrapType>(),
                AvailableDoors = Array.Empty<DoorType>(),
            },

            VictoryConditions = new IVictoryCondition[]
            {
                new AttractCreaturesCondition { RequiredCount = 8 }, // 4 imps + 4 attracted = 8 total
            },
            DefeatConditions = new IDefeatCondition[]
            {
                new DungeonHeartDestroyedCondition(),
            },

            // No hero waves for level 1 — pure tutorial
            HeroWaves = Array.Empty<Waves.WaveDefinition>(),
            HeroGates = Array.Empty<HeroGateDefinition>(),
            EnemyKeepers = Array.Empty<EnemyKeeperDefinition>(),
        };
    }
}
