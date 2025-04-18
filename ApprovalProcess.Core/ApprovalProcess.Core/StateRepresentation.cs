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

        public TState State { get; set; }

        public ICollection<TransitioningTriggerBehaviour<TState, TTrigger>> Behaviours { get; set; } =
            new List<TransitioningTriggerBehaviour<TState, TTrigger>>();

        private IDictionary<TTrigger, ICollection<TransitioningTriggerBehaviour<TState, TTrigger>>> _triggerBehaviours;
        /// <summary>
        /// 触发器行为
        /// </summary>
        public IDictionary<TTrigger, ICollection<TransitioningTriggerBehaviour<TState, TTrigger>>> TriggerBehaviours
        {
            get
            {
                if (_triggerBehaviours == null)
                {
                    _triggerBehaviours = new Dictionary<TTrigger, ICollection<TransitioningTriggerBehaviour<TState, TTrigger>>>();
                    foreach (var group in Behaviours.GroupBy(s => s.Trigger))
                    {
                        _triggerBehaviours.Add(group.Key, group.ToList());
                    }
                }

                return _triggerBehaviours;
            }
        }


        /// <summary>
        /// Accept the specified trigger and transition to the destination state.
        /// </summary>
        /// <param name="trigger">The accepted trigger.</param>
        /// <param name="destinationState">The state that the trigger will cause a transition to.</param>
        /// <returns>The receiver.</returns>
        public StateRepresentation<TState, TTrigger> Permit(TTrigger trigger, TState destinationState)
        {
            EnforceNotIdentityTransition(destinationState);
            AddTriggerBehaviour(new TransitioningTriggerBehaviour<TState, TTrigger>(trigger, destinationState));
            return this;
        }

        private void AddTriggerBehaviour(TransitioningTriggerBehaviour<TState, TTrigger> triggerBehaviour)
        {
            if (!TriggerBehaviours.TryGetValue(triggerBehaviour.Trigger, out ICollection<TransitioningTriggerBehaviour<TState, TTrigger>> allowed))
            {
                allowed = new List<TransitioningTriggerBehaviour<TState, TTrigger>>();
                TriggerBehaviours.Add(triggerBehaviour.Trigger, allowed);
            }

            allowed.Add(triggerBehaviour);
            Behaviours.Add(triggerBehaviour);
        }


        public bool TryFindBehaviour(TTrigger trigger, out ICollection<TransitioningTriggerBehaviour<TState, TTrigger>> triggerBehaviours)
        {
            // Get list of candidate trigger handlers
            if (!TriggerBehaviours.TryGetValue(trigger, out ICollection<TransitioningTriggerBehaviour<TState, TTrigger>> possible))
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
    }
}
