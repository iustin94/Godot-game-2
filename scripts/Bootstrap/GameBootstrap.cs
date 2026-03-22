using DungeonKeeper.Campaign;
using DungeonKeeper.Campaign.Availability;
using DungeonKeeper.Campaign.Conditions;
using DungeonKeeper.Campaign.Levels;
using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Creatures.Components;
using DungeonKeeper.Creatures.Definitions;
using DungeonKeeper.Dungeon.Map;
using DungeonKeeper.Dungeon.Rooms;
using DungeonKeeper.GameState;
using DungeonKeeper.Scripts.Input;
using DungeonKeeper.Scripts.Presenters;
using DungeonKeeper.Scripts.UI;
using Godot;

namespace DungeonKeeper.Scripts.Bootstrap;

public partial class GameBootstrap : Node3D
{
    private enum GamePhase
    {
        LevelSelect,
        Briefing,
        Playing,
        Ended
    }

    private GameSession _session = null!;
    private GodotPresentationFactory _factory = null!;
    private AStarPathfinding _pathfinding = null!;
    private float _tickAccumulator;
    private bool _paused;
    private bool _tutorialPaused;
    private GamePhase _phase = GamePhase.LevelSelect;

    // Campaign state
    private CampaignDefinition _campaign = null!;
    private LevelDefinition? _currentLevel;
    private VictoryConditionEvaluator? _evaluator;

    // UI screens
    private LevelSelectScreen? _levelSelectScreen;
    private BriefingScreen? _briefingScreen;
    private LevelCompleteOverlay? _levelCompleteOverlay;
    private RoomBuildPanel? _roomBuildPanel;

    // Room registry
    private RoomDefinitionRegistry _roomRegistry = null!;

    // Portal spawning state
    private float _portalSpawnAccumulator;
    private const float PortalSpawnIntervalSeconds = 30f; // Spawn a creature every 30 seconds
    private const float PortalFirstSpawnDelay = 15f; // First spawn after 15 seconds

    // Imp AI state
    private readonly Dictionary<EntityId, TileCoordinate?> _impDigTargets = new();
    private readonly Dictionary<EntityId, IReadOnlyList<TileCoordinate>?> _impPaths = new();
    private readonly Dictionary<EntityId, int> _impPathIndex = new();

    private const float DigDamagePerTick = 5f;

    public override void _Ready()
    {
        _roomRegistry = new RoomDefinitionRegistry();
        DK2RoomData.RegisterAll(_roomRegistry);

        _campaign = CampaignLevelRegistry.CreateDefaultCampaign();
        ShowLevelSelect();
    }

    private void ShowLevelSelect()
    {
        _phase = GamePhase.LevelSelect;
        ClearGameWorld();

        _levelSelectScreen = new LevelSelectScreen();
        GetNode<CanvasLayer>("CanvasLayer").AddChild(_levelSelectScreen);
        _levelSelectScreen.SetCampaign(_campaign);
        _levelSelectScreen.LevelSelected += OnLevelSelected;
    }

    private void OnLevelSelected(int levelNumber)
    {
        var level = _campaign.GetLevel(levelNumber);
        if (level == null) return;

        _currentLevel = level;

        // Remove level select
        if (_levelSelectScreen != null)
        {
            _levelSelectScreen.LevelSelected -= OnLevelSelected;
            _levelSelectScreen.QueueFree();
            _levelSelectScreen = null;
        }

        ShowBriefing(level);
    }

    private void ShowBriefing(LevelDefinition level)
    {
        _phase = GamePhase.Briefing;

        _briefingScreen = new BriefingScreen();
        GetNode<CanvasLayer>("CanvasLayer").AddChild(_briefingScreen);
        _briefingScreen.SetLevel(level);
        _briefingScreen.StartLevel += OnStartLevel;
    }

    private void OnStartLevel()
    {
        if (_currentLevel == null) return;

        // Remove briefing
        if (_briefingScreen != null)
        {
            _briefingScreen.StartLevel -= OnStartLevel;
            _briefingScreen.QueueFree();
            _briefingScreen = null;
        }

        StartGameplay(_currentLevel);
    }

