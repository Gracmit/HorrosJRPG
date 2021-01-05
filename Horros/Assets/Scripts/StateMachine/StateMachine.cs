using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<IState, List<StateTransition>> _stateTransitions = new Dictionary<IState, List<StateTransition>>();
    
    private List<IState> _states;
    private IState _currentState;

    public void AddState(IState state)
    {
        _states.Add(state);
    }

    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        if(_stateTransitions.ContainsKey(from) == false)
            _stateTransitions[from] = new List<StateTransition>();
        
        _stateTransitions[from].Add(new StateTransition(from, to, condition));
    }

    public void SetState(IState state)
    {
        if (_currentState == state)
            return;
        
        _currentState?.OnExit();
        
        _currentState = state;
        _currentState.OnEnter();
    }

    public void Tick()
    {
        var transition = CheckForTransitions();
        if (transition != null)
        {
            SetState(transition.To);
        }
        _currentState.Tick();
    }

    private StateTransition CheckForTransitions()
    {
        if (_stateTransitions.ContainsKey(_currentState))
        {
            foreach (var transition in _stateTransitions[_currentState])
            {
                if (transition.Condition())
                    return transition;
            }
        }
        return null;
    }
}