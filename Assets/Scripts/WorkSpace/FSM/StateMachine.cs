using System.Collections.Generic;
using UnityEngine;
using WorkSpace.FSM.State;

namespace WorkSpace.FSM
{
    public abstract class StateMachine
    {
        protected State.State _currentState;

        protected Dictionary<BehaviourState, State.State> _states;

        protected List<Transition> _transitions;

        protected StateMachine(Dictionary<BehaviourState, State.State> states, List<Transition> transitions)
        {
            _states = states;
            _transitions = transitions;
        }

        public virtual void Update()
        {
            _currentState?.Update();

            for (int i = 0; i < _transitions.Count; i++)
            {
                var transition = _transitions[i];

                if (transition.From == _currentState.StateType && transition.Condition())
                {
                    SetState(transition.To);

                    break;
                }
            }
        }

        public virtual void SetState(BehaviourState newState)
        {
            if (_states.TryGetValue(newState, out var state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
            else
            {
                Debug.LogWarning($"The state machine {this} doesnt contains state {newState}");
            }
        }

        public virtual void AddTransitionRule(Transition transitionRule)
        {
            if (_transitions.Contains(transitionRule))
            {
                Debug.LogWarning($"The State machine {this} already contains transition {transitionRule}");

                return;
            }
            
            _transitions.Add(transitionRule);
        }
    }
}