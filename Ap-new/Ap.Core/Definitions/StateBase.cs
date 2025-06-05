using System;
using Ap.Core.Behaviours;
using Ap.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Ap.Core.Definitions
{
    public abstract class StateBase : NodeBase, IState
    {
        protected StateBase(string name) : this(name, Guid.NewGuid().ToString("N"))
        {

        }

        protected StateBase(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; protected set; }

        public Dictionary<string, IBehaviour> Transitions { get; } = new();

        public virtual TriggerDictionary GetTrigger()
        {
            var node = new StateNode(Id, Name)
            {
                Triggers = Transitions.Select(s => s.Key).ToList()
            };
            return new(node);
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
