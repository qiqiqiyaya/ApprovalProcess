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
    public class StateSettings<TState, TTrigger>
    {

        internal StateSettings()
        {
        }

        public StateSettings(TState state)
        {
            State = state;
        }

        public StateSettings(TState state, IEnumerable<Transition<TState, TTrigger>> transitions)
        {
            State = state;
            TriggerBehaviours = transitions
                .GroupBy(t => t.Trigger)
                .ToDictionary(g => g.Key, g => (ICollection<Transition<TState, TTrigger>>)g.ToList());
        }

        public TState State { get; private set; }


        internal StateSettings<TState, TTrigger> SetState(TState state)
        {
            State = state;
            return this;
        }

        internal StateSettings<TState, TTrigger> SetTransitions(IEnumerable<Transition<TState, TTrigger>> transitions)
        {
            TriggerBehaviours = transitions
                .GroupBy(t => t.Trigger)
                .ToDictionary(g => g.Key, g => (ICollection<Transition<TState, TTrigger>>)g.ToList());

            return this;
        }

        internal StateSettings<TState, TTrigger> SetEntryAction(string entryActionName)
        {
            EntryActions.Add(new StateSettingAction(entryActionName));
            return this;
        }

        internal StateSettings<TState, TTrigger> SetEntryAction(string entryActionName, ActionConfiguration configuration)
        {
            EntryActions.Add(new StateSettingAction(entryActionName, configuration));
            return this;
        }

        internal StateSettings<TState, TTrigger> SetExitAction(string exitActionName)
        {
            ExitActions.Add(exitActionName);
            return this;
        }

        internal StateSettings<TState, TTrigger> SetExitAction(string entryActionName, ActionConfiguration configuration)
        {
            //ExitActions.Add(exitActionName);
            return this;
        }

        /// <summary>
        /// 触发器行为
        /// </summary>
        public IDictionary<TTrigger, ICollection<Transition<TState, TTrigger>>> TriggerBehaviours =
            new Dictionary<TTrigger, ICollection<Transition<TState, TTrigger>>>();

        //internal List<string> EntryActions { get; private set; } = new List<string>();
        internal List<StateSettingAction> EntryActions { get; private set; } = new List<StateSettingAction>();


        internal List<string> ExitActions { get; private set; } = new List<string>();


        /// <summary>
        /// Accept the specified trigger and transition to the destination state.
        /// </summary>
        /// <param name="trigger">The accepted trigger.</param>
        /// <param name="destinationState">The state that the trigger will cause a transition to.</param>
        /// <returns>The receiver.</returns>
        public StateSettings<TState, TTrigger> Permit(TTrigger trigger, TState destinationState)
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

        internal async ValueTask Entry(EntryActionContext<TState, TTrigger> context)
        {
            var pipeline = context.GetEntryPipeline<TState, TTrigger>(EntryActions);
            if (pipeline == null) return;
            await pipeline.RunAsync(context);
        }

        public StateSettings<TState, TTrigger> OnEntry(string actionName)
        {
            EntryActions.Add(new StateSettingAction(actionName));
            return this;
        }

        public StateSettings<TState, TTrigger> OnEntry(string actionName, ActionConfiguration configuration)
        {
            EntryActions.Add(new StateSettingAction(actionName, configuration));
            return this;
        }

        public StateSettings<TState, TTrigger> OnEntry<TEntryAction>()
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

        public StateSettings<TState, TTrigger> OnEntry<TEntryAction>(ActionConfiguration configuration)
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

        private readonly StateSettingsList<TState, TTrigger> _children = new StateSettingsList<TState, TTrigger>();

        public StateSettings<TState, TTrigger> Children(TState completion, ParallelRelationship relationship, Action<StateSettingsBuilder<TState, TTrigger>> buildChildren)
        {
            var builder = new StateSettingsBuilder<TState, TTrigger>();
            buildChildren(builder);

            _children.AddRange(builder.Build());
            return this;
        }
    }

    public class StateSettingsList<TState, TTrigger> : List<StateSettings<TState, TTrigger>>
    {
        public ParallelRelationship Relationship { get; set; }
    }

    public class StateSettingsBuilder<TState, TTrigger>
    {
        private readonly List<StateSettings<TState, TTrigger>> _stateSettings = new List<StateSettings<TState, TTrigger>>();

        public StateSettings<TState, TTrigger> New(TState state)
        {
            var newOne = new StateSettings<TState, TTrigger>(state);
            _stateSettings.Add(newOne);
            return newOne;
        }

        internal List<StateSettings<TState, TTrigger>> Build()
        {
            return _stateSettings;
        }
    }

    public enum ParallelRelationship
    {
        And,
        Or
    }
}
