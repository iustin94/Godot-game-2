# Asset Tracker

## Target Resolution

**Viewport:** 1920x1080 (Full HD), maximized window, canvas_items stretch mode

---

## Sizing Specification

### Reference Unit: 1 Tile = 1.0 x 1.0 World Units

Everything in the game is measured relative to the tile grid. One tile is the fundamental unit of space.

### Screen Coverage at 1080p

The camera sits at -70° looking down (isometric-like). At different zoom levels, tiles cover different amounts of screen space:

| Zoom Level | Camera Height | Tiles Visible (approx) | Tile Screen Size | Use Case |
|------------|---------------|------------------------|------------------|----------|
| Close (10) | 10 units | ~12 x 7 | ~160 x 155 px | Detail view, possession mode |
| Medium (20) | 20 units | ~22 x 13 | ~87 x 83 px | Normal gameplay |
| Default (40) | 40 units | ~44 x 26 | ~44 x 42 px | Standard overview |
| Far (60) | 60 units | ~65 x 38 | ~30 x 28 px | Strategic overview |
| Max (80) | 80 units | ~85 x 50 | ~23 x 22 px | Full map view |

### 3D Model Sizing (World Units)

All models are authored in **world units where 1 unit = 1 tile width**.

| Category | Size (W x H x D) | Notes |
|----------|-------------------|-------|
| **Tile** | 1.0 x 0.1 x 1.0 | Floor height 0.1. Walls (Earth/Gold/etc) are 1.0 x 0.6 x 1.0 |
| **Small creature** | 0.4 x 0.6 x 0.4 | Imp, Goblin, Firefly, Thief, Fairy |
| **Medium creature** | 0.5 x 0.8 x 0.5 | Warlock, DarkElf, Rogue, Skeleton, Knight, Wizard, Guard, Monk, Samurai, ElvenArcher |
| **Large creature** | 0.6 x 1.0 x 0.6 | Troll, BileDemon, DarkMistress, BlackKnight, Vampire, Salamander, Giant, RoyalGuard |
| **Elite creature** | 0.7 x 1.2 x 0.7 | Dragon, HornedReaper, Avatar, LordOfTheLand |
| **Dungeon Heart** | 3.0 x 1.5 x 3.0 | Spans 3x3 tiles, tallest structure in dungeon |
| **Portal** | 3.0 x 1.2 x 3.0 | Spans 3x3 tiles (operational at 9 tiles) |
| **Hero Gate** | 1.0 x 1.5 x 1.0 | Single tile archway, tall enough to frame heroes |
| **Door** | 1.0 x 0.8 x 0.2 | Fills one tile width, thinner on depth axis |
| **Trap (floor)** | 0.8 x 0.15 x 0.8 | Sits flush with floor, slightly inset from tile edge |
| **Trap (upright)** | 0.5 x 0.6 x 0.5 | Sentry, FearTrap — standing mechanisms |
| **Room prop** | 0.6 x 0.5 x 0.6 | Gravestone, altar, cage, combat pit post, slot machine |
| **Bridge tile** | 1.0 x 0.05 x 1.0 | Flat plank/stone over water or lava |

### 3D Model Quality Targets

| Metric | Target |
|--------|--------|
| Polygon count (small creature) | 800–2,000 tris |
| Polygon count (medium creature) | 1,500–3,500 tris |
| Polygon count (large/elite creature) | 3,000–6,000 tris |
| Polygon count (structure/prop) | 500–2,000 tris |
| Texture per model | 512x512 diffuse + 512x512 normal (optional) |
| Skeleton bones (animated creatures) | 15–30 bones |
| Animation format | Embedded in GLTF or separate .anim |
| Export origin | Model center at ground level (Y=0), facing -Z |

### Texture Sizing

Textures tile across tiles at 1:1 UV mapping. At close zoom, a tile covers ~160px on screen, so textures need enough detail for that.

| Category | Resolution | Notes |
|----------|------------|-------|
| **Tile textures** (earth, gold, etc) | 256x256 | Tiling. 1 texture = 1 tile. At closest zoom ~160px, 256 provides sharp detail |
| **Room floor textures** | 256x256 | Tiling. Same logic as tile textures |
| **Creature diffuse map** | 512x512 | Per-model atlas. Creatures are small on screen but benefit from detail at close zoom |
| **Creature normal map** | 512x512 | Optional, adds surface detail without extra geometry |
| **Structure textures** (heart, portal, gate) | 512x512 or 1024x1024 | Larger models warrant higher res. Heart/portal are focal points |
| **Door/trap textures** | 256x256 | Small models, don't need high res |

