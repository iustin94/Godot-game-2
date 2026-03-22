using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Core.StateMachine;

public interface IState<TContext>
{
    void Enter(TContext context);
    void Update(TContext context, GameTime deltaTime);
    void Exit(TContext context);
}
