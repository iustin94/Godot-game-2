using DungeonKeeper.Creatures.Definitions;

namespace DungeonKeeper.Keeper.HeroInvasion;

public record InvasionGroup(CreatureType HeroType, int Count, int Level);