### UI Sizing at 1920x1080

UI elements are drawn in screen pixels at the base 1920x1080 viewport and scale with `canvas_items` stretch mode.

| Category | Resolution | Rendered Size | Notes |
|----------|------------|---------------|-------|
| **HUD icons** (gold, mana, creatures) | 48x48 | 48x48 px | Crisp at 1080p, includes 2x padding for retina |
| **Room/spell/trap/door build icons** | 96x96 | 96x96 px | Build panel buttons, need clear silhouettes |
| **Character portraits** | 192x192 | 128x128 px | Rendered at 1.5x for sharp downscale |
| **Level briefing background** | 1920x1080 | Full screen | Match viewport exactly |
| **Level select background** | 1920x1080 | Full screen | Match viewport exactly |
| **Victory/defeat banners** | 768x192 | ~700x180 px | Centered on screen overlay |
| **Timer HUD frame** | 192x96 | ~180x80 px | Countdown timer in survival levels |

### Audio Specs

| Category | Format | Sample Rate | Channels | Notes |
|----------|--------|-------------|----------|-------|
| **SFX** | OGG Vorbis | 44.1 kHz | Mono | Spatial 3D audio, mono required for positional |
| **Music** | OGG Vorbis | 44.1 kHz | Stereo | Looping, normalize to -14 LUFS |
| **Voice lines** | OGG Vorbis | 44.1 kHz | Mono | Normalize to -16 LUFS, 0.1s fade in/out |

---

## Stage 1: Tutorial Arc (Levels 1-4)

### 3D Models

| Status | Asset | Description | Format | Size (WxHxD) |
|--------|-------|-------------|--------|--------------|
| [ ] | `dungeon_heart.glb` | Pulsing heart structure, 3x3 tile footprint | GLTF | 3.0 x 1.5 x 3.0 |
| [ ] | `imp.glb` | Small worker creature, idle + walk + dig animations | GLTF | 0.4 x 0.6 x 0.4 |
| [ ] | `goblin.glb` | Small melee fighter, idle + walk + attack animations | GLTF | 0.4 x 0.6 x 0.4 |
| [ ] | `firefly.glb` | Tiny flying creature with glow, idle + fly animations | GLTF | 0.3 x 0.3 x 0.3 |
| [ ] | `warlock.glb` | Robed spellcaster, idle + walk + cast animations | GLTF | 0.5 x 0.8 x 0.5 |
| [ ] | `troll.glb` | Large brute, idle + walk + attack + workshop-work animations | GLTF | 0.6 x 1.0 x 0.6 |
| [ ] | `dark_elf.glb` | Lean ranged fighter, idle + walk + shoot animations | GLTF | 0.5 x 0.8 x 0.5 |
| [ ] | `dwarf.glb` | Hero — stocky miner/fighter, idle + walk + attack | GLTF | 0.5 x 0.6 x 0.5 |
| [ ] | `thief.glb` | Hero — agile rogue, idle + walk + attack | GLTF | 0.4 x 0.6 x 0.4 |
| [ ] | `guard.glb` | Hero — armored soldier, idle + walk + attack | GLTF | 0.5 x 0.8 x 0.5 |
| [ ] | `fairy.glb` | Hero — flying healer with glow | GLTF | 0.3 x 0.3 x 0.3 |
| [ ] | `hero_gate.glb` | Stone archway portal where heroes spawn | GLTF | 1.0 x 1.5 x 1.0 |
| [ ] | `portal.glb` | Dark swirling portal where keeper creatures arrive | GLTF | 3.0 x 1.2 x 3.0 |
| [ ] | `wooden_door.glb` | Simple plank door, open/close states | GLTF | 1.0 x 0.8 x 0.2 |
| [ ] | `sentry_trap.glb` | Small crossbow turret on floor | GLTF | 0.5 x 0.6 x 0.5 |
| [ ] | `alarm_trap.glb` | Bell/gong on the ground | GLTF | 0.5 x 0.6 x 0.5 |

### Tile Textures (tiling, 256x256)

