using Ap.Flow.Behaviours;

namespace Ap.Flow
{
    /// <summary>
    /// 状态表示
    /// </summary>
    public class StateRepresentation<TState, TTrigger>
    {
        public StateRepresentation(TState state)
        {
            State = state;
        }

        public TState State { get; private set; }

        /// <summary>
        /// 触发器行为
        /// </summary>
        public IDictionary<TTrigger, ICollection<TriggerBehaviour<TState, TTrigger>>> Transitions =
            new Dictionary<TTrigger, ICollection<TriggerBehaviour<TState, TTrigger>>>();

        //internal List<string> EntryActions { get; private set; } = new List<string>();
        //internal List<StateSettingAction> EntryActions { get; private set; } = new List<StateSettingAction>();


        internal List<string> ExitActions { get; private set; } = new List<string>();

        internal StateMachine<TState, TTrigger>? Children { get; private set; }

        internal void AddChildren(StateMachine<TState, TTrigger> children)
        {
            Children = children;
        }

        internal bool HasChildren()
        {
            return Children != null;
        }


        /// <summary>
        /// Accept the specified trigger and transition to the destination state.
        /// </summary>
        /// <param name="trigger">The accepted trigger.</param>
        /// <param name="destinationState">The state that the trigger will cause a transition to.</param>
        /// <returns>The receiver.</returns>
        public virtual StateRepresentation<TState, TTrigger> Permit(TTrigger trigger, TState destinationState)
        {
            EnforceNotIdentityTransition(destinationState);
            AddTransition(new TransitionTriggerBehaviour<TState, TTrigger>(trigger, destinationState));
            return this;
        }

        public StateRepresentation<TState, TTrigger> PermitReentry(TTrigger trigger)
        {
            AddTransition(new ReentryTriggerBehaviour<TState, TTrigger>(trigger, State));
            return this;
        }

        public virtual StateRepresentation<TState, TTrigger> ForkOr(TTrigger trigger)
        {
            if (Transitions.ContainsKey(trigger))
            {
                throw new ArgumentException($"The trigger '{trigger}' is already registered for state '{State}'.");
            }

            AddTransition(new ForkOrTriggerBehaviour<TState, TTrigger>(trigger, State));
            return this;
        }

        protected void AddTransition(TriggerBehaviour<TState, TTrigger> triggerBehaviour)
        {
            if (!Transitions.TryGetValue(triggerBehaviour.Trigger, out ICollection<TriggerBehaviour<TState, TTrigger>>? allowed))
            {
                allowed = new List<TriggerBehaviour<TState, TTrigger>>();
                Transitions.Add(triggerBehaviour.Trigger, allowed);
            }

            allowed.Add(triggerBehaviour);
        }


        public TriggerBehaviour<TState, TTrigger> FindTriggerBehaviour(TTrigger trigger)
        {
            // Get list of candidate trigger handlers
            ICollection<TriggerBehaviour<TState, TTrigger>> transitions = new List<TriggerBehaviour<TState, TTrigger>>();
            if (Transitions.TryGetValue(trigger, out ICollection<TriggerBehaviour<TState, TTrigger>>? possible))
            {
                transitions = possible;
            }

            if (transitions.Count <= 1) return transitions.FirstOrDefault();

            var message = $"Multiple permitted exit transitions are configured from state '{State}' for trigger '{trigger}'. Guard clauses must be mutually exclusive.";
            throw new InvalidOperationException(message);
        }

        protected void EnforceNotIdentityTransition(TState destination)
        {
            if (destination.Equals(State))
            {
                throw new ArgumentException($"Permit() (and PermitIf()) require that the destination state is not equal to the source state. To accept a trigger without changing state, use either Ignore() or PermitReentry().");
            }
        }

        public void Exit(Transition<TState, TTrigger> transition)
        {
            ExecuteExitActions(transition);
        }

        void ExecuteExitActions(Transition<TState, TTrigger> transition)
        {

        }
    }

}
