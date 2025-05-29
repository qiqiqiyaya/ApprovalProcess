using System.Collections.Generic;

namespace Sm.Core.StateMachine
{
    public class StateMachineConfiguration
    {
        private readonly Dictionary<string, StateMachine<string, string>> _stateMachines = new Dictionary<string, StateMachine<string, string>>();

        public StateMachineConfiguration() { }

        public void Add(StateMachine<string, string> stateMachine)
        {
            _stateMachines.Add(stateMachine.Id, stateMachine);
        }

        public StateMachine<string, string> Get(string id)
        {
            return _stateMachines[id];
        }
    }
}
