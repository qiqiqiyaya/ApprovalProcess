namespace ApprovalProcess.Core.Converts.ToStateSettings
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

        public IStateSettingsBuilder<TState, TTrigger> SetTransitions(params Transition<TState, TTrigger>[] transitions)
        {
            _configuration.Transitions.AddRange(transitions);
            return this;
        }

        public IStateSettingsBuilder<TState, TTrigger> SetEntryActions(params string[] actionName)
        {
            _configuration.EntryActionNames.AddRange(actionName);
            return this;
        }

        public IStateSettingsBuilder<TState, TTrigger> SetExitActions(params string[] actionName)
        {
            _configuration.EntryActionNames.AddRange(actionName);
            return this;
        }

        public StateSettings<TState, TTrigger> Build()
        {
            var stateSettings = new StateSettings<TState, TTrigger>();
            stateSettings.SetState(_configuration.State)
                .SetTransitions(_configuration.Transitions);

            return stateSettings;
        }
    }
}
