namespace WorkSpace.FSM.State
{
    public abstract class State
    {
        public BehaviourState StateType { get; protected set; }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}