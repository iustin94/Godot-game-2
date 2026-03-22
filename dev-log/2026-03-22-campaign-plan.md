# Campaign & Level Progression System

## Review

### Current State
- All visuals are **procedural** — colored BoxMesh, CapsuleMesh, SphereMesh, CylinderMesh with StandardMaterial3D. No external 3D models, textures, or audio files exist.
- Audio presenter interface exists (`GodotAudioPresenter`) but only logs to console — no actual audio playback.
- The data model is comprehensive (creatures, rooms, spells, traps, doors, combat, research, invasions) but there is no campaign framework, no victory/defeat conditions, no level selection, and no hero spawning logic.

### Short-Term Goal
Get a playable campaign with levels 1-4 (tutorial arc) working end-to-end with the existing procedural graphics. This validates the entire campaign pipeline — level loading, availability filtering, victory conditions, and basic hero invasions — before investing in art assets.

### Long-Term Goal
A complete 20-level campaign with proper 3D models, textures, audio, UI screens (main menu, level select, briefing, victory/defeat), and AI keeper opponents.

---

## Stage 0: Campaign Framework (no new assets needed)

**Goal:** Build the `DungeonKeeper.Campaign` data model and pipeline so levels can be defined and loaded. All existing procedural visuals continue to work.

### Code Work
1. Create `DungeonKeeper.Campaign` csproj
2. Implement data model classes: `LevelDefinition`, `CampaignDefinition`, `LevelAvailability`, `MapBlueprint`, `TileBlueprintEntry`, `PlayerStartingPosition`, `HeroGateDefinition`, `EnemyKeeperDefinition`, `WaveDefinition`
3. Implement condition system: `IVictoryCondition`, `IDefeatCondition`, `LevelOutcome`, `VictoryConditionEvaluator`, and 6 concrete condition classes
4. Implement `WaveScheduleBuilder` (bridge to existing `InvasionScheduler`)
5. Add `LevelDefinition?` to `GameSession`
6. Refactor `MapInitializer` to accept `LevelDefinition`, keeping current sandbox mode as fallback
7. Update `GameBootstrap` to load a level definition and run `VictoryConditionEvaluator` each tick

### Assets Required
None — uses existing procedural graphics.

### UI Work
- Victory overlay (text + "Next Level" button) — can be built programmatically like `TutorialDialog`
- Defeat overlay (text + "Retry" button)

---

## Stage 1: Tutorial Arc (Levels 1-4)

**Goal:** First 4 playable levels introducing core mechanics one at a time. Player learns to dig, build rooms, attract creatures, research, train, and manufacture.

### Levels

| # | Name | Introduces | Victory |
|---|------|------------|---------|
| 1 | Eversmile | Digging, Lair, Hatchery, Treasury, Portal, CreateImp spell | Attract 4 creatures |
| 2 | Cosyton | Library, research, Warlock | Destroy hero fortress |
| 3 | Waterdream Warm | TrainingRoom, Heal/SightOfEvil spells, Troll, Alarm trap | Destroy hero fortress |
| 4 | Flowerhat | Workshop, manufacturing, DarkElf, Wooden door, Sentry trap | Destroy hero fortress |

### Code Work
- Define 4 level blueprints in `CampaignLevelRegistry`
- Hero fortress: pre-built hero area with a destructible "Hero Heart" tile/room
- Hero gate spawning logic: spawn `InvasionGroup` creatures at gate location, pathfind toward dungeon heart
- Level select screen (simple list for now)
- Level briefing screen (show `BriefingText` before level starts)

### Assets Required

