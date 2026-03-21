using DungeonKeeper.Core.Common;
using DungeonKeeper.Core.Entities;
using DungeonKeeper.Core.StateMachine;

namespace DungeonKeeper.Creatures.AI;

public sealed class CreatureStateMachine
{
    private readonly StateMachine<IEntity> _stateMachine = new();
    private readonly Dictionary<CreatureState, IState<IEntity>> _stateMap = new();

    public CreatureState CurrentCreatureState { get; private set; } = CreatureState.Idle;

    public void RegisterState(CreatureState state, IState<IEntity> stateImpl)
    {
        _stateMap[state] = stateImpl;
    }

    public void TransitionTo(CreatureState state, IEntity context)
    {
        if (_stateMap.TryGetValue(state, out var stateImpl))
        {
            CurrentCreatureState = state;
            _stateMachine.TransitionTo(stateImpl, context);
        }
    }

    public void Update(IEntity context, GameTime time)
    {
        _stateMachine.Update(context, time);
    }
}
