using Ap.Core.Behaviours;
using Ap.Core.Configurations;
using Ap.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
    public abstract class StateBase : NodeBase, IState
    {
        protected StateBase(string name)
            : this(name, Guid.NewGuid().ToString("N"))
        {

        }

        protected StateBase(string name, string id)
            : this(name, id, StateType.General)

        {

        }
        protected StateBase(string name, StateType stateType)
            : this(name, Guid.NewGuid().ToString("N"), stateType)
        {

        }

        protected StateBase(string name, string id, StateType stateType)
        {
            Name = name;
            Id = id;
            StateType = stateType;
        }

        public string Name { get; internal set; }

        public StateType StateType { get; set; }

        public Dictionary<string, IBehaviour> Transitions { get; } = new();

        public StateConfiguration StateConfiguration { get; } = new();

        public IServiceProvider ServiceProvider { get; set; }

        public virtual ValueTask<StateTriggerCollection> GetTrigger()
        {
            var detail = ToDetail();
            var triggers = Transitions.Keys.Select(s => new StateTrigger(s, detail)).ToList();
            return new ValueTask<StateTriggerCollection>(new StateTriggerCollection(triggers));
        }

        public virtual async ValueTask Entry(EntryContext context)
        {
            await context.ActionRunAsync(StateConfiguration);
        }

        public virtual async ValueTask Exit(ExitContext context)
        {
            await context.ActionRunAsync(StateConfiguration);
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
            return new StateDetail(Id, Name);
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
