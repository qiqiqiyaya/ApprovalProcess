namespace ApprovalProcess.Core.Converts.ToStateSettings
{
    public interface IStateSettingsBuilder<TState, TTrigger>
    {
        IStateSettingsBuilder<TState, TTrigger> SetState(TState state);

        IStateSettingsBuilder<TState, TTrigger> SetTransitions(params Transition<TState, TTrigger>[] transitions);

        IStateSettingsBuilder<TState, TTrigger> SetEntryActions(params string[] actionName);

        IStateSettingsBuilder<TState, TTrigger> SetExitActions(params string[] actionName);

        StateSettings<TState, TTrigger> Build();
    }
}
