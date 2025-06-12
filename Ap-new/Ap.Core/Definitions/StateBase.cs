using Ap.Core.Behaviours;
using Ap.Core.Definitions.Actions;
using Ap.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public string Name { get; internal set; }

        public Dictionary<string, IBehaviour> Transitions { get; } = new();

        public ActionConfiguration ActionConfiguration { get; } = new();

        public virtual StateTriggerCollection GetTrigger()
        {
            var detail = ToDetail();
            var triggers = Transitions.Keys.Select(s => new StateTrigger(s, detail)).ToList();
            return new StateTriggerCollection(triggers);
        }

        public async ValueTask Entry(EntryContext context)
        {
            await context.PipelineRunAsync(ActionConfiguration.EntryTypes);
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
                throw new ApAlreadyExistsException<StateDetail>(trigger, ToDetail());
            }
        }

        protected List<Transition> ToTransition()
        {
            return Transitions.Values.Select(s => s.ToMap()).ToList();
        }

        public StateDetail ToDetail()
        {
            return new StateDetail(Id, Name, ToTransition());
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
