using DungeonKeeper.Combat;
using DungeonKeeper.Traps;

namespace DungeonKeeper.Traps.Tests;

public class TrapDefinitionRegistryTests
{
    private static TrapDefinition CreateDefinition(string id = "spike-trap") => new()
    {
        Id = id,
        Name = "Spike Trap",
        AssetId = "trap_spike",
        Type = TrapType.SpikeTrap,
        ManufactureCost = 500,
        ManufactureTime = 100,
        Damage = 50,
        DamageType = DamageType.Physical,
        TriggerRadius = 1.5f,
        TriggerType = TrapTriggerType.Pressure,
        IsReusable = true,
        RearmTime = 5f
    };

    [Fact]
    public void Register_and_Get_work()
    {
        var registry = new TrapDefinitionRegistry();
        var def = CreateDefinition();

        registry.Register(def);
        var result = registry.Get("spike-trap");

        Assert.Same(def, result);
    }

    [Fact]
    public void GetAll_returns_registered_traps()
    {
        var registry = new TrapDefinitionRegistry();
        var def1 = CreateDefinition("trap-a");
        var def2 = CreateDefinition("trap-b");

        registry.Register(def1);
        registry.Register(def2);

        var all = registry.GetAll();
        Assert.Equal(2, all.Count);
        Assert.Contains(all, d => d.Id == "trap-a");
        Assert.Contains(all, d => d.Id == "trap-b");
    }
}