#### 3D Models
| Asset | Description | Format | Priority |
|-------|-------------|--------|----------|
| `dungeon_heart.glb` | Pulsing heart structure, 3x3 tile footprint | GLTF | High |
| `imp.glb` | Small worker creature, idle + walk + dig animations | GLTF | High |
| `goblin.glb` | Small melee fighter, idle + walk + attack animations | GLTF | High |
| `firefly.glb` | Tiny flying creature with glow, idle + fly animations | GLTF | Medium |
| `warlock.glb` | Robed spellcaster, idle + walk + cast animations | GLTF | Medium |
| `troll.glb` | Large brute, idle + walk + attack + workshop-work animations | GLTF | Medium |
| `dark_elf.glb` | Lean ranged fighter, idle + walk + shoot animations | GLTF | Medium |
| `dwarf.glb` | Hero — stocky miner/fighter, idle + walk + attack | GLTF | Medium |
| `thief.glb` | Hero — agile rogue, idle + walk + attack | GLTF | Low |
| `guard.glb` | Hero — armored soldier, idle + walk + attack | GLTF | Low |
| `fairy.glb` | Hero — flying healer with glow | GLTF | Low |
| `hero_gate.glb` | Stone archway portal where heroes spawn | GLTF | Medium |
| `portal.glb` | Dark swirling portal where keeper creatures arrive | GLTF | Medium |
| `wooden_door.glb` | Simple plank door, open/close states | GLTF | Low |
| `sentry_trap.glb` | Small crossbow turret on floor | GLTF | Low |
| `alarm_trap.glb` | Bell/gong on the ground | GLTF | Low |

#### Tile Textures (tiling, 512x512 or 1024x1024)
| Asset | Description | Format |
|-------|-------------|--------|
| `earth.png` | Brown packed dirt with rock fragments | PNG |
| `claimed_path.png` | Dark stone floor with subtle pattern | PNG |
| `gold_vein.png` | Rock with gold ore veins | PNG |
| `gem_vein.png` | Rock with purple crystal clusters | PNG |
| `reinforced_wall.png` | Dark reinforced stone | PNG |
| `impenetrable.png` | Black obsidian-like rock | PNG |
| `water.png` | Dark underground water (animated UV optional) | PNG |
| `lava.png` | Molten rock with glow (animated UV optional) | PNG |

#### Room Floor Textures (tiling, 512x512)
| Asset | Description | Format |
|-------|-------------|--------|
| `lair_floor.png` | Straw/nest bedding | PNG |
| `hatchery_floor.png` | Wet stone with chicken feathers | PNG |
| `treasury_floor.png` | Gold-inlaid stone | PNG |
| `library_floor.png` | Wooden planks with book debris | PNG |
| `training_room_floor.png` | Worn stone with weapon marks | PNG |
| `workshop_floor.png` | Wooden workbench planks | PNG |

#### UI Graphics
| Asset | Description | Format |
|-------|-------------|--------|
| `icon_gold.png` | Gold coin icon for HUD | PNG 32x32 |
| `icon_mana.png` | Blue mana crystal icon for HUD | PNG 32x32 |
| `icon_creatures.png` | Creature silhouette icon for HUD | PNG 32x32 |
| `room_icons/*.png` | Icon per room type for build panel (6 rooms) | PNG 64x64 |
| `spell_icons/create_imp.png` | Create Imp spell icon | PNG 64x64 |
| `spell_icons/heal.png` | Heal spell icon | PNG 64x64 |
| `spell_icons/sight_of_evil.png` | Sight of Evil spell icon | PNG 64x64 |
| `briefing_bg.png` | Parchment/scroll background for level briefing | PNG 800x600 |
| `level_select_bg.png` | Dark underground map for level select | PNG 1920x1080 |
| `victory_banner.png` | "Victory" banner graphic | PNG 512x128 |
| `defeat_banner.png` | "Defeat" banner graphic | PNG 512x128 |

#### Audio — Sound Effects
| Asset | Description | Format | Trigger |
|-------|-------------|--------|---------|
| `sfx_dig.ogg` | Pick hitting rock | OGG | Imp digging a tile |
| `sfx_tile_collapse.ogg` | Rock crumbling/falling | OGG | Tile fully dug out |
| `sfx_gold_collect.ogg` | Coins clinking | OGG | Gold mined from tile |
| `sfx_claim_tile.ogg` | Stone sealing/claiming sound | OGG | Tile claimed |
| `sfx_room_build.ogg` | Construction thud | OGG | Room placed |
| `sfx_creature_spawn.ogg` | Magical whoosh | OGG | Creature arrives at portal |
| `sfx_imp_acknowledge.ogg` | Small grunt/chirp | OGG | Imp given task |
| `sfx_button_click.ogg` | UI click | OGG | Any UI button pressed |
| `sfx_button_hover.ogg` | Subtle UI hover | OGG | Mouse over UI button |
| `sfx_melee_hit.ogg` | Blunt impact | OGG | Melee attack connects |
| `sfx_creature_death.ogg` | Death groan | OGG | Creature dies |
| `sfx_spell_cast.ogg` | Magic sparkle | OGG | Any spell cast |
| `sfx_victory.ogg` | Triumphant fanfare (3-5s) | OGG | Level victory |
| `sfx_defeat.ogg` | Dark ominous stinger (3-5s) | OGG | Level defeat |
| `sfx_alarm.ogg` | Bell ringing | OGG | Alarm trap triggered |
| `sfx_sentry_fire.ogg` | Crossbow bolt launch | OGG | Sentry trap fires |
| `sfx_door_open.ogg` | Wood creaking open | OGG | Door opened |
| `sfx_door_close.ogg` | Wood slamming shut | OGG | Door closed |

#### Audio — Music
| Asset | Description | Format | Usage |
|-------|-------------|--------|-------|
| `music_menu.ogg` | Dark ambient, mysterious (looping) | OGG | Main menu / level select |
| `music_gameplay_calm.ogg` | Brooding dungeon ambience (looping, 2-3 min) | OGG | Normal gameplay, no combat |
| `music_gameplay_combat.ogg` | Intense percussion-heavy (looping, 2-3 min) | OGG | During combat / invasions |
| `music_briefing.ogg` | Ominous narrative music (30-60s, looping) | OGG | Level briefing screen |

#### Audio — Mentor Voice Lines
| Asset | Description | Format |
|-------|-------------|--------|
| `mentor_welcome.ogg` | "Welcome, Keeper..." | OGG |
| `mentor_claim_territory.ogg` | "Claim territory by digging..." | OGG |
| `mentor_creatures_arriving.ogg` | "Creatures are arriving at your portal..." | OGG |
| `mentor_under_attack.ogg` | "Your dungeon is under attack!" | OGG |
| `mentor_heart_damaged.ogg` | "Your dungeon heart is taking damage!" | OGG |
| `mentor_victory.ogg` | "This realm is yours, Keeper." | OGG |

---

## Stage 2: Rising Threat (Levels 5-7)

**Goal:** Introduce defensive mechanics (guard rooms, prison, torture), stronger hero types, and water traversal. Player learns to defend proactively.

### Levels

| # | Name | Introduces | Victory |
|---|------|------------|---------|
| 5 | Lushmeadow | GuardRoom, Lightning spell, Rogue, SpikeTrap, Braced door, Knights | Destroy hero fortress |
| 6 | Snuggledell | Prison, Possession/SpeedMonster spells, DarkMistress, Boulder, Steel door | Destroy hero fortress |
| 7 | Moonbrush Wood | TortureChamber, WoodenBridge, ProtectMonster spell, Skeleton, PoisonGas | Destroy hero fortress |

### New Assets Required

#### 3D Models
| Asset | Description | Format |
|-------|-------------|--------|
| `rogue.glb` | Hooded assassin, idle + walk + backstab animations | GLTF |
| `dark_mistress.glb` | Whip-wielding dominatrix, idle + walk + attack | GLTF |
| `skeleton.glb` | Undead warrior, idle + walk + attack (rises from ground) | GLTF |
| `knight.glb` | Hero — heavy armored, idle + walk + sword attack | GLTF |
| `wizard.glb` | Hero — robed with staff, idle + walk + cast | GLTF |
| `spike_trap.glb` | Floor spikes that retract/extend | GLTF |
| `boulder_trap.glb` | Large rolling boulder | GLTF |
| `poison_gas_trap.glb` | Vented floor panel with green mist | GLTF |
| `braced_door.glb` | Iron-reinforced wooden door | GLTF |
| `steel_door.glb` | Full steel door | GLTF |
| `wooden_bridge.glb` | Plank bridge tile (for over water) | GLTF |
| `prison_cage.glb` | Iron cage/cell for imprisoned creatures | GLTF |
| `torture_device.glb` | Rack or wheel device | GLTF |

#### Room Floor Textures
| Asset | Description | Format |
|-------|-------------|--------|
| `guard_room_floor.png` | Patrolled stone with flags | PNG |
| `prison_floor.png` | Cold damp stone with chains | PNG |
| `torture_chamber_floor.png` | Dark stone with blood stains | PNG |

#### UI Graphics
| Asset | Description | Format |
|-------|-------------|--------|
| `room_icons/guard_room.png` | Guard room build icon | PNG 64x64 |
| `room_icons/prison.png` | Prison build icon | PNG 64x64 |
| `room_icons/torture_chamber.png` | Torture chamber build icon | PNG 64x64 |
| `room_icons/wooden_bridge.png` | Bridge build icon | PNG 64x64 |
| `spell_icons/lightning.png` | Lightning spell icon | PNG 64x64 |
| `spell_icons/possession.png` | Possession spell icon | PNG 64x64 |
| `spell_icons/speed_monster.png` | Speed Monster spell icon | PNG 64x64 |
| `spell_icons/protect_monster.png` | Protect Monster spell icon | PNG 64x64 |
| `trap_icons/*.png` | Icons for SpikeTrap, Boulder, PoisonGas (3 icons) | PNG 64x64 |
| `door_icons/*.png` | Icons for Braced, Steel doors (2 icons) | PNG 64x64 |

#### Audio — Sound Effects
| Asset | Description | Format |
|-------|-------------|--------|
| `sfx_lightning.ogg` | Electric crack and sizzle | OGG |
| `sfx_possession_enter.ogg` | Ethereal whoosh (entering creature) | OGG |
| `sfx_possession_exit.ogg` | Reverse whoosh (leaving creature) | OGG |
| `sfx_speed_buff.ogg` | Quick ascending tone | OGG |
| `sfx_protection_buff.ogg` | Shield shimmer | OGG |
| `sfx_spike_trap.ogg` | Metal spikes shooting up | OGG |
| `sfx_boulder_roll.ogg` | Heavy rumbling roll | OGG |
| `sfx_poison_gas.ogg` | Hissing gas release | OGG |
| `sfx_prisoner_captured.ogg` | Cage slamming | OGG |
| `sfx_torture.ogg` | Creaking rack + groan | OGG |
| `sfx_creature_converted.ogg` | Dark magical transformation | OGG |
| `sfx_steel_door.ogg` | Heavy metal door slam | OGG |

#### Audio — Mentor Voice Lines
| Asset | Description | Format |
|-------|-------------|--------|
| `mentor_prison_full.ogg` | "Your prison is full..." | OGG |
| `mentor_creature_converted.ogg` | "A hero has been converted to your cause!" | OGG |
| `mentor_research_complete.ogg` | "Your warlocks have completed their research." | OGG |

---

## Stage 3: Rival Keepers (Levels 8-11)

**Goal:** Introduce enemy AI keepers with their own dungeons. Player must manage offense and defense simultaneously. Lava terrain and advanced creature types appear.

### Levels

| # | Name | Introduces | Victory |
|---|------|------------|---------|
| 8 | Tickle | CombatPit, CallToArms, Salamander, LightningTrap, 1st rival keeper | Destroy enemy keeper heart |
| 9 | Moonbrush Glade | Graveyard, CaveIn, Vampire, Freeze trap | Destroy enemy keeper heart |
| 10 | Nevergrim | Casino, Turncoat, BileDemon, FearTrap, Magic door | Destroy enemy keeper heart |
| 11 | Hearth | StoneBridge, Inferno, BlackKnight, Fireburst trap | Destroy enemy keeper heart |

### Code Work (significant)
- **AI Keeper system**: enemy keeper that builds rooms, trains creatures, and attacks the player
- **Fog of war per player**: each keeper only sees their own territory
- **Enemy dungeon heart**: destructible target with health bar shown when visible

### New Assets Required

#### 3D Models
| Asset | Description | Format |
|-------|-------------|--------|
| `salamander.glb` | Fire lizard, idle + walk + fire-breath attack | GLTF |
| `vampire.glb` | Caped undead lord, idle + walk + bite attack + bat-transform | GLTF |
| `bile_demon.glb` | Bloated demon, idle + waddle + gas attack | GLTF |
| `black_knight.glb` | Dark armored knight, idle + walk + heavy swing | GLTF |
| `monk.glb` | Hero — martial artist, idle + walk + kick | GLTF |
| `elven_archer.glb` | Hero — elf with bow, idle + walk + shoot | GLTF |
| `giant.glb` | Hero — oversized brute, idle + walk + stomp | GLTF |
| `samurai.glb` | Hero — katana wielder, idle + walk + slash | GLTF |
| `lightning_trap.glb` | Tesla coil trap | GLTF |
| `freeze_trap.glb` | Ice crystal trap | GLTF |
| `fear_trap.glb` | Skull totem that emits fear | GLTF |
| `fireburst_trap.glb` | Fire vent trap | GLTF |
| `magic_door.glb` | Glowing enchanted door | GLTF |
| `stone_bridge.glb` | Stone bridge tile (for over lava) | GLTF |
| `gravestone.glb` | Tombstone prop for graveyard room | GLTF |
| `combat_pit_post.glb` | Arena post/pillar for combat pit | GLTF |
| `slot_machine.glb` | Casino gambling device | GLTF |
| `enemy_dungeon_heart.glb` | Rival keeper's heart (different color/style from player's) | GLTF |

#### Room Floor Textures
| Asset | Description | Format |
|-------|-------------|--------|
| `combat_pit_floor.png` | Sandy arena surface | PNG |
| `graveyard_floor.png` | Dark earth with bones | PNG |
| `casino_floor.png` | Decorated stone with patterns | PNG |

#### Particle Effects
| Asset | Description | Format |
|-------|-------------|--------|
| `vfx_cave_in.tres` | Falling rocks particle system | Godot ParticlesMaterial |
| `vfx_inferno.tres` | Large fire explosion particles | Godot ParticlesMaterial |
| `vfx_freeze.tres` | Ice crystal expansion particles | Godot ParticlesMaterial |
| `vfx_turncoat.tres` | Dark-to-light color shift particles | Godot ParticlesMaterial |
| `vfx_call_to_arms.tres` | Rally banner/flag particles | Godot ParticlesMaterial |

#### UI Graphics
| Asset | Description | Format |
|-------|-------------|--------|
| `room_icons/combat_pit.png` | Combat pit build icon | PNG 64x64 |
| `room_icons/graveyard.png` | Graveyard build icon | PNG 64x64 |
| `room_icons/casino.png` | Casino build icon | PNG 64x64 |
| `room_icons/stone_bridge.png` | Stone bridge build icon | PNG 64x64 |
| `spell_icons/call_to_arms.png` | Call to Arms spell icon | PNG 64x64 |
| `spell_icons/cave_in.png` | Cave-In spell icon | PNG 64x64 |
| `spell_icons/turncoat.png` | Turncoat spell icon | PNG 64x64 |
| `spell_icons/inferno.png` | Inferno spell icon | PNG 64x64 |
| `trap_icons/*.png` | Icons for LightningTrap, Freeze, Fear, Fireburst (4 icons) | PNG 64x64 |
| `door_icons/magic.png` | Magic door icon | PNG 64x64 |
| `enemy_keeper_portrait.png` | Rival keeper face for notifications | PNG 128x128 |

#### Audio — Sound Effects
| Asset | Description | Format |
|-------|-------------|--------|
| `sfx_cave_in.ogg` | Massive rock collapse | OGG |
| `sfx_inferno.ogg` | Roaring fire explosion | OGG |
| `sfx_freeze_trap.ogg` | Ice crystallization crack | OGG |
| `sfx_fear_trap.ogg` | Ghostly wail | OGG |
| `sfx_fireburst.ogg` | Fire jet burst | OGG |
| `sfx_lightning_trap.ogg` | Electric arc discharge | OGG |
| `sfx_turncoat.ogg` | Dark magical charm sound | OGG |
| `sfx_call_to_arms.ogg` | War horn blast | OGG |
| `sfx_vampire_bite.ogg` | Quick bite with hiss | OGG |
| `sfx_bile_demon_gas.ogg` | Disgusting gas release | OGG |
| `sfx_casino_win.ogg` | Slot machine jackpot | OGG |
| `sfx_casino_lose.ogg` | Disappointing clunk | OGG |
| `sfx_enemy_heart_hit.ogg` | Enemy heart taking damage | OGG |
| `sfx_enemy_heart_destroyed.ogg` | Massive explosion/collapse | OGG |

#### Audio — Music
| Asset | Description | Format |
|-------|-------------|--------|
| `music_gameplay_tense.ogg` | Building tension, rival keeper nearby (looping, 2-3 min) | OGG |
| `music_enemy_defeated.ogg` | Short triumphant sting when enemy keeper falls (5-10s) | OGG |

#### Audio — Mentor Voice Lines
| Asset | Description | Format |
|-------|-------------|--------|
| `mentor_rival_keeper.ogg` | "An enemy keeper dwells in this realm..." | OGG |
| `mentor_enemy_breached.ogg` | "The enemy has breached your defenses!" | OGG |
| `mentor_enemy_heart_found.ogg` | "You have found the enemy's dungeon heart!" | OGG |
| `mentor_enemy_defeated.ogg` | "The rival keeper has been vanquished!" | OGG |

---

## Stage 4: Endgame Preparation (Levels 12-15)

**Goal:** Multi-keeper maps, resource scarcity, and the most powerful creatures (Dragon, Horned Reaper). All game mechanics are now available.

### Levels

| # | Name | Introduces | Victory |
|---|------|------------|---------|
| 12 | Elf's Dance | Temple, Tremor, Dragon, JackInTheBox, 2 rival keepers | Destroy both hearts |
| 13 | Buffy Oak | CreateGold spell, Secret door, gold-scarce map | Destroy both hearts |
| 14 | Sleepiburgh | Trigger trap, RoyalGuard heroes, time-survival objective | Destroy keeper + survive 15 min |
| 15 | Woodly Rhyme | SummonHornedReaper spell, Horned Reaper creature | Destroy both hearts |

### New Assets Required

#### 3D Models
| Asset | Description | Format |
|-------|-------------|--------|
| `dragon.glb` | Large winged dragon, idle + walk + fly + fire-breath (elite creature) | GLTF |
| `horned_reaper.glb` | Massive demon with scythe, idle + walk + devastating attacks (most complex model) | GLTF |
| `royal_guard.glb` | Hero — elite knight with cape, idle + walk + attack | GLTF |
| `jack_in_the_box_trap.glb` | Pop-up scare trap | GLTF |
| `trigger_trap.glb` | Floor pressure plate | GLTF |
| `secret_door.glb` | Wall segment that opens (looks like reinforced wall) | GLTF |
| `temple_altar.glb` | Dark altar/shrine prop for temple room | GLTF |

#### Room Floor Textures
| Asset | Description | Format |
|-------|-------------|--------|
| `temple_floor.png` | Dark ritual stone with runes | PNG |

#### Particle Effects
| Asset | Description | Format |
|-------|-------------|--------|
| `vfx_tremor.tres` | Ground shaking with dust and rock debris | Godot ParticlesMaterial |
| `vfx_create_gold.tres` | Gold sparkle transmutation effect | Godot ParticlesMaterial |
| `vfx_summon_reaper.tres` | Dark hellfire portal summoning effect | Godot ParticlesMaterial |
| `vfx_dragon_breath.tres` | Fire stream particles | Godot ParticlesMaterial |

#### UI Graphics
| Asset | Description | Format |
|-------|-------------|--------|
| `room_icons/temple.png` | Temple build icon | PNG 64x64 |
| `spell_icons/tremor.png` | Tremor spell icon | PNG 64x64 |
| `spell_icons/create_gold.png` | Create Gold spell icon | PNG 64x64 |
| `spell_icons/summon_horned_reaper.png` | Summon Horned Reaper spell icon | PNG 64x64 |
| `trap_icons/jack_in_the_box.png` | JackInTheBox trap icon | PNG 64x64 |
| `trap_icons/trigger.png` | Trigger trap icon | PNG 64x64 |
| `door_icons/secret.png` | Secret door icon | PNG 64x64 |
| `timer_hud.png` | Countdown timer frame for survival objective | PNG 128x64 |

#### Audio — Sound Effects
| Asset | Description | Format |
|-------|-------------|--------|
| `sfx_tremor.ogg` | Deep earthquake rumble | OGG |
| `sfx_create_gold.ogg` | Magical transmutation shimmer | OGG |
| `sfx_summon_reaper.ogg` | Hellish roar + fire crackle | OGG |
| `sfx_dragon_roar.ogg` | Dragon roar | OGG |
| `sfx_dragon_breath.ogg` | Sustained fire breath | OGG |
| `sfx_reaper_scythe.ogg` | Heavy scythe swing | OGG |
| `sfx_reaper_angry.ogg` | Reaper becoming enraged (if neglected) | OGG |
| `sfx_jack_in_the_box.ogg` | Spring pop + scream | OGG |
| `sfx_temple_prayer.ogg` | Dark chanting loop | OGG |

#### Audio — Mentor Voice Lines
| Asset | Description | Format |
|-------|-------------|--------|
| `mentor_dragon_arrived.ogg` | "A dragon has come to your dungeon!" | OGG |
| `mentor_reaper_summoned.ogg` | "The Horned Reaper walks among us..." | OGG |
| `mentor_reaper_angry.ogg` | "The Horned Reaper is angry! Beware!" | OGG |
| `mentor_gold_low.ogg` | "Your treasury is running low, Keeper." | OGG |
| `mentor_survive_timer.ogg` | "You must hold out a little longer..." | OGG |

---

## Stage 5: Final Campaign (Levels 16-20)

**Goal:** Endgame levels with maximum difficulty. Avatar encounters, three-keeper maps, all systems pushed to their limits. No new mechanics — pure escalation.

### Levels

| # | Name | Key Feature | Victory |
|---|------|-------------|---------|
| 16 | Skybird Trill I | Avatar preview (appears for 30s then retreats) | Destroy both hearts |
| 17 | Skybird Trill II | Resource management challenge | Destroy both + accumulate 50000 gold |
| 18 | Tulipscent | 3 rival keepers simultaneously | Destroy all 3 hearts |
| 19 | Mirthshire | Full-power Avatar (level 10) | Destroy hearts + defeat Avatar |
| 20 | Skybird Finale | Everything — 3 keepers + Avatar + Lord of the Land | Destroy all enemies |