| Status | Asset | Description | Format | Resolution |
|--------|-------|-------------|--------|------------|
| [ ] | `earth.png` | Brown packed dirt with rock fragments | PNG | 256x256 |
| [ ] | `claimed_path.png` | Dark stone floor with subtle pattern | PNG | 256x256 |
| [ ] | `gold_vein.png` | Rock with gold ore veins | PNG | 256x256 |
| [ ] | `gem_vein.png` | Rock with purple crystal clusters | PNG | 256x256 |
| [ ] | `reinforced_wall.png` | Dark reinforced stone | PNG | 256x256 |
| [ ] | `impenetrable.png` | Black obsidian-like rock | PNG | 256x256 |
| [ ] | `water.png` | Dark underground water (animated UV optional) | PNG | 256x256 |
| [ ] | `lava.png` | Molten rock with glow (animated UV optional) | PNG | 256x256 |

### Room Floor Textures (tiling, 256x256)

| Status | Asset | Description | Format | Resolution |
|--------|-------|-------------|--------|------------|
| [ ] | `lair_floor.png` | Straw/nest bedding | PNG | 256x256 |
| [ ] | `hatchery_floor.png` | Wet stone with chicken feathers | PNG | 256x256 |
| [ ] | `treasury_floor.png` | Gold-inlaid stone | PNG | 256x256 |
| [ ] | `library_floor.png` | Wooden planks with book debris | PNG | 256x256 |
| [ ] | `training_room_floor.png` | Worn stone with weapon marks | PNG | 256x256 |
| [ ] | `workshop_floor.png` | Wooden workbench planks | PNG | 256x256 |

### UI Graphics

| Status | Asset | Description | Format | Resolution |
|--------|-------|-------------|--------|------------|
| [ ] | `icon_gold.png` | Gold coin icon for HUD | PNG | 48x48 |
| [ ] | `icon_mana.png` | Blue mana crystal icon for HUD | PNG | 48x48 |
| [ ] | `icon_creatures.png` | Creature silhouette icon for HUD | PNG | 48x48 |
| [ ] | `room_icons/lair.png` | Lair build icon | PNG | 96x96 |
| [ ] | `room_icons/hatchery.png` | Hatchery build icon | PNG | 96x96 |
| [ ] | `room_icons/treasury.png` | Treasury build icon | PNG | 96x96 |
| [ ] | `room_icons/library.png` | Library build icon | PNG | 96x96 |
| [ ] | `room_icons/training_room.png` | Training room build icon | PNG | 96x96 |
| [ ] | `room_icons/workshop.png` | Workshop build icon | PNG | 96x96 |
| [ ] | `spell_icons/create_imp.png` | Create Imp spell icon | PNG | 96x96 |
| [ ] | `spell_icons/heal.png` | Heal spell icon | PNG | 96x96 |
| [ ] | `spell_icons/sight_of_evil.png` | Sight of Evil spell icon | PNG | 96x96 |
| [ ] | `briefing_bg.png` | Parchment/scroll background for level briefing | PNG | 1920x1080 |
| [ ] | `level_select_bg.png` | Dark underground map for level select | PNG | 1920x1080 |
| [ ] | `victory_banner.png` | "Victory" banner graphic | PNG | 768x192 |
| [ ] | `defeat_banner.png` | "Defeat" banner graphic | PNG | 768x192 |

### Sound Effects

| Status | Asset | Description | Format | Trigger |
|--------|-------|-------------|--------|---------|
| [ ] | `sfx_dig.ogg` | Pick hitting rock | OGG | Imp digging a tile |
| [ ] | `sfx_tile_collapse.ogg` | Rock crumbling/falling | OGG | Tile fully dug out |
| [ ] | `sfx_gold_collect.ogg` | Coins clinking | OGG | Gold mined from tile |
| [ ] | `sfx_claim_tile.ogg` | Stone sealing/claiming sound | OGG | Tile claimed |
| [ ] | `sfx_room_build.ogg` | Construction thud | OGG | Room placed |
| [ ] | `sfx_creature_spawn.ogg` | Magical whoosh | OGG | Creature arrives at portal |
| [ ] | `sfx_imp_acknowledge.ogg` | Small grunt/chirp | OGG | Imp given task |
| [ ] | `sfx_button_click.ogg` | UI click | OGG | Any UI button pressed |
| [ ] | `sfx_button_hover.ogg` | Subtle UI hover | OGG | Mouse over UI button |
| [ ] | `sfx_melee_hit.ogg` | Blunt impact | OGG | Melee attack connects |
| [ ] | `sfx_creature_death.ogg` | Death groan | OGG | Creature dies |
| [ ] | `sfx_spell_cast.ogg` | Magic sparkle | OGG | Any spell cast |
| [ ] | `sfx_victory.ogg` | Triumphant fanfare (3-5s) | OGG | Level victory |
| [ ] | `sfx_defeat.ogg` | Dark ominous stinger (3-5s) | OGG | Level defeat |
| [ ] | `sfx_alarm.ogg` | Bell ringing | OGG | Alarm trap triggered |
| [ ] | `sfx_sentry_fire.ogg` | Crossbow bolt launch | OGG | Sentry trap fires |
| [ ] | `sfx_door_open.ogg` | Wood creaking open | OGG | Door opened |
| [ ] | `sfx_door_close.ogg` | Wood slamming shut | OGG | Door closed |

### Music

| Status | Asset | Description | Format | Usage |
|--------|-------|-------------|--------|-------|
| [ ] | `music_menu.ogg` | Dark ambient, mysterious (looping) | OGG | Main menu / level select |
| [ ] | `music_gameplay_calm.ogg` | Brooding dungeon ambience (looping, 2-3 min) | OGG | Normal gameplay |
| [ ] | `music_gameplay_combat.ogg` | Intense percussion-heavy (looping, 2-3 min) | OGG | During combat / invasions |
| [ ] | `music_briefing.ogg` | Ominous narrative music (30-60s, looping) | OGG | Level briefing screen |

### Mentor Voice Lines

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `mentor_welcome.ogg` | "Welcome, Keeper..." | OGG |
| [ ] | `mentor_claim_territory.ogg` | "Claim territory by digging..." | OGG |
| [ ] | `mentor_creatures_arriving.ogg` | "Creatures are arriving at your portal..." | OGG |
| [ ] | `mentor_under_attack.ogg` | "Your dungeon is under attack!" | OGG |
| [ ] | `mentor_heart_damaged.ogg` | "Your dungeon heart is taking damage!" | OGG |
| [ ] | `mentor_victory.ogg` | "This realm is yours, Keeper." | OGG |

---

## Stage 2: Rising Threat (Levels 5-7)

### 3D Models

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `rogue.glb` | Hooded assassin, idle + walk + backstab animations | GLTF | 0.5 x 0.8 x 0.5 |
| [ ] | `dark_mistress.glb` | Whip-wielding dominatrix, idle + walk + attack | GLTF | 0.6 x 1.0 x 0.6 |
| [ ] | `skeleton.glb` | Undead warrior, idle + walk + attack (rises from ground) | GLTF | 0.5 x 0.8 x 0.5 |
| [ ] | `knight.glb` | Hero — heavy armored, idle + walk + sword attack | GLTF | 0.5 x 0.8 x 0.5 |
| [ ] | `wizard.glb` | Hero — robed with staff, idle + walk + cast | GLTF | 0.5 x 0.8 x 0.5 |
| [ ] | `spike_trap.glb` | Floor spikes that retract/extend | GLTF | 0.8 x 0.15 x 0.8 |
| [ ] | `boulder_trap.glb` | Large rolling boulder | GLTF | 0.7 x 0.7 x 0.7 |
| [ ] | `poison_gas_trap.glb` | Vented floor panel with green mist | GLTF | 0.8 x 0.15 x 0.8 |
| [ ] | `braced_door.glb` | Iron-reinforced wooden door | GLTF | 1.0 x 0.8 x 0.2 |
| [ ] | `steel_door.glb` | Full steel door | GLTF | 1.0 x 0.8 x 0.2 |
| [ ] | `wooden_bridge.glb` | Plank bridge tile (for over water) | GLTF | 1.0 x 0.05 x 1.0 |
| [ ] | `prison_cage.glb` | Iron cage/cell for imprisoned creatures | GLTF | 0.6 x 0.5 x 0.6 |
| [ ] | `torture_device.glb` | Rack or wheel device | GLTF | 0.6 x 0.5 x 0.6 |

### Room Floor Textures

| Status | Asset | Description | Format | Resolution |
|--------|-------|-------------|--------|------------|
| [ ] | `guard_room_floor.png` | Patrolled stone with flags | PNG | 256x256 |
| [ ] | `prison_floor.png` | Cold damp stone with chains | PNG | 256x256 |
| [ ] | `torture_chamber_floor.png` | Dark stone with blood stains | PNG | 256x256 |