    private void StartGameplay(LevelDefinition level)
    {
        _phase = GamePhase.Playing;
        _tickAccumulator = 0;
        _paused = false;
        _tutorialPaused = false;
        _impDigTargets.Clear();
        _impPaths.Clear();
        _impPathIndex.Clear();

        // Create factory and session
        _factory = new GodotPresentationFactory(
            GetNode<Node3D>("MapRoot"),
            GetNode<Node3D>("CreaturesRoot"),
            GetNode<Node3D>("RoomsRoot"),
            GetNode<Node3D>("EffectsRoot")
        );

        var map = new DungeonMap(level.MapBlueprint.Width, level.MapBlueprint.Height);
        _session = new GameSession(
            map: map,
            presentationFactory: _factory
        );

        MapInitializer.Initialize(_session, level);
        _pathfinding = new AStarPathfinding(_session.Map);
        _factory.MapPresenter.RenderFullMap(_session.Map);

        // Set up evaluator
        _evaluator = new VictoryConditionEvaluator(level);

        // Initialize input handler with room registry
        var inputHandler = GetNode<InputHandler>("InputHandler");
        inputHandler.Initialize(_session, _factory.MapPresenter);
        inputHandler.SetRoomRegistry(_roomRegistry);

        // Initialize HUD
        var hud = GetNode<HudOverlay>("CanvasLayer/HudOverlay");
        hud.Initialize(_session);
        hud.Visible = true;

        // Set up room build panel filtered by level availability
        _roomBuildPanel = new RoomBuildPanel();
        GetNode<CanvasLayer>("CanvasLayer").AddChild(_roomBuildPanel);
        var availableRoomDefs = GetAvailableRoomDefinitions(level.Availability);
        _roomBuildPanel.SetAvailableRooms(availableRoomDefs);
        _roomBuildPanel.RoomSelected += (roomTypeIndex) =>
        {
            inputHandler.SelectRoom((RoomType)roomTypeIndex);
        };
        _roomBuildPanel.BuildCancelled += () =>
        {
            inputHandler.SelectRoom(null);
        };

        // Reset portal spawn timer
        _portalSpawnAccumulator = -PortalFirstSpawnDelay;

        // Initialize imp tracking
        foreach (var player in _session.Players)
        {
            foreach (var creatureId in player.Dungeon.OwnedCreatureIds)
            {
                _impDigTargets[creatureId] = null;
                _impPaths[creatureId] = null;
                _impPathIndex[creatureId] = 0;
            }
        }

        // Set up victory/defeat overlay
        _levelCompleteOverlay = new LevelCompleteOverlay();
        GetNode<CanvasLayer>("CanvasLayer").AddChild(_levelCompleteOverlay);
        _levelCompleteOverlay.Continue += OnLevelCompleteReturnToMenu;

        GD.Print($"Level {level.LevelNumber}: {level.Name} started!");

        // Start tutorial only on level 1
        if (level.LevelNumber == 1)
        {
            var tutorialDialog = new TutorialDialog();
            GetNode<CanvasLayer>("CanvasLayer").AddChild(tutorialDialog);

            var tutorialManager = new TutorialManager();
            AddChild(tutorialManager);
            tutorialManager.Initialize(_session, tutorialDialog, paused => _tutorialPaused = paused);
        }
    }

    private void OnLevelCompleteReturnToMenu()
    {
        if (_levelCompleteOverlay != null)
        {
            _levelCompleteOverlay.Continue -= OnLevelCompleteReturnToMenu;
            _levelCompleteOverlay.QueueFree();
            _levelCompleteOverlay = null;
        }

        ShowLevelSelect();
    }

