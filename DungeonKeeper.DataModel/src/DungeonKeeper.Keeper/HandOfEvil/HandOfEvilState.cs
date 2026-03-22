namespace DungeonKeeper.Keeper.HandOfEvil;

public class HandOfEvilState
{
    public HandInventory HandInventory { get; } = new();
    public string? SelectedSpellId { get; set; }
    public bool IsActive { get; set; }
}
