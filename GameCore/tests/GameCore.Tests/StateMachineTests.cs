using GameCore.Common;
using GameCore.StateMachine;

namespace GameCore.Tests;

public class StateMachineTests
{
    private class TestState : IState<string>
    {
        public bool Entered { get; private set; }
        public bool Exited { get; private set; }
        public bool Updated { get; private set; }

        public void Enter(string context) => Entered = true;
        public void Exit(string context) => Exited = true;
        public void Update(string context, GameTime deltaTime) => Updated = true;
    }

    [Fact]
    public void TransitionTo_calls_Exit_on_old_state_and_Enter_on_new_state()
    {
        var sm = new StateMachine<string>();
        var oldState = new TestState();
        var newState = new TestState();

        sm.TransitionTo(oldState, "ctx");
        sm.TransitionTo(newState, "ctx");

        Assert.True(oldState.Exited);
        Assert.True(newState.Entered);
    }

    [Fact]
    public void Update_calls_Update_on_current_state()
    {
        var sm = new StateMachine<string>();
        var state = new TestState();
        sm.TransitionTo(state, "ctx");

        sm.Update("ctx", new GameTime(1, 0.016f, 0.016f));

        Assert.True(state.Updated);
    }

    [Fact]
    public void CurrentState_reflects_latest_transition()
    {
        var sm = new StateMachine<string>();
        var stateA = new TestState();
        var stateB = new TestState();

        sm.TransitionTo(stateA, "ctx");
        Assert.Same(stateA, sm.CurrentState);

        sm.TransitionTo(stateB, "ctx");
        Assert.Same(stateB, sm.CurrentState);
    }
}
