using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Combat;

public interface ICombatResolver
{
    CombatRound ResolveTick(IReadOnlyList<CombatParticipant> participants, GameTime time);
}
