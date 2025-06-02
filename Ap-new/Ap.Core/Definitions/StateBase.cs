using Ap.Core.Behaviours;
using Ap.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Ap.Core.Definitions
{
    public abstract class StateBase(string name) : NodeBase, IState
    {
        public string Name { get; protected set; } = name;

        public Dictionary<string, IBehaviour> Transitions { get; } = new();

        public virtual TriggerDictionary GetTrigger()
        {
            var list = Transitions.Select(s => new TriggerResult(s.Key));
            return new(list);
        }

        public void Entry()
        {

        }

        public void Exit()
        {

        }

        public void AddTransition(IBehaviour behaviour)
        {
            Check(behaviour.Trigger);
            Transitions.Add(behaviour.Trigger, behaviour);
        }

        protected virtual void Check(string trigger)
        {
            if (Transitions.ContainsKey(trigger))
            {
                throw new ApAlreadyExistsException<StateDetail>(trigger, new StateDetail(Id, Name, Transitions));
            }
        }

        private class StateDetail(string id, string name, Dictionary<string, IBehaviour> transitions)
        {
            public string Id { get; } = id;
            public string Name { get; } = name;

            public Dictionary<string, IBehaviour> Transitions { get; } = transitions;
        }
    }
}
