using Sm.Core.Actions.Models;
using Sm.Core.StateMachine;

namespace Sm.Core.Converts.ToStateSettings
{
    public interface IStateSettingsBuilder<TState, TTrigger>
    {
        IStateSettingsBuilder<TState, TTrigger> SetState(TState state);

        IStateSettingsBuilder<TState, TTrigger> SetTransitions(params TriggerBehaviour<TState, TTrigger>[] transitions);

        IStateSettingsBuilder<TState, TTrigger> SetEntryAction(string actionName);

        IStateSettingsBuilder<TState, TTrigger> SetEntryAction(string actionName, ActionConfiguration configuration);

        IStateSettingsBuilder<TState, TTrigger> SetExitAction(string actionName);

        StateRepresentation<TState, TTrigger> Build();
    }
}
