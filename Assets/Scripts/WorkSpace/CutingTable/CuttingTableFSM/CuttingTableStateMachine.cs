using System.Collections.Generic;
using WorkSpace.FSM;
using WorkSpace.FSM.State;

namespace WorkSpace.CutingTable.CuttingTableFSM
{
    public class CuttingTableStateMachine : StateMachine
    {
        private CuttingTable _context;

        public CuttingTableStateMachine(CuttingTable context, 
            Dictionary<BehaviourState, State> states, 
            List<Transition> transitions) : base(states, transitions)
        {
            _context = context;
        }
    }
}