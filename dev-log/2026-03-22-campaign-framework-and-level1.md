# 2026-03-22 — Campaign Framework & Level 1 Implementation

## What was built

### Stage 0 — Campaign Framework

**New project:** `DungeonKeeper.DataModel/src/DungeonKeeper.Campaign/` (16 files)

**Data models:**
- `LevelDefinition` — central class defining a level (map, availability, conditions, waves)
- `CampaignDefinition` — ordered list of levels
- `LevelAvailability` — filters rooms/spells/creatures/traps/doors per level
- `MapBlueprint` — sparse tile-based map definition with helpers (`AddGoldCluster`, `AddGemCluster`, `AddPool`, `AddImpenetrableBorder`)
- `TileBlueprintEntry` — single tile in a blueprint
- `PlayerStartingPosition` — dungeon heart center + claimed area radius
- `HeroGateDefinition` — hero spawn point on map
- `EnemyKeeperDefinition` — AI rival keeper placement
- `WaveDefinition` — hero invasion wave (reuses existing `InvasionGroup`)

**Condition system:**
- `IVictoryCondition` / `IDefeatCondition` interfaces
- `LevelOutcome` enum (InProgress, Victory, Defeat)
- `VictoryConditionEvaluator` — checks defeat first, then all victory conditions
- Concrete conditions: `AttractCreaturesCondition`, `AccumulateGoldCondition`, `DestroyAllEnemyHeartsCondition`, `SurviveForDurationCondition`, `DungeonHeartDestroyedCondition`, `TimeLimitDefeatCondition`

**Wave system:**
- `WaveScheduleBuilder` — bridges campaign `WaveDefinition` data to existing `InvasionScheduler`

**Architecture decision — circular dependency resolution:**
Campaign depends on GameState (conditions need `GameSession`). GameState does NOT depend on Campaign. The Godot root project references both. `GameSession` has no knowledge of campaign — `LevelDefinition` is held by `GameBootstrap` on the Godot side.

### Stage 1, Level 1 — Eversmile

**Level definition (`CampaignLevelRegistry`):**
- 50x50 map with impenetrable borders
- Abundant gold clusters close to start (6 clusters), gem clusters farther out (2 clusters)
- Small water pool
- Starting resources: 15,000 gold, 500 mana, 4 imps
- Available rooms: Lair, Hatchery, Treasury, Portal
- Available spells: CreateImp
- Available creatures: Goblin, Firefly
- Victory condition: attract 8 total creatures (4 imps + 4 attracted)
- Defeat condition: dungeon heart destroyed
- No hero invasions (pure tutorial level)

**Refactored `MapInitializer`:**
- New overload: `Initialize(GameSession, LevelDefinition)` — reads blueprint, carves starting area, spawns imps, schedules waves
- Original sandbox `Initialize(GameSession)` preserved for free-play

**Rewrote `GameBootstrap`:**
- Full game phase management: LevelSelect → Briefing → Playing → Ended → back to LevelSelect
- `VictoryConditionEvaluator` runs each tick during gameplay
- Tutorial only runs on Level 1
- `ClearGameWorld()` cleans up 3D nodes and UI between levels

**3 new UI screens:**
- `LevelSelectScreen` — dark themed level list with styled buttons, signals `LevelSelected(int)`
- `BriefingScreen` — level name, briefing text, "Begin Conquest" button
- `LevelCompleteOverlay` — victory (gold border) or defeat (red border) overlay with return button

## Files created

```
DungeonKeeper.DataModel/src/DungeonKeeper.Campaign/
├── DungeonKeeper.Campaign.csproj
├── LevelDefinition.cs
├── CampaignDefinition.cs
├── Availability/
│   └── LevelAvailability.cs
├── Conditions/
│   ├── IVictoryCondition.cs
│   ├── IDefeatCondition.cs
│   ├── LevelOutcome.cs
│   ├── VictoryConditionEvaluator.cs
│   ├── AttractCreaturesCondition.cs
│   ├── AccumulateGoldCondition.cs
│   ├── DestroyAllEnemyHeartsCondition.cs
│   ├── SurviveForDurationCondition.cs
│   ├── DungeonHeartDestroyedCondition.cs
│   └── TimeLimitDefeatCondition.cs
├── MapBlueprint/
│   ├── MapBlueprint.cs
│   ├── TileBlueprintEntry.cs
│   ├── PlayerStartingPosition.cs
│   ├── HeroGateDefinition.cs
│   └── EnemyKeeperDefinition.cs
├── Waves/
│   ├── WaveDefinition.cs
│   └── WaveScheduleBuilder.cs
└── Levels/
    └── CampaignLevelRegistry.cs

scripts/UI/
├── LevelSelectScreen.cs (new)
├── BriefingScreen.cs (new)
└── LevelCompleteOverlay.cs (new)
```

## Files modified

- `DungeonKeeper.DataModel/DungeonKeeper.DataModel.sln` — added Campaign project
- `dungeon-keeper.csproj` — added Campaign project reference
- `scripts/Bootstrap/GameBootstrap.cs` — rewrote with phase management and campaign flow
- `scripts/Bootstrap/MapInitializer.cs` — added LevelDefinition overload, parameterized helpers
- `scripts/UI/TutorialDialog.cs` — fixed FullRect anchors (from earlier session)

## Verification

- `dotnet build` — succeeds for both DataModel solution and Godot root project
- `dotnet test` — all 128 existing tests pass
- No regressions
