using DungeonKeeper.Core.Common;

namespace DungeonKeeper.Core.StateMachine;

public class StateMachine<TContext> : IStateMachine<TContext>
{
    public IState<TContext>? CurrentState { get; private set; }

    public void TransitionTo(IState<TContext> newState, TContext context)
    {
        CurrentState?.Exit(context);
        CurrentState = newState;
        CurrentState.Enter(context);
    }

    public void Update(TContext context, GameTime deltaTime)
    {
        CurrentState?.Update(context, deltaTime);
    }
}
