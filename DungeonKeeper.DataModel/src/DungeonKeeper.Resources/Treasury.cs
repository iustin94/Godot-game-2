namespace DungeonKeeper.Resources;

public class Treasury
{
    public const int DefaultBaseCapacity = 16000;
    public const int CapacityPerTreasuryTile = 3000;

    /// <summary>
    /// Base gold capacity provided by the dungeon heart.
    /// </summary>
    public int BaseCapacity { get; set; } = DefaultBaseCapacity;

    /// <summary>
    /// Additional gold capacity provided by treasury room tiles.
    /// </summary>
    public int RoomCapacity { get; set; }

    /// <summary>
    /// Total gold storage capacity.
    /// </summary>
    public int TotalCapacity => BaseCapacity + RoomCapacity;
}
