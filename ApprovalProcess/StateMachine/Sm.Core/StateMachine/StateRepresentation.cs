using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;
using Sm.Core.Actions;
using Sm.Core.Actions.Entry;
using Sm.Core.Actions.Models;
using Sm.Share.Actions;

namespace Sm.Core.StateMachine
{
    /// <summary>
    /// 状态表示
    /// </summary>
    public class StateRepresentation<TState, TTrigger>
    {

        internal StateRepresentation()
        {
        }

        public StateRepresentation(TState state)
        {
            State = state;
        }

        public StateRepresentation(TState state, IEnumerable<TriggerBehaviour<TState, TTrigger>> transitions)
        {
            State = state;
            Transitions = transitions
                .GroupBy(t => t.Trigger)
                .ToDictionary(g => g.Key, g => (ICollection<TriggerBehaviour<TState, TTrigger>>)g.ToList());
        }

        public TState State { get; private set; }


        internal StateRepresentation<TState, TTrigger> SetState(TState state)
        {
            State = state;
            return this;
        }

        internal StateRepresentation<TState, TTrigger> SetTransitions(IEnumerable<TriggerBehaviour<TState, TTrigger>> transitions)
        {
            Transitions = transitions
                .GroupBy(t => t.Trigger)
                .ToDictionary(g => g.Key, g => (ICollection<TriggerBehaviour<TState, TTrigger>>)g.ToList());

            return this;
        }

        internal StateRepresentation<TState, TTrigger> SetEntryAction(string entryActionName)
        {
            EntryActions.Add(new StateSettingAction(entryActionName));
            return this;
        }

        internal StateRepresentation<TState, TTrigger> SetEntryAction(string entryActionName, ActionConfiguration configuration)
        {
            EntryActions.Add(new StateSettingAction(entryActionName, configuration));
            return this;
        }

        internal StateRepresentation<TState, TTrigger> SetExitAction(string exitActionName)
        {
            ExitActions.Add(exitActionName);
            return this;
        }

        internal StateRepresentation<TState, TTrigger> SetExitAction(string entryActionName, ActionConfiguration configuration)
        {
            //ExitActions.Add(exitActionName);
            return this;
        }

        /// <summary>
        /// 触发器行为
        /// </summary>
        public IDictionary<TTrigger, ICollection<TriggerBehaviour<TState, TTrigger>>> Transitions =
            new Dictionary<TTrigger, ICollection<TriggerBehaviour<TState, TTrigger>>>();

        //internal List<string> EntryActions { get; private set; } = new List<string>();
        internal List<StateSettingAction> EntryActions { get; private set; } = new List<StateSettingAction>();


        internal List<string> ExitActions { get; private set; } = new List<string>();

        internal StateRepresentationChildren<TState, TTrigger>? Children = null;

        internal void AddChildren(StateRepresentationChildren<TState, TTrigger> children)
        {
            Children = children;
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
            AddTransition(new TriggerBehaviour<TState, TTrigger>(trigger, destinationState));
            return this;
        }

        public StateRepresentation<TState, TTrigger> PermitReentry(TTrigger trigger)
        {
            AddTransition(new ReentryTriggerBehaviour<TState, TTrigger>(trigger, State));
            return this;
        }

        private void AddTransition(TriggerBehaviour<TState, TTrigger> triggerBehaviour)
        {
            if (!Transitions.TryGetValue(triggerBehaviour.Trigger, out ICollection<TriggerBehaviour<TState, TTrigger>> allowed))
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
            if (Transitions.TryGetValue(trigger, out ICollection<TriggerBehaviour<TState, TTrigger>> possible))
            {
                transitions = possible;
            }

            if (transitions.Count <= 1) return transitions.FirstOrDefault();

            var message = $"Multiple permitted exit transitions are configured from state '{State}' for trigger '{trigger}'. Guard clauses must be mutually exclusive.";
            throw new InvalidOperationException(message);
        }

        private void EnforceNotIdentityTransition(TState destination)
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

        internal async ValueTask Entry(EntryActionContext<TState, TTrigger> context)
        {
            var pipeline = context.GetEntryPipeline<TState, TTrigger>(EntryActions);
            if (pipeline == null) return;
            await pipeline.RunAsync(context);
        }

        public StateRepresentation<TState, TTrigger> OnEntry(string actionName)
        {
            EntryActions.Add(new StateSettingAction(actionName));
            return this;
        }

        public StateRepresentation<TState, TTrigger> OnEntry(string actionName, ActionConfiguration configuration)
        {
            EntryActions.Add(new StateSettingAction(actionName, configuration));
            return this;
        }

        public StateRepresentation<TState, TTrigger> OnEntry<TEntryAction>()
            where TEntryAction : IEntryAction<TState, TTrigger>
        {
            var type = typeof(TEntryAction);
            var actionName = type.GetCustomAttribute<ActionNameAttribute>();
            if (actionName == null)
            {
                throw new ArgumentException($"Action {type.Name} must have ActionNameAttribute");
            }
            OnEntry(actionName.Name);
            return this;
        }

        public StateRepresentation<TState, TTrigger> OnEntry<TEntryAction>(ActionConfiguration configuration)
            where TEntryAction : IEntryAction<TState, TTrigger>
        {
            var type = typeof(TEntryAction);
            var actionName = type.GetCustomAttribute<ActionNameAttribute>();
            if (actionName == null)
            {
                throw new ArgumentException($"Action {type.Name} must have ActionNameAttribute");
            }
            OnEntry(actionName.Name, configuration);
            return this;
        }

        //public void EntryAction(ISmAction action)
        //{
        //    if (!EntryActions.Contains(action))
        //    {
        //        EntryActions.Add(action);
        //    }
        //}

        //private StateSettingsList<TState, TTrigger> _children;

        //public StateRepresentation<TState, TTrigger> ParallelOr(TState completion, Action<RepresentationChildrenBuilder<TState, TTrigger>> buildChildren)
        //{
        //    if (_children != null) throw new ArgumentException("ParallelOr or ParallelAnd can only be called once");
        //    var builder = new RepresentationChildrenBuilder<TState, TTrigger>();
        //    buildChildren(builder);

        //    _children = new StateSettingsList<TState, TTrigger>(ParallelRelationship.Or);
        //    _children.AddRange(builder.Build());
        //    return this;
        //}

        //public StateRepresentation<TState, TTrigger> ParallelAnd(TState completion, Action<RepresentationChildrenBuilder<TState, TTrigger>> buildChildren)
        //{
        //    if (_children != null) throw new ArgumentException("ParallelOr or ParallelAnd can only be called once");
        //    var builder = new RepresentationChildrenBuilder<TState, TTrigger>();
        //    buildChildren(builder);

        //    _children = new StateSettingsList<TState, TTrigger>(ParallelRelationship.Or);
        //    _children.AddRange(builder.Build());
        //    return this;
        //}
    }

}