### UI Graphics

| Status | Asset | Description | Format | Resolution |
|--------|-------|-------------|--------|------------|
| [ ] | `room_icons/guard_room.png` | Guard room build icon | PNG | 96x96 |
| [ ] | `room_icons/prison.png` | Prison build icon | PNG | 96x96 |
| [ ] | `room_icons/torture_chamber.png` | Torture chamber build icon | PNG | 96x96 |
| [ ] | `room_icons/wooden_bridge.png` | Bridge build icon | PNG | 96x96 |
| [ ] | `spell_icons/lightning.png` | Lightning spell icon | PNG | 96x96 |
| [ ] | `spell_icons/possession.png` | Possession spell icon | PNG | 96x96 |
| [ ] | `spell_icons/speed_monster.png` | Speed Monster spell icon | PNG | 96x96 |
| [ ] | `spell_icons/protect_monster.png` | Protect Monster spell icon | PNG | 96x96 |
| [ ] | `trap_icons/spike_trap.png` | Spike trap icon | PNG | 96x96 |
| [ ] | `trap_icons/boulder.png` | Boulder trap icon | PNG | 96x96 |
| [ ] | `trap_icons/poison_gas.png` | Poison gas trap icon | PNG | 96x96 |
| [ ] | `door_icons/braced.png` | Braced door icon | PNG | 96x96 |
| [ ] | `door_icons/steel.png` | Steel door icon | PNG | 96x96 |

### Sound Effects

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `sfx_lightning.ogg` | Electric crack and sizzle | OGG |
| [ ] | `sfx_possession_enter.ogg` | Ethereal whoosh (entering creature) | OGG |
| [ ] | `sfx_possession_exit.ogg` | Reverse whoosh (leaving creature) | OGG |
| [ ] | `sfx_speed_buff.ogg` | Quick ascending tone | OGG |
| [ ] | `sfx_protection_buff.ogg` | Shield shimmer | OGG |
| [ ] | `sfx_spike_trap.ogg` | Metal spikes shooting up | OGG |
| [ ] | `sfx_boulder_roll.ogg` | Heavy rumbling roll | OGG |
| [ ] | `sfx_poison_gas.ogg` | Hissing gas release | OGG |
| [ ] | `sfx_prisoner_captured.ogg` | Cage slamming | OGG |
| [ ] | `sfx_torture.ogg` | Creaking rack + groan | OGG |
| [ ] | `sfx_creature_converted.ogg` | Dark magical transformation | OGG |
| [ ] | `sfx_steel_door.ogg` | Heavy metal door slam | OGG |

### Mentor Voice Lines

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `mentor_prison_full.ogg` | "Your prison is full..." | OGG |
| [ ] | `mentor_creature_converted.ogg` | "A hero has been converted to your cause!" | OGG |
| [ ] | `mentor_research_complete.ogg` | "Your warlocks have completed their research." | OGG |

---

## Stage 3: Rival Keepers (Levels 8-11)

### 3D Models

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `salamander.glb` | Fire lizard, idle + walk + fire-breath attack | GLTF | 0.6 x 1.0 x 0.6 |
| [ ] | `vampire.glb` | Caped undead lord, idle + walk + bite attack + bat-transform | GLTF | 0.6 x 1.0 x 0.6 |
| [ ] | `bile_demon.glb` | Bloated demon, idle + waddle + gas attack | GLTF | 0.6 x 1.0 x 0.6 |
| [ ] | `black_knight.glb` | Dark armored knight, idle + walk + heavy swing | GLTF | 0.6 x 1.0 x 0.6 |
| [ ] | `monk.glb` | Hero — martial artist, idle + walk + kick | GLTF | 0.5 x 0.8 x 0.5 |
| [ ] | `elven_archer.glb` | Hero — elf with bow, idle + walk + shoot | GLTF | 0.5 x 0.8 x 0.5 |
| [ ] | `giant.glb` | Hero — oversized brute, idle + walk + stomp | GLTF | 0.6 x 1.0 x 0.6 |
| [ ] | `samurai.glb` | Hero — katana wielder, idle + walk + slash | GLTF | 0.5 x 0.8 x 0.5 |
| [ ] | `lightning_trap.glb` | Tesla coil trap | GLTF | 0.5 x 0.6 x 0.5 |
| [ ] | `freeze_trap.glb` | Ice crystal trap | GLTF | 0.8 x 0.15 x 0.8 |
| [ ] | `fear_trap.glb` | Skull totem that emits fear | GLTF | 0.5 x 0.6 x 0.5 |
| [ ] | `fireburst_trap.glb` | Fire vent trap | GLTF | 0.8 x 0.15 x 0.8 |
| [ ] | `magic_door.glb` | Glowing enchanted door | GLTF | 1.0 x 0.8 x 0.2 |
| [ ] | `stone_bridge.glb` | Stone bridge tile (for over lava) | GLTF | 1.0 x 0.05 x 1.0 |
| [ ] | `gravestone.glb` | Tombstone prop for graveyard room | GLTF | 0.6 x 0.5 x 0.6 |
| [ ] | `combat_pit_post.glb` | Arena post/pillar for combat pit | GLTF | 0.6 x 0.5 x 0.6 |
| [ ] | `slot_machine.glb` | Casino gambling device | GLTF | 0.6 x 0.5 x 0.6 |
| [ ] | `enemy_dungeon_heart.glb` | Rival keeper's heart (different color/style) | GLTF | 3.0 x 1.5 x 3.0 |

