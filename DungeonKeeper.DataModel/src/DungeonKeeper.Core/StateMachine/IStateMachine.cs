using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Core.StateMachine;

public interface IStateMachine<TContext>
{
    IState<TContext>? CurrentState { get; }
    void TransitionTo(IState<TContext> newState, TContext context);
    void Update(TContext context, GameTime deltaTime);
}
