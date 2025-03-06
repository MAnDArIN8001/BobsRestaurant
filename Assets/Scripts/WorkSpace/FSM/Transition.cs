using System;
using WorkSpace.FSM.State;

namespace WorkSpace.FSM
{
    public class Transition
    {
        public BehaviourState From { get; }
        public BehaviourState To { get; }

        public Func<bool> Condition { get; }

        public Transition(BehaviourState from, BehaviourState to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}