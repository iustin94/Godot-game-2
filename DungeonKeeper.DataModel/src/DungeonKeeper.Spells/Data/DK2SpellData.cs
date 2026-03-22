using DungeonKeeper.Spells.Effects;

namespace DungeonKeeper.Spells.Data;

public static class DK2SpellData
{
    public static void RegisterAll(SpellDefinitionRegistry registry)
    {
        registry.Register(new SpellDefinition
        {
            Id = "create_imp",
            Name = "Create Imp",
            AssetId = "spell_create_imp",
            Type = SpellType.CreateImp,
            ManaCost = 150,
            Cooldown = 0f,
            TargetType = SpellTargetType.None,
            AvailableByDefault = true,
            Effect = new CreateImpEffect()
        });

        registry.Register(new SpellDefinition
        {
            Id = "possession",
            Name = "Possession",
            AssetId = "spell_possession",
            Type = SpellType.Possession,
            ManaCost = 500,
            Cooldown = 0f,
            TargetType = SpellTargetType.Creature,
            ResearchPointsRequired = 800,
            Effect = new PossessionEffect()
        });

        registry.Register(new SpellDefinition
        {
            Id = "heal",
            Name = "Heal",
            AssetId = "spell_heal",
            Type = SpellType.Heal,
            ManaCost = 300,
            Cooldown = 0f,
            TargetType = SpellTargetType.Creature,
            ResearchPointsRequired = 600,
            Effect = new HealEffect(healAmount: 500)
        });

        registry.Register(new SpellDefinition
        {
            Id = "lightning",
            Name = "Lightning",
            AssetId = "spell_lightning",
            Type = SpellType.Lightning,
            ManaCost = 500,
            Cooldown = 0f,
            TargetType = SpellTargetType.AreaOfEffect,
            Range = 15f,
            AreaRadius = 1f,
            ResearchPointsRequired = 400,
            Effect = new DamageAreaEffect(damage: 300, radius: 1f)
        });

        registry.Register(new SpellDefinition
        {
            Id = "sight_of_evil",
            Name = "Sight of Evil",
            AssetId = "spell_sight_of_evil",
            Type = SpellType.SightOfEvil,
            ManaCost = 200,
            Cooldown = 0f,
            TargetType = SpellTargetType.Tile,
            ResearchPointsRequired = 200,
            AreaRadius = 10f,
            Effect = new SightOfEvilEffect()
        });

        registry.Register(new SpellDefinition
        {
            Id = "speed_monster",
            Name = "Speed Monster",
            AssetId = "spell_speed_monster",
            Type = SpellType.SpeedMonster,
            ManaCost = 250,
            Cooldown = 0f,
            TargetType = SpellTargetType.Creature,
            ResearchPointsRequired = 300,
            Effect = new BuffCreatureEffect(buffType: "Speed", duration: 60f, magnitude: 2f)
        });

        registry.Register(new SpellDefinition
        {
            Id = "protect_monster",
            Name = "Protect Monster",
            AssetId = "spell_protect_monster",
            Type = SpellType.ProtectMonster,
            ManaCost = 350,
            Cooldown = 0f,
            TargetType = SpellTargetType.Creature,
            ResearchPointsRequired = 500,
            Effect = new BuffCreatureEffect(buffType: "Protection", duration: 60f, magnitude: 0.5f)
        });

        registry.Register(new SpellDefinition
        {
            Id = "call_to_arms",
            Name = "Call to Arms",
            AssetId = "spell_call_to_arms",
            Type = SpellType.CallToArms,
            ManaCost = 300,
            Cooldown = 0f,
            TargetType = SpellTargetType.Global,
            ResearchPointsRequired = 700,
            Effect = new CallToArmsEffect()
        });

        registry.Register(new SpellDefinition
        {
            Id = "cave_in",
            Name = "Cave-In",
            AssetId = "spell_cave_in",
            Type = SpellType.CaveIn,
            ManaCost = 2000,
            Cooldown = 0f,
            TargetType = SpellTargetType.AreaOfEffect,
            AreaRadius = 3f,
            ResearchPointsRequired = 1200,
            Effect = new CaveInEffect()
        });

        registry.Register(new SpellDefinition
        {
            Id = "turncoat",
            Name = "Turncoat",
            AssetId = "spell_turncoat",
            Type = SpellType.Turncoat,
            ManaCost = 5000,
            Cooldown = 0f,
            TargetType = SpellTargetType.Creature,
            ResearchPointsRequired = 2000,
            Effect = new TurncoatEffect()
        });

        registry.Register(new SpellDefinition
        {
            Id = "inferno",
            Name = "Inferno",
            AssetId = "spell_inferno",
            Type = SpellType.Inferno,
            ManaCost = 1500,
            Cooldown = 0f,
            TargetType = SpellTargetType.AreaOfEffect,
            AreaRadius = 5f,
            ResearchPointsRequired = 1500,
            Effect = new DamageAreaEffect(damage: 500, radius: 5f)
        });

        registry.Register(new SpellDefinition
        {
            Id = "tremor",
            Name = "Tremor",
            AssetId = "spell_tremor",
            Type = SpellType.Tremor,
            ManaCost = 3000,
            Cooldown = 0f,
            TargetType = SpellTargetType.AreaOfEffect,
            AreaRadius = 8f,
            ResearchPointsRequired = 1800,
            Effect = new DamageAreaEffect(damage: 800, radius: 8f)
        });

        registry.Register(new SpellDefinition
        {
            Id = "create_gold",
            Name = "Create Gold",
            AssetId = "spell_create_gold",
            Type = SpellType.CreateGold,
            ManaCost = 1000,
            Cooldown = 0f,
            TargetType = SpellTargetType.None,
            ResearchPointsRequired = 1000,
            Effect = new CreateGoldEffect(goldAmount: 1000)
        });

        registry.Register(new SpellDefinition
        {
            Id = "summon_horned_reaper",
            Name = "Summon Horned Reaper",
            AssetId = "spell_summon_horned_reaper",
            Type = SpellType.SummonHornedReaper,
            ManaCost = 10000,
            Cooldown = 0f,
            TargetType = SpellTargetType.None,
            ResearchPointsRequired = 5000,
            Effect = new SummonCreatureEffect(creatureType: "HornedReaper")
        });
    }
}
