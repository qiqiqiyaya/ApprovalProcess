using Sm.Core.Actions.Models;
using Sm.Core.StateMachine;

namespace Sm.Core.Converts.ToStateSettings
{
    internal class StateSettingsBuilder<TState, TTrigger> : IStateSettingsBuilder<TState, TTrigger>
    {
        private readonly StateConfiguration<TState, TTrigger> _configuration;

        public StateSettingsBuilder()
        {
            _configuration = new StateConfiguration<TState, TTrigger>();
        }

        public IStateSettingsBuilder<TState, TTrigger> SetState(TState state)
        {
            _configuration.State = state;
            return this;
        }

        public IStateSettingsBuilder<TState, TTrigger> SetTransitions(params TriggerBehaviour<TState, TTrigger>[] transitions)
        {
            _configuration.Transitions.AddRange(transitions);
            return this;
        }

        public IStateSettingsBuilder<TState, TTrigger> SetEntryAction(string actionName)
        {
            _configuration.EntryActionNames.Add(actionName, null);
            return this;
        }

        public IStateSettingsBuilder<TState, TTrigger> SetEntryAction(string actionName, ActionConfiguration configuration)
        {
            _configuration.EntryActionNames.Add(actionName, configuration);
            return this;
        }

        public IStateSettingsBuilder<TState, TTrigger> SetExitAction(string actionName)
        {
            _configuration.ExitActionNames.Add(actionName, null);
            return this;
        }

        public StateRepresentation<TState, TTrigger> Build()
        {
            var stateSettings = new StateRepresentation<TState, TTrigger>();
            stateSettings.SetState(_configuration.State)
                .SetTransitions(_configuration.Transitions);

            foreach (var action in _configuration.EntryActionNames)
            {
                if (action.Value != null) stateSettings.SetEntryAction(action.Key, action.Value);
                else stateSettings.SetEntryAction(action.Key);
            }

            foreach (var action in _configuration.ExitActionNames)
            {
                if (action.Value != null) stateSettings.SetExitAction(action.Key, action.Value);
                else stateSettings.SetExitAction(action.Key);
            }

            return stateSettings;
        }
    }
}
