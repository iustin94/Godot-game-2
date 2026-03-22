using DungeonKeeper.Creatures.Definitions;
using DungeonKeeper.Dungeon.Rooms;

namespace DungeonKeeper.Creatures.Data;

/// <summary>
/// Registers all Dungeon Keeper 2 creature definitions with approximate stats.
/// </summary>
public static class DK2CreatureData
{
    public static void RegisterAll(CreatureDefinitionRegistry registry)
    {
        RegisterKeeperCreatures(registry);
        RegisterHeroCreatures(registry);
    }

    private static void RegisterKeeperCreatures(CreatureDefinitionRegistry registry)
    {
        // Imp - cheap worker, mines gold, claims tiles
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Imp)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Imp")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 200, MeleeAttack: 30, MeleeDamage: 5,
                Defense: 20, Armor: 0, Luck: 50, Speed: 48f,
                ResearchSkill: 0, ManufactureSkill: 0, TrainingCost: 0))
            .WithLevelProgression(new LevelProgression(20, 3, 1, 2, 0, 2f))
            .WithWages(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
            .WithManaDrain(0.2f)
            .CannotBeAttractedViaPortal()
            .WithDropStunDuration(1f)
            .WithTrainingRoomMaxLevel(4)
            .WithCombatPitMaxLevel(4)
            .Build());

        // Goblin - basic fighter
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Goblin)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Goblin")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 600, MeleeAttack: 80, MeleeDamage: 15,
                Defense: 60, Armor: 5, Luck: 60, Speed: 40f,
                ResearchSkill: 0, ManufactureSkill: 1, TrainingCost: 25))
            .WithLevelProgression(new LevelProgression(60, 8, 3, 5, 1, 1.5f))
            .WithAttractionRequirement(RoomType.Lair, 1)
            .WithAttractionRequirement(RoomType.TrainingRoom, 9)
            .WithJobPreference(RoomType.TrainingRoom)
            .WithJobPreference(RoomType.GuardRoom)
            .WithWages(25, 50, 75, 100, 150, 200, 275, 350, 450, 600)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(4, "haste")
            .WithAbilitiesAtLevel(8, "whirlwind")
            .Build());

        // Warlock - researcher
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Warlock)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Warlock")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 800, MeleeAttack: 40, MeleeDamage: 8,
                Defense: 40, Armor: 2, Luck: 70, Speed: 32f,
                ResearchSkill: 4, ManufactureSkill: 0, TrainingCost: 50))
            .WithLevelProgression(new LevelProgression(80, 5, 2, 4, 1, 1f))
            .WithAttractionRequirement(RoomType.Lair, 1)
            .WithAttractionRequirement(RoomType.Library, 25)
            .WithJobPreference(RoomType.Library)
            .WithWages(35, 70, 110, 150, 200, 260, 330, 420, 530, 700)
            .WithAbilitiesAtLevel(1, "fireball")
            .WithAbilitiesAtLevel(3, "heal")
            .WithAbilitiesAtLevel(6, "lightning")
            .WithAbilitiesAtLevel(9, "meteor")
            .WithAntipathy(CreatureType.Vampire)
            .Build());

        // Troll - manufacturer
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Troll)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Troll")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 1000, MeleeAttack: 70, MeleeDamage: 20,
                Defense: 50, Armor: 8, Luck: 40, Speed: 28f,
                ResearchSkill: 0, ManufactureSkill: 4, TrainingCost: 40))
            .WithLevelProgression(new LevelProgression(100, 7, 4, 5, 2, 1f))
            .WithAttractionRequirement(RoomType.Lair, 1)
            .WithAttractionRequirement(RoomType.Workshop, 9)
            .WithJobPreference(RoomType.Workshop)
            .WithWages(30, 60, 95, 130, 175, 230, 300, 380, 480, 650)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(5, "boulder_throw")
            .Build());

        // Dark Elf - ranged attacker
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.DarkElf)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Dark Elf")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 700, MeleeAttack: 90, MeleeDamage: 18,
                Defense: 70, Armor: 4, Luck: 80, Speed: 44f,
                ResearchSkill: 1, ManufactureSkill: 1, TrainingCost: 45))
            .WithLevelProgression(new LevelProgression(70, 9, 4, 6, 1, 1.5f))
            .WithAttractionRequirement(RoomType.Lair, 1)
            .WithAttractionRequirement(RoomType.CombatPit, 25)
            .WithJobPreference(RoomType.CombatPit)
            .WithJobPreference(RoomType.GuardRoom)
            .WithWages(40, 80, 120, 170, 225, 290, 370, 460, 580, 750)
            .WithAbilitiesAtLevel(1, "arrow_shot")
            .WithAbilitiesAtLevel(3, "guided_bolt")
            .WithAbilitiesAtLevel(7, "call_to_arms")
            .Build());

        // Salamander - fire creature
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Salamander)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Salamander")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 1200, MeleeAttack: 85, MeleeDamage: 22,
                Defense: 65, Armor: 10, Luck: 60, Speed: 36f,
                ResearchSkill: 0, ManufactureSkill: 2, TrainingCost: 60))
            .WithLevelProgression(new LevelProgression(120, 9, 5, 6, 2, 1f))
            .WithAttractionRequirement(RoomType.Lair, 1)
            .WithAttractionRequirement(RoomType.TrainingRoom, 25)
            .WithJobPreference(RoomType.TrainingRoom)
            .WithJobPreference(RoomType.GuardRoom)
            .WithWages(50, 100, 155, 215, 285, 365, 460, 570, 710, 900)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(1, "fire_breath")
            .WithAbilitiesAtLevel(5, "flame_wave")
            .Build());

        // Bile Demon - tough, slow manufacturer
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.BileDemon)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Bile Demon")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 1800, MeleeAttack: 75, MeleeDamage: 25,
                Defense: 55, Armor: 15, Luck: 30, Speed: 24f,
                ResearchSkill: 0, ManufactureSkill: 3, TrainingCost: 75))
            .WithLevelProgression(new LevelProgression(180, 8, 5, 5, 3, 0.5f))
            .WithAttractionRequirement(RoomType.Hatchery, 25)
            .WithAttractionRequirement(RoomType.Workshop, 9)
            .WithJobPreference(RoomType.Workshop)
            .WithJobPreference(RoomType.Hatchery)
            .IsImmuneToPoison()
            .WithWages(60, 120, 185, 260, 345, 445, 560, 700, 870, 1100)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(3, "poison_gas")
            .WithAbilitiesAtLevel(7, "bile_vomit")
            .Build());

        // Vampire - undead, spawns from graveyard
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Vampire)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Vampire")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 1200, MeleeAttack: 90, MeleeDamage: 20,
                Defense: 75, Armor: 8, Luck: 90, Speed: 40f,
                ResearchSkill: 3, ManufactureSkill: 0, TrainingCost: 65))
            .WithLevelProgression(new LevelProgression(120, 10, 4, 7, 2, 1.5f))
            .CannotBeAttractedViaPortal()
            .IsUndead()
            .WithJobPreference(RoomType.Library)
            .WithJobPreference(RoomType.Graveyard)
            .WithWages(55, 110, 170, 240, 320, 415, 525, 650, 810, 1050)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(1, "drain_life")
            .WithAbilitiesAtLevel(4, "bat_form")
            .WithAbilitiesAtLevel(8, "raise_dead")
            .WithAntipathy(CreatureType.Warlock)
            .Build());

        // Dark Mistress - enjoys torture
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.DarkMistress)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Dark Mistress")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 900, MeleeAttack: 85, MeleeDamage: 16,
                Defense: 65, Armor: 5, Luck: 75, Speed: 40f,
                ResearchSkill: 0, ManufactureSkill: 0, TrainingCost: 50))
            .WithLevelProgression(new LevelProgression(90, 9, 3, 6, 1, 1.5f))
            .WithAttractionRequirement(RoomType.Lair, 1)
            .WithAttractionRequirement(RoomType.TortureChamber, 9)
            .WithJobPreference(RoomType.TortureChamber)
            .WithJobPreference(RoomType.CombatPit)
            .WithWages(40, 80, 125, 175, 235, 305, 390, 490, 615, 800)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(3, "lightning_whip")
            .WithAbilitiesAtLevel(6, "speed_boost")
            .Build());

        // Rogue - thief, sets traps
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Rogue)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Rogue")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 650, MeleeAttack: 80, MeleeDamage: 14,
                Defense: 70, Armor: 3, Luck: 90, Speed: 48f,
                ResearchSkill: 0, ManufactureSkill: 2, TrainingCost: 35))
            .WithLevelProgression(new LevelProgression(65, 8, 3, 6, 1, 2f))
            .WithAttractionRequirement(RoomType.Lair, 1)
            .WithAttractionRequirement(RoomType.Casino, 9)
            .WithJobPreference(RoomType.Casino)
            .WithJobPreference(RoomType.Workshop)
            .WithWages(30, 60, 95, 130, 175, 230, 300, 380, 480, 650)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(2, "stealth")
            .WithAbilitiesAtLevel(5, "backstab")
            .WithAbilitiesAtLevel(8, "set_trap")
            .Build());

        // Black Knight - elite melee
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.BlackKnight)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Black Knight")
            .IsElite()
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 1500, MeleeAttack: 100, MeleeDamage: 28,
                Defense: 85, Armor: 20, Luck: 60, Speed: 32f,
                ResearchSkill: 0, ManufactureSkill: 0, TrainingCost: 80))
            .WithLevelProgression(new LevelProgression(150, 10, 6, 8, 4, 1f))
            .WithAttractionRequirement(RoomType.Lair, 1)
            .WithAttractionRequirement(RoomType.GuardRoom, 25)
            .WithJobPreference(RoomType.GuardRoom)
            .WithJobPreference(RoomType.CombatPit)
            .WithWages(75, 150, 230, 320, 425, 545, 690, 860, 1070, 1350)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(3, "shield_bash")
            .WithAbilitiesAtLevel(6, "whirlwind_slash")
            .WithAbilitiesAtLevel(9, "war_cry")
            .Build());

        // Dragon - powerful, slow, can fly
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Dragon)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Dragon")
            .IsElite()
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 2000, MeleeAttack: 95, MeleeDamage: 35,
                Defense: 80, Armor: 25, Luck: 50, Speed: 28f,
                ResearchSkill: 2, ManufactureSkill: 0, TrainingCost: 100))
            .WithLevelProgression(new LevelProgression(200, 10, 7, 8, 5, 0.5f))
            .HasFlight()
            .WithAttractionRequirement(RoomType.Lair, 1)
            .WithAttractionRequirement(RoomType.Treasury, 25)
            .WithJobPreference(RoomType.Treasury)
            .WithJobPreference(RoomType.Library)
            .WithWages(100, 200, 310, 435, 575, 740, 930, 1160, 1440, 1800)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(1, "fire_breath")
            .WithAbilitiesAtLevel(5, "flame_wave")
            .WithAbilitiesAtLevel(8, "inferno")
            .Build());

        // Firefly - scout, weak, can fly
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Firefly)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Firefly")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 300, MeleeAttack: 20, MeleeDamage: 3,
                Defense: 30, Armor: 0, Luck: 80, Speed: 56f,
                ResearchSkill: 0, ManufactureSkill: 0, TrainingCost: 15))
            .WithLevelProgression(new LevelProgression(30, 2, 1, 3, 0, 2f))
            .HasFlight()
            .WithAttractionRequirement(RoomType.Lair, 1)
            .WithJobPreference(RoomType.GuardRoom)
            .WithWages(10, 20, 30, 45, 60, 80, 105, 135, 170, 225)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(3, "light_flash")
            .Build());

        // Skeleton - undead, spawns from prison
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Skeleton)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Skeleton")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 500, MeleeAttack: 65, MeleeDamage: 12,
                Defense: 50, Armor: 3, Luck: 30, Speed: 32f,
                ResearchSkill: 0, ManufactureSkill: 1, TrainingCost: 20))
            .WithLevelProgression(new LevelProgression(50, 6, 2, 4, 1, 1f))
            .IsUndead()
            .CannotBeAttractedViaPortal()
            .WithJobPreference(RoomType.GuardRoom)
            .WithJobPreference(RoomType.TrainingRoom)
            .WithWages(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(5, "bone_throw")
            .Build());

        // Horned Reaper - most powerful creature, summoned by spell
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Horny)
            .WithFaction(CreatureFaction.Keeper)
            .WithName("Horned Reaper")
            .IsElite()
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 3000, MeleeAttack: 120, MeleeDamage: 50,
                Defense: 90, Armor: 30, Luck: 100, Speed: 40f,
                ResearchSkill: 0, ManufactureSkill: 0, TrainingCost: 150))
            .WithLevelProgression(new LevelProgression(300, 12, 10, 9, 6, 1f))
            .CannotBeAttractedViaPortal()
            .WithWages(200, 400, 620, 870, 1150, 1480, 1860, 2320, 2880, 3600)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(1, "scythe_slash")
            .WithAbilitiesAtLevel(3, "fire_wave")
            .WithAbilitiesAtLevel(6, "berserk")
            .WithAbilitiesAtLevel(9, "armageddon")
            .WithDropStunDuration(0.5f)
            .Build());
    }

    private static void RegisterHeroCreatures(CreatureDefinitionRegistry registry)
    {
        // Knight - standard hero fighter
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Knight)
            .WithFaction(CreatureFaction.Hero)
            .WithName("Knight")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 1500, MeleeAttack: 95, MeleeDamage: 25,
                Defense: 85, Armor: 20, Luck: 60, Speed: 32f,
                ResearchSkill: 0, ManufactureSkill: 0, TrainingCost: 0))
            .WithLevelProgression(new LevelProgression(150, 10, 5, 8, 4, 1f))
            .WithWages(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(3, "shield_bash")
            .WithAbilitiesAtLevel(6, "holy_strike")
            .Build());

        // Wizard - spell-based hero
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Wizard)
            .WithFaction(CreatureFaction.Hero)
            .WithName("Wizard")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 800, MeleeAttack: 35, MeleeDamage: 7,
                Defense: 40, Armor: 2, Luck: 80, Speed: 30f,
                ResearchSkill: 4, ManufactureSkill: 0, TrainingCost: 0))
            .WithLevelProgression(new LevelProgression(80, 4, 2, 4, 1, 0.5f))
            .WithWages(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
            .WithAbilitiesAtLevel(1, "fireball")
            .WithAbilitiesAtLevel(3, "heal")
            .WithAbilitiesAtLevel(5, "lightning_bolt")
            .WithAbilitiesAtLevel(8, "blizzard")
            .Build());

        // Giant - slow but powerful hero
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Giant)
            .WithFaction(CreatureFaction.Hero)
            .WithName("Giant")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 2500, MeleeAttack: 85, MeleeDamage: 40,
                Defense: 60, Armor: 18, Luck: 30, Speed: 20f,
                ResearchSkill: 0, ManufactureSkill: 0, TrainingCost: 0))
            .WithLevelProgression(new LevelProgression(250, 9, 8, 6, 4, 0.5f))
            .WithWages(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(3, "ground_slam")
            .WithAbilitiesAtLevel(7, "boulder_hurl")
            .Build());

        // Guard - basic hero soldier
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Guard)
            .WithFaction(CreatureFaction.Hero)
            .WithName("Guard")
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 800, MeleeAttack: 70, MeleeDamage: 15,
                Defense: 65, Armor: 10, Luck: 50, Speed: 32f,
                ResearchSkill: 0, ManufactureSkill: 0, TrainingCost: 0))
            .WithLevelProgression(new LevelProgression(80, 7, 3, 5, 2, 1f))
            .WithWages(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(4, "shield_wall")
            .Build());

        // Avatar - ultimate hero
        registry.Register(new CreatureDefinitionBuilder()
            .WithType(CreatureType.Avatar)
            .WithFaction(CreatureFaction.Hero)
            .WithName("Avatar")
            .IsElite()
            .WithBaseStats(new CreatureBaseStats(
                MaxHealth: 5000, MeleeAttack: 120, MeleeDamage: 45,
                Defense: 95, Armor: 30, Luck: 100, Speed: 36f,
                ResearchSkill: 4, ManufactureSkill: 0, TrainingCost: 0))
            .WithLevelProgression(new LevelProgression(500, 12, 9, 10, 6, 1f))
            .WithWages(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
            .WithAbilitiesAtLevel(1, "melee_attack")
            .WithAbilitiesAtLevel(1, "heal")
            .WithAbilitiesAtLevel(3, "holy_strike")
            .WithAbilitiesAtLevel(5, "divine_light")
            .WithAbilitiesAtLevel(7, "resurrect")
            .WithAbilitiesAtLevel(10, "wrath_of_gods")
            .Build());
    }
}
