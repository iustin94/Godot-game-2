using DungeonKeeper.Combat;

namespace DungeonKeeper.Traps.Data;

public static class DK2TrapData
{
    public static void RegisterAllTraps(TrapDefinitionRegistry registry)
    {
        registry.Register(new TrapDefinition
        {
            Id = "sentry", Name = "Sentry Trap", AssetId = "trap_sentry",
            Type = TrapType.Sentry, ManufactureCost = 300, ManufactureTime = 100,
            Damage = 100, DamageType = DamageType.Physical,
            TriggerRadius = 2f, TriggerType = TrapTriggerType.Proximity,
            IsReusable = true, RearmTime = 5f
        });

        registry.Register(new TrapDefinition
        {
            Id = "boulder", Name = "Boulder", AssetId = "trap_boulder",
            Type = TrapType.Boulder, ManufactureCost = 500, ManufactureTime = 200,
            Damage = 500, DamageType = DamageType.Physical,
            TriggerRadius = 1f, TriggerType = TrapTriggerType.Pressure,
            IsReusable = false, RearmTime = 0f
        });

        registry.Register(new TrapDefinition
        {
            Id = "lightning", Name = "Lightning Trap", AssetId = "trap_lightning",
            Type = TrapType.LightningTrap, ManufactureCost = 400, ManufactureTime = 150,
            Damage = 200, DamageType = DamageType.Lightning,
            TriggerRadius = 3f, TriggerType = TrapTriggerType.Proximity,
            IsReusable = true, RearmTime = 8f
        });

        registry.Register(new TrapDefinition
        {
            Id = "spike", Name = "Spike Trap", AssetId = "trap_spike",
            Type = TrapType.SpikeTrap, ManufactureCost = 200, ManufactureTime = 80,
            Damage = 150, DamageType = DamageType.Physical,
            TriggerRadius = 1f, TriggerType = TrapTriggerType.Pressure,
            IsReusable = true, RearmTime = 4f
        });

        registry.Register(new TrapDefinition
        {
            Id = "fear", Name = "Fear Trap", AssetId = "trap_fear",
            Type = TrapType.FearTrap, ManufactureCost = 350, ManufactureTime = 120,
            Damage = 0, DamageType = DamageType.Dark,
            TriggerRadius = 3f, TriggerType = TrapTriggerType.Proximity,
            IsReusable = true, RearmTime = 10f,
            AppliedEffects = new[] { "Fear" }
        });

        registry.Register(new TrapDefinition
        {
            Id = "poison_gas", Name = "Poison Gas", AssetId = "trap_poison_gas",
            Type = TrapType.PoisonGas, ManufactureCost = 300, ManufactureTime = 100,
            Damage = 50, DamageType = DamageType.Poison,
            TriggerRadius = 3f, TriggerType = TrapTriggerType.Proximity,
            IsReusable = true, RearmTime = 6f
        });

        registry.Register(new TrapDefinition
        {
            Id = "freeze", Name = "Freeze Trap", AssetId = "trap_freeze",
            Type = TrapType.Freeze, ManufactureCost = 400, ManufactureTime = 150,
            Damage = 0, DamageType = DamageType.Physical,
            TriggerRadius = 2f, TriggerType = TrapTriggerType.Proximity,
            IsReusable = true, RearmTime = 12f,
            AppliedEffects = new[] { "Frozen" }
        });

        registry.Register(new TrapDefinition
        {
            Id = "alarm", Name = "Alarm", AssetId = "trap_alarm",
            Type = TrapType.Alarm, ManufactureCost = 100, ManufactureTime = 50,
            Damage = 0, DamageType = DamageType.Physical,
            TriggerRadius = 5f, TriggerType = TrapTriggerType.Alarm,
            IsReusable = true, RearmTime = 2f
        });

        registry.Register(new TrapDefinition
        {
            Id = "fireburst", Name = "Fireburst", AssetId = "trap_fireburst",
            Type = TrapType.Fireburst, ManufactureCost = 450, ManufactureTime = 180,
            Damage = 250, DamageType = DamageType.Fire,
            TriggerRadius = 3f, TriggerType = TrapTriggerType.Proximity,
            IsReusable = true, RearmTime = 10f
        });

        registry.Register(new TrapDefinition
        {
            Id = "jack_in_the_box", Name = "Jack-in-the-Box", AssetId = "trap_jack",
            Type = TrapType.JackInTheBox, ManufactureCost = 350, ManufactureTime = 120,
            Damage = 100, DamageType = DamageType.Physical,
            TriggerRadius = 2f, TriggerType = TrapTriggerType.Proximity,
            IsReusable = true, RearmTime = 8f,
            AppliedEffects = new[] { "Fear" }
        });

        registry.Register(new TrapDefinition
        {
            Id = "trigger", Name = "Trigger Trap", AssetId = "trap_trigger",
            Type = TrapType.Trigger, ManufactureCost = 150, ManufactureTime = 60,
            Damage = 0, DamageType = DamageType.Physical,
            TriggerRadius = 1f, TriggerType = TrapTriggerType.Manual,
            IsReusable = true, RearmTime = 1f
        });
    }

    public static void RegisterAllDoors(DoorDefinitionRegistry registry)
    {
        registry.Register(new DoorDefinition
        {
            Id = "wooden", Name = "Wooden Door", AssetId = "door_wooden",
            Type = DoorType.Wooden, ManufactureCost = 200, ManufactureTime = 80,
            Health = 500, IsLocked = false, IsSecret = false, IsMagic = false
        });

        registry.Register(new DoorDefinition
        {
            Id = "braced", Name = "Braced Door", AssetId = "door_braced",
            Type = DoorType.Braced, ManufactureCost = 400, ManufactureTime = 150,
            Health = 1000, IsLocked = true, IsSecret = false, IsMagic = false
        });

        registry.Register(new DoorDefinition
        {
            Id = "steel", Name = "Steel Door", AssetId = "door_steel",
            Type = DoorType.Steel, ManufactureCost = 600, ManufactureTime = 200,
            Health = 2000, IsLocked = true, IsSecret = false, IsMagic = false
        });

        registry.Register(new DoorDefinition
        {
            Id = "magic", Name = "Magic Door", AssetId = "door_magic",
            Type = DoorType.Magic, ManufactureCost = 1000, ManufactureTime = 300,
            Health = 3000, IsLocked = true, IsSecret = false, IsMagic = true
        });

        registry.Register(new DoorDefinition
        {
            Id = "secret", Name = "Secret Door", AssetId = "door_secret",
            Type = DoorType.Secret, ManufactureCost = 500, ManufactureTime = 150,
            Health = 800, IsLocked = false, IsSecret = true, IsMagic = false
        });
    }
}
