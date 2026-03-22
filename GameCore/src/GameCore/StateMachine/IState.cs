using GameCore.Common;

namespace GameCore.StateMachine;

public interface IState<TContext>
{
    void Enter(TContext context);
    void Update(TContext context, GameTime deltaTime);
    void Exit(TContext context);
}