    private void ClearGameWorld()
    {
        // Clear 3D scene nodes
        foreach (var rootName in new[] { "MapRoot", "CreaturesRoot", "RoomsRoot", "EffectsRoot" })
        {
            var root = GetNode<Node3D>(rootName);
            foreach (var child in root.GetChildren())
            {
                child.QueueFree();
            }
        }

        // Clear CanvasLayer UI (except HudOverlay)
        var canvasLayer = GetNode<CanvasLayer>("CanvasLayer");
        var hud = GetNode<HudOverlay>("CanvasLayer/HudOverlay");
        hud.Visible = false;

        foreach (var child in canvasLayer.GetChildren())
        {
            if (child == hud) continue;
            if (child is Node node)
            {
                node.QueueFree();
            }
        }

        // Clear any tutorial managers
        foreach (var child in GetChildren())
        {
            if (child is TutorialManager tm)
            {
                tm.QueueFree();
            }
        }
    }

    public override void _Process(double delta)
    {
        if (_phase != GamePhase.Playing) return;
        if (_paused || _tutorialPaused) return;

        _tickAccumulator += (float)delta;

        while (_tickAccumulator >= _session.Clock.TickDurationSeconds)
        {
            _tickAccumulator -= _session.Clock.TickDurationSeconds;
            var gameTime = _session.Clock.Advance();
            TickGameSystems(gameTime);
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (_phase != GamePhase.Playing) return;

        if (@event is InputEventKey key && key.Pressed && key.Keycode == Key.P)
        {
            _paused = !_paused;
            GD.Print(_paused ? "Game PAUSED" : "Game RESUMED");
        }
    }

    private void TickGameSystems(GameTime gameTime)
    {
        TickImpAI(gameTime);
        TickResources(gameTime);
        TickPortalSpawning(gameTime);
        CheckVictoryConditions();
    }

    private void CheckVictoryConditions()
    {
        if (_evaluator == null || _phase != GamePhase.Playing) return;

        var outcome = _evaluator.Evaluate(_session);
        if (outcome == LevelOutcome.InProgress) return;

        _phase = GamePhase.Ended;
        GD.Print($"Level outcome: {outcome}");

        _levelCompleteOverlay?.ShowOutcome(outcome, _currentLevel?.DebriefingText);
    }

    private void TickImpAI(GameTime gameTime)
    {
        if (_session.Players.Count == 0) return;
        var player = _session.Players[0];

        foreach (var creatureId in player.Dungeon.OwnedCreatureIds)
        {
            var entity = _session.Entities.TryGet(creatureId);
            if (entity == null) continue;

            var identity = entity.TryGetComponent<CreatureIdentityComponent>();
            if (identity == null || identity.CreatureType != CreatureType.Imp) continue;

            var movement = entity.TryGetComponent<MovementComponent>();
            if (movement == null) continue;

            // Ensure imp is tracked
            if (!_impDigTargets.ContainsKey(creatureId))
            {
                _impDigTargets[creatureId] = null;
                _impPaths[creatureId] = null;
                _impPathIndex[creatureId] = 0;
            }

            TickSingleImp(creatureId, entity, movement, gameTime);
        }
    }

    private void TickSingleImp(EntityId impId, IEntity entity, MovementComponent movement, GameTime gameTime)
    {
        var currentPos = movement.CurrentPosition;

        if (_impDigTargets.TryGetValue(impId, out var target) && target != null)
        {
            var targetTile = _session.Map.GetTile(target.Value);

            if (targetTile == null || !targetTile.IsMarkedForDigging || !_session.Map.IsDiggable(target.Value))
            {
                ClearImpTarget(impId);
                return;
            }

            if (currentPos.ManhattanDistanceTo(target.Value) == 1)
            {
                targetTile.Health -= DigDamagePerTick;

                if (targetTile.Health <= 0)
                {
                    CompleteDig(targetTile, target.Value);
                    ClearImpTarget(impId);
                }
                return;
            }

            if (FollowPath(impId, entity, movement))
                return;

            var path = _pathfinding.FindPathAdjacentTo(currentPos, target.Value);
            if (path == null || path.Count == 0)
            {
                ClearImpTarget(impId);
                return;
            }

            _impPaths[impId] = path;
            _impPathIndex[impId] = 1;
            FollowPath(impId, entity, movement);
            return;
        }

        var nearestMarked = FindNearestMarkedTile(currentPos);
        if (nearestMarked != null)
        {
            _impDigTargets[impId] = nearestMarked;
            var path = _pathfinding.FindPathAdjacentTo(currentPos, nearestMarked.Value);
            if (path != null && path.Count > 0)
            {
                _impPaths[impId] = path;
                _impPathIndex[impId] = 1;
            }
            return;
        }

        IdleWander(impId, entity, movement, gameTime);
    }

    private bool FollowPath(EntityId impId, IEntity entity, MovementComponent movement)
    {
        if (!_impPaths.TryGetValue(impId, out var path) || path == null) return false;
        if (!_impPathIndex.TryGetValue(impId, out var idx)) return false;
        if (idx >= path.Count) return false;

        var nextPos = path[idx];

        if (!_session.Map.IsPassable(nextPos))
        {
            _impPaths[impId] = null;
            return false;
        }

        var oldPos = movement.CurrentPosition;
        movement.CurrentPosition = nextPos;
        _impPathIndex[impId] = idx + 1;

        var presenter = _session.PresentationFactory.CreateCreaturePresenter(entity.Id, "Imp");
        presenter.OnMoved(entity.Id, oldPos, nextPos);

        return true;
    }

    private void CompleteDig(Tile tile, TileCoordinate coord)
    {
        var player = _session.Players[0];

        if (tile.Type == TileType.Gold && tile.GoldRemaining > 0)
        {
            player.Dungeon.Gold.Add(tile.GoldRemaining);
            tile.GoldRemaining = 0;
        }

        tile.Type = TileType.ClaimedPath;
        tile.OwnerId = player.Id;
        tile.IsMarkedForDigging = false;
        tile.Health = 100f;

        var mapPresenter = _factory.MapPresenter;
        mapPresenter.OnTileUnmarked(coord);
        mapPresenter.OnTileDug(coord);

        _pathfinding = new AStarPathfinding(_session.Map);
    }

    private void ClearImpTarget(EntityId impId)
    {
        _impDigTargets[impId] = null;
        _impPaths[impId] = null;
        _impPathIndex[impId] = 0;
    }

    private TileCoordinate? FindNearestMarkedTile(TileCoordinate from)
    {
        TileCoordinate? nearest = null;
        int bestDist = int.MaxValue;

        for (int x = 0; x < _session.Map.Width; x++)
        {
            for (int y = 0; y < _session.Map.Height; y++)
            {
                var coord = new TileCoordinate(x, y);
                var tile = _session.Map.GetTile(coord);
                if (tile == null || !tile.IsMarkedForDigging) continue;

                bool alreadyTargeted = false;
                foreach (var kvp in _impDigTargets)
                {
                    if (kvp.Value == coord)
                    {
                        alreadyTargeted = true;
                        break;
                    }
                }
                if (alreadyTargeted) continue;

                int dist = from.ManhattanDistanceTo(coord);
                if (dist < bestDist)
                {
                    bestDist = dist;
                    nearest = coord;
                }
            }
        }

        return nearest;
    }

    private void IdleWander(EntityId impId, IEntity entity, MovementComponent movement, GameTime gameTime)
    {
        if (gameTime.TickNumber % 30 != (impId.Value.GetHashCode() & 0x7FFFFFFF) % 30)
            return;

        var currentPos = movement.CurrentPosition;
        var neighbors = currentPos.GetNeighbors();
        var passable = new List<TileCoordinate>();

        foreach (var n in neighbors)
        {
            if (_session.Map.IsPassable(n))
                passable.Add(n);
        }

        if (passable.Count == 0) return;

        var rng = new Random(gameTime.TickNumber + impId.Value.GetHashCode());
        var target = passable[rng.Next(passable.Count)];

        var oldPos = movement.CurrentPosition;
        movement.CurrentPosition = target;

        var presenter = _session.PresentationFactory.CreateCreaturePresenter(entity.Id, "Imp");
        presenter.OnMoved(entity.Id, oldPos, target);
    }

    private void TickResources(GameTime gameTime)
    {
        foreach (var player in _session.Players)
        {
            var mana = player.Dungeon.Mana;
            if (mana.NetRate > 0)
            {
                int manaGain = (int)(mana.NetRate * _session.Clock.TickDurationSeconds);
                if (manaGain > 0)
                    mana.Add(manaGain);
            }
        }
    }

    private void TickPortalSpawning(GameTime gameTime)
    {
        if (_currentLevel == null || _session.Players.Count == 0) return;

        var player = _session.Players[0];
        var dungeon = player.Dungeon;
        var availability = _currentLevel.Availability;

        // Check prerequisites: player must have a portal, lair, and hatchery
        bool hasPortal = dungeon.HasRoom(RoomType.Portal);
        bool hasLair = dungeon.HasRoom(RoomType.Lair);
        bool hasHatchery = dungeon.HasRoom(RoomType.Hatchery);

        if (!hasPortal || !hasLair || !hasHatchery) return;
        if (availability.AvailableCreatures.Count == 0) return;

        _portalSpawnAccumulator += _session.Clock.TickDurationSeconds;

        if (_portalSpawnAccumulator < PortalSpawnIntervalSeconds) return;
        _portalSpawnAccumulator -= PortalSpawnIntervalSeconds;

        // Find a portal room to spawn at
        TileCoordinate? spawnPos = null;
        foreach (var room in dungeon.OwnedRooms)
        {
            if (room.Type == RoomType.Portal && room.IsOperational && room.Tiles.Count > 0)
            {
                // Spawn at center tile of portal
                spawnPos = room.Tiles[room.Tiles.Count / 2];
                break;
            }
        }

        if (spawnPos == null) return;

        // Pick a random creature type from available list
        var rng = new Random(gameTime.TickNumber);
        var creatureType = availability.AvailableCreatures[rng.Next(availability.AvailableCreatures.Count)];

        SpawnCreatureAtPortal(player, creatureType, spawnPos.Value);
    }

    private void SpawnCreatureAtPortal(Player player, CreatureType creatureType, TileCoordinate position)
    {
        var creature = new Entity();
        var typeName = creatureType.ToString();

        // Base stats vary by creature type
        var (hp, speed, attack, damage, defense) = creatureType switch
        {
            CreatureType.Goblin => (80, 1.5f, 8, 5, 3),
            CreatureType.Firefly => (40, 3.0f, 4, 2, 1),
            CreatureType.Warlock => (60, 1.0f, 6, 8, 2),
            CreatureType.Troll => (120, 1.0f, 10, 8, 5),
            CreatureType.DarkElf => (70, 2.0f, 9, 6, 3),
            _ => (60, 1.5f, 6, 4, 2),
        };

        creature.AddComponent(new CreatureIdentityComponent
        {
            CreatureType = creatureType,
            Faction = CreatureFaction.Keeper,
            OwnerId = player.Id
        });
        creature.AddComponent(new StatsComponent
        {
            CurrentHealth = hp,
            MaxHealth = hp,
            Speed = speed,
            MeleeAttack = attack,
            MeleeDamage = damage,
            Defense = defense
        });
        creature.AddComponent(new MovementComponent
        {
            CurrentPosition = position
        });

        _session.Entities.Register(creature);
        player.Dungeon.AddCreature(creature.Id);

        var presenter = _session.PresentationFactory.CreateCreaturePresenter(creature.Id, typeName);
        presenter.OnSpawned(creature.Id, position);

        GD.Print($"{typeName} arrived through the portal!");
    }

    private List<RoomDefinition> GetAvailableRoomDefinitions(LevelAvailability availability)
    {
        var result = new List<RoomDefinition>();
        foreach (var roomType in availability.AvailableRooms)
        {
            try
            {
                result.Add(_roomRegistry.Get(roomType));
            }
            catch (KeyNotFoundException)
            {
                // Room type not in registry — skip
            }
        }
        return result;
    }
}
