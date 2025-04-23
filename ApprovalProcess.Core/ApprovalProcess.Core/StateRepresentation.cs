using ApprovalProcess.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApprovalProcess.Core
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

        public StateRepresentation(TState state, IEnumerable<Transition<TState, TTrigger>> transitions)
        {
            State = state;
            TriggerBehaviours = transitions
                .GroupBy(t => t.Trigger)
                .ToDictionary(g => g.Key, g => (ICollection<Transition<TState, TTrigger>>)g.ToList());
        }

        public TState State { get; set; }

        /// <summary>
        /// 触发器行为
        /// </summary>
        public IDictionary<TTrigger, ICollection<Transition<TState, TTrigger>>> TriggerBehaviours =
            new Dictionary<TTrigger, ICollection<Transition<TState, TTrigger>>>();

        internal Dictionary<string, ISmAction?> EntryActions { get; } = new Dictionary<string, ISmAction?>();

        internal Dictionary<string, ISmAction?> ExitActions { get; } = new Dictionary<string, ISmAction?>();

        /// <summary>
        /// Accept the specified trigger and transition to the destination state.
        /// </summary>
        /// <param name="trigger">The accepted trigger.</param>
        /// <param name="destinationState">The state that the trigger will cause a transition to.</param>
        /// <returns>The receiver.</returns>
        public StateRepresentation<TState, TTrigger> Permit(TTrigger trigger, TState destinationState)
        {
            EnforceNotIdentityTransition(destinationState);
            AddTriggerBehaviour(new Transition<TState, TTrigger>(trigger, destinationState));
            return this;
        }

        private void AddTriggerBehaviour(Transition<TState, TTrigger> triggerBehaviour)
        {
            if (!TriggerBehaviours.TryGetValue(triggerBehaviour.Trigger, out ICollection<Transition<TState, TTrigger>> allowed))
            {
                allowed = new List<Transition<TState, TTrigger>>();
                TriggerBehaviours.Add(triggerBehaviour.Trigger, allowed);
            }

            allowed.Add(triggerBehaviour);
        }


        public bool TryFindBehaviour(TTrigger trigger, out ICollection<Transition<TState, TTrigger>> triggerBehaviours)
        {
            // Get list of candidate trigger handlers
            if (!TriggerBehaviours.TryGetValue(trigger, out ICollection<Transition<TState, TTrigger>> possible))
            {
                triggerBehaviours = null;
                return false;
            }

            triggerBehaviours = possible;
            return true;
        }

        private void EnforceNotIdentityTransition(TState destination)
        {
            if (destination.Equals(State))
            {
                throw new ArgumentException($"目标状态不能等于原状态 {State}");
            }
        }

        public void EntryAction(string actionName)
        {
            //if (!EntryActions.Contains(action))
            //{
            //    EntryActions.Add(action);
            //}
        }

        //public void EntryAction(ISmAction action)
        //{
        //    if (!EntryActions.Contains(action))
        //    {
        //        EntryActions.Add(action);
        //    }
        //}
    }
}