### Room Floor Textures

| Status | Asset | Description | Format | Resolution |
|--------|-------|-------------|--------|------------|
| [ ] | `combat_pit_floor.png` | Sandy arena surface | PNG | 256x256 |
| [ ] | `graveyard_floor.png` | Dark earth with bones | PNG | 256x256 |
| [ ] | `casino_floor.png` | Decorated stone with patterns | PNG | 256x256 |

### Particle Effects

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `vfx_cave_in.tres` | Falling rocks particle system | Godot ParticlesMaterial |
| [ ] | `vfx_inferno.tres` | Large fire explosion particles | Godot ParticlesMaterial |
| [ ] | `vfx_freeze.tres` | Ice crystal expansion particles | Godot ParticlesMaterial |
| [ ] | `vfx_turncoat.tres` | Dark-to-light color shift particles | Godot ParticlesMaterial |
| [ ] | `vfx_call_to_arms.tres` | Rally banner/flag particles | Godot ParticlesMaterial |

### UI Graphics

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `room_icons/combat_pit.png` | Combat pit build icon | PNG | 96x96 |
| [ ] | `room_icons/graveyard.png` | Graveyard build icon | PNG | 96x96 |
| [ ] | `room_icons/casino.png` | Casino build icon | PNG | 96x96 |
| [ ] | `room_icons/stone_bridge.png` | Stone bridge build icon | PNG | 96x96 |
| [ ] | `spell_icons/call_to_arms.png` | Call to Arms spell icon | PNG | 96x96 |
| [ ] | `spell_icons/cave_in.png` | Cave-In spell icon | PNG | 96x96 |
| [ ] | `spell_icons/turncoat.png` | Turncoat spell icon | PNG | 96x96 |
| [ ] | `spell_icons/inferno.png` | Inferno spell icon | PNG | 96x96 |
| [ ] | `trap_icons/lightning_trap.png` | Lightning trap icon | PNG | 96x96 |
| [ ] | `trap_icons/freeze.png` | Freeze trap icon | PNG | 96x96 |
| [ ] | `trap_icons/fear.png` | Fear trap icon | PNG | 96x96 |
| [ ] | `trap_icons/fireburst.png` | Fireburst trap icon | PNG | 96x96 |
| [ ] | `door_icons/magic.png` | Magic door icon | PNG | 96x96 |
| [ ] | `enemy_keeper_portrait.png` | Rival keeper face for notifications | PNG | 192x192 |

### Sound Effects

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `sfx_cave_in.ogg` | Massive rock collapse | OGG |
| [ ] | `sfx_inferno.ogg` | Roaring fire explosion | OGG |
| [ ] | `sfx_freeze_trap.ogg` | Ice crystallization crack | OGG |
| [ ] | `sfx_fear_trap.ogg` | Ghostly wail | OGG |
| [ ] | `sfx_fireburst.ogg` | Fire jet burst | OGG |
| [ ] | `sfx_lightning_trap.ogg` | Electric arc discharge | OGG |
| [ ] | `sfx_turncoat.ogg` | Dark magical charm sound | OGG |
| [ ] | `sfx_call_to_arms.ogg` | War horn blast | OGG |
| [ ] | `sfx_vampire_bite.ogg` | Quick bite with hiss | OGG |
| [ ] | `sfx_bile_demon_gas.ogg` | Disgusting gas release | OGG |
| [ ] | `sfx_casino_win.ogg` | Slot machine jackpot | OGG |
| [ ] | `sfx_casino_lose.ogg` | Disappointing clunk | OGG |
| [ ] | `sfx_enemy_heart_hit.ogg` | Enemy heart taking damage | OGG |
| [ ] | `sfx_enemy_heart_destroyed.ogg` | Massive explosion/collapse | OGG |

### Music

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `music_gameplay_tense.ogg` | Building tension, rival keeper nearby (looping, 2-3 min) | OGG |
| [ ] | `music_enemy_defeated.ogg` | Short triumphant sting when enemy keeper falls (5-10s) | OGG |

### Mentor Voice Lines

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `mentor_rival_keeper.ogg` | "An enemy keeper dwells in this realm..." | OGG |
| [ ] | `mentor_enemy_breached.ogg` | "The enemy has breached your defenses!" | OGG |
| [ ] | `mentor_enemy_heart_found.ogg` | "You have found the enemy's dungeon heart!" | OGG |
| [ ] | `mentor_enemy_defeated.ogg` | "The rival keeper has been vanquished!" | OGG |

---

## Stage 4: Endgame Preparation (Levels 12-15)

### 3D Models

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `dragon.glb` | Large winged dragon, idle + walk + fly + fire-breath | GLTF | 0.7 x 1.2 x 0.7 |
| [ ] | `horned_reaper.glb` | Massive demon with scythe, idle + walk + devastating attacks | GLTF | 0.7 x 1.2 x 0.7 |
| [ ] | `royal_guard.glb` | Hero — elite knight with cape, idle + walk + attack | GLTF | 0.6 x 1.0 x 0.6 |
| [ ] | `jack_in_the_box_trap.glb` | Pop-up scare trap | GLTF | 0.5 x 0.6 x 0.5 |
| [ ] | `trigger_trap.glb` | Floor pressure plate | GLTF | 0.8 x 0.05 x 0.8 |
| [ ] | `secret_door.glb` | Wall segment that opens (looks like reinforced wall) | GLTF | 1.0 x 0.8 x 0.2 |
| [ ] | `temple_altar.glb` | Dark altar/shrine prop for temple room | GLTF | 0.6 x 0.5 x 0.6 |

### Room Floor Textures

| Status | Asset | Description | Format | Resolution |
|--------|-------|-------------|--------|------------|
| [ ] | `temple_floor.png` | Dark ritual stone with runes | PNG | 256x256 |

### Particle Effects

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `vfx_tremor.tres` | Ground shaking with dust and rock debris | Godot ParticlesMaterial |
| [ ] | `vfx_create_gold.tres` | Gold sparkle transmutation effect | Godot ParticlesMaterial |
| [ ] | `vfx_summon_reaper.tres` | Dark hellfire portal summoning effect | Godot ParticlesMaterial |
| [ ] | `vfx_dragon_breath.tres` | Fire stream particles | Godot ParticlesMaterial |

### UI Graphics

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `room_icons/temple.png` | Temple build icon | PNG | 96x96 |
| [ ] | `spell_icons/tremor.png` | Tremor spell icon | PNG | 96x96 |
| [ ] | `spell_icons/create_gold.png` | Create Gold spell icon | PNG | 96x96 |
| [ ] | `spell_icons/summon_horned_reaper.png` | Summon Horned Reaper spell icon | PNG | 96x96 |
| [ ] | `trap_icons/jack_in_the_box.png` | JackInTheBox trap icon | PNG | 96x96 |
| [ ] | `trap_icons/trigger.png` | Trigger trap icon | PNG | 96x96 |
| [ ] | `door_icons/secret.png` | Secret door icon | PNG | 96x96 |
| [ ] | `timer_hud.png` | Countdown timer frame for survival objective | PNG | 192x96 |