### New Assets Required

#### 3D Models
| Asset | Description | Format |
|-------|-------------|--------|
| `avatar.glb` | Hero — towering radiant warrior, idle + walk + devastating attacks + holy aura (most impressive hero model) | GLTF |
| `lord_of_the_land.glb` | Hero — crowned super-knight, idle + walk + charge + attack (variant of knight, but grander) | GLTF |

#### Particle Effects
| Asset | Description | Format |
|-------|-------------|--------|
| `vfx_avatar_aura.tres` | Holy golden glow around Avatar | Godot ParticlesMaterial |
| `vfx_avatar_smite.tres` | Massive holy damage explosion | Godot ParticlesMaterial |

#### UI Graphics
| Asset | Description | Format |
|-------|-------------|--------|
| `avatar_portrait.png` | Avatar face/icon for warnings and notifications | PNG 128x128 |
| `campaign_complete_bg.png` | Final victory screen background | PNG 1920x1080 |
| `credits_bg.png` | Credits scroll background | PNG 1920x1080 |

#### Audio — Sound Effects
| Asset | Description | Format |
|-------|-------------|--------|
| `sfx_avatar_appear.ogg` | Radiant holy fanfare + tremor | OGG |
| `sfx_avatar_smite.ogg` | Massive holy explosion | OGG |
| `sfx_avatar_retreat.ogg` | Ethereal fade-away | OGG |
| `sfx_avatar_defeated.ogg` | Holy energy dissipating + collapse | OGG |
| `sfx_final_victory.ogg` | Grand triumphant fanfare (10-15s) | OGG |

#### Audio — Music
| Asset | Description | Format |
|-------|-------------|--------|
| `music_avatar_battle.ogg` | Epic orchestral combat (looping, 3-4 min) | OGG |
| `music_final_victory.ogg` | Grand triumphant theme (60-90s) | OGG |
| `music_credits.ogg` | Reflective closing theme (2-3 min) | OGG |

#### Audio — Mentor Voice Lines
| Asset | Description | Format |
|-------|-------------|--------|
| `mentor_avatar_approaches.ogg` | "The Avatar approaches! Prepare your defenses!" | OGG |
| `mentor_avatar_defeated.ogg` | "The Avatar has fallen! The land is yours!" | OGG |
| `mentor_final_victory.ogg` | "All realms bow before you. You are the ultimate Keeper!" | OGG |
| `mentor_three_keepers.ogg` | "Three rival keepers oppose you in this realm..." | OGG |

---

## Asset Summary by Stage

| Stage | 3D Models | Textures | UI Icons | Sound Effects | Music Tracks | Voice Lines | Code Priority |
|-------|-----------|----------|----------|---------------|--------------|-------------|---------------|
| 0 | 0 | 0 | 0 | 0 | 0 | 0 | Highest — framework |
| 1 | 16 | 14 | ~20 | 18 | 4 | 6 | High — first playable |
| 2 | 13 | 3 | ~16 | 12 | 0 | 3 | Medium |
| 3 | 18 | 3 | ~14 | 14 | 2 | 4 | Medium — AI keeper is major code |
| 4 | 7 | 1 | ~8 | 9 | 0 | 5 | Low — mostly level data |
| 5 | 2 | 0 | ~3 | 5 | 3 | 4 | Low — mostly level data |
| **Total** | **56** | **21** | **~61** | **58** | **9** | **22** | |

### Recommended Execution Order

1. **Stage 0** first (pure code, no assets needed, enables everything else)
2. **Stage 1** code + assets in parallel (you source assets while I build level definitions)
3. **Stage 3 code** early (AI keeper system is the biggest code challenge, needed by level 8)
4. **Stages 2-5** assets can be sourced incrementally as levels are built
5. Audio can lag behind — game is fully playable with procedural graphics and no sound
