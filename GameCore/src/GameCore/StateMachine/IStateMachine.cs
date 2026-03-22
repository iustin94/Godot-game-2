using GameCore.Common;

namespace GameCore.StateMachine;

public interface IStateMachine<TContext>
{
    IState<TContext>? CurrentState { get; }
    void TransitionTo(IState<TContext> newState, TContext context);
    void Update(TContext context, GameTime deltaTime);
}