### Sound Effects

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `sfx_tremor.ogg` | Deep earthquake rumble | OGG |
| [ ] | `sfx_create_gold.ogg` | Magical transmutation shimmer | OGG |
| [ ] | `sfx_summon_reaper.ogg` | Hellish roar + fire crackle | OGG |
| [ ] | `sfx_dragon_roar.ogg` | Dragon roar | OGG |
| [ ] | `sfx_dragon_breath.ogg` | Sustained fire breath | OGG |
| [ ] | `sfx_reaper_scythe.ogg` | Heavy scythe swing | OGG |
| [ ] | `sfx_reaper_angry.ogg` | Reaper becoming enraged (if neglected) | OGG |
| [ ] | `sfx_jack_in_the_box.ogg` | Spring pop + scream | OGG |
| [ ] | `sfx_temple_prayer.ogg` | Dark chanting loop | OGG |

### Mentor Voice Lines

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `mentor_dragon_arrived.ogg` | "A dragon has come to your dungeon!" | OGG |
| [ ] | `mentor_reaper_summoned.ogg` | "The Horned Reaper walks among us..." | OGG |
| [ ] | `mentor_reaper_angry.ogg` | "The Horned Reaper is angry! Beware!" | OGG |
| [ ] | `mentor_gold_low.ogg` | "Your treasury is running low, Keeper." | OGG |
| [ ] | `mentor_survive_timer.ogg` | "You must hold out a little longer..." | OGG |

---

## Stage 5: Final Campaign (Levels 16-20)

### 3D Models

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `avatar.glb` | Hero — towering radiant warrior, holy aura, devastating attacks | GLTF | 0.7 x 1.2 x 0.7 |
| [ ] | `lord_of_the_land.glb` | Hero — crowned super-knight, charge + attack | GLTF | 0.7 x 1.2 x 0.7 |

### Particle Effects

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `vfx_avatar_aura.tres` | Holy golden glow around Avatar | Godot ParticlesMaterial |
| [ ] | `vfx_avatar_smite.tres` | Massive holy damage explosion | Godot ParticlesMaterial |

### UI Graphics

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `avatar_portrait.png` | Avatar face/icon for warnings and notifications | PNG | 192x192 |
| [ ] | `campaign_complete_bg.png` | Final victory screen background | PNG | 1920x1080 |
| [ ] | `credits_bg.png` | Credits scroll background | PNG | 1920x1080 |

### Sound Effects

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `sfx_avatar_appear.ogg` | Radiant holy fanfare + tremor | OGG |
| [ ] | `sfx_avatar_smite.ogg` | Massive holy explosion | OGG |
| [ ] | `sfx_avatar_retreat.ogg` | Ethereal fade-away | OGG |
| [ ] | `sfx_avatar_defeated.ogg` | Holy energy dissipating + collapse | OGG |
| [ ] | `sfx_final_victory.ogg` | Grand triumphant fanfare (10-15s) | OGG |

### Music

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `music_avatar_battle.ogg` | Epic orchestral combat (looping, 3-4 min) | OGG |
| [ ] | `music_final_victory.ogg` | Grand triumphant theme (60-90s) | OGG |
| [ ] | `music_credits.ogg` | Reflective closing theme (2-3 min) | OGG |

### Mentor Voice Lines

| Status | Asset | Description | Format |
|--------|-------|-------------|--------|
| [ ] | `mentor_avatar_approaches.ogg` | "The Avatar approaches! Prepare your defenses!" | OGG |
| [ ] | `mentor_avatar_defeated.ogg` | "The Avatar has fallen! The land is yours!" | OGG |
| [ ] | `mentor_final_victory.ogg` | "All realms bow before you. You are the ultimate Keeper!" | OGG |
| [ ] | `mentor_three_keepers.ogg` | "Three rival keepers oppose you in this realm..." | OGG |

---

## Summary

| Stage | 3D Models | Textures | UI Icons | SFX | Music | Voice | Total |
|-------|-----------|----------|----------|-----|-------|-------|-------|
| 1 | 0/16 | 0/14 | 0/16 | 0/18 | 0/4 | 0/6 | 0/74 |
| 2 | 0/13 | 0/3 | 0/13 | 0/12 | 0/0 | 0/3 | 0/44 |
| 3 | 0/18 | 0/3 | 0/14 | 0/14 | 0/2 | 0/4 | 0/55 |
| 4 | 0/7 | 0/1 | 0/8 | 0/9 | 0/0 | 0/5 | 0/30 |
| 5 | 0/2 | 0/0 | 0/3 | 0/5 | 0/3 | 0/4 | 0/17 |
| **Total** | **0/56** | **0/21** | **0/54** | **0/58** | **0/9** | **0/22** | **0/220** |
