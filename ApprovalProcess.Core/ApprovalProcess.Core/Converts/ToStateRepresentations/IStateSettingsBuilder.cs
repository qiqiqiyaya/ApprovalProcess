using System;
using System.Collections.Generic;
using System.Text;

namespace ApprovalProcess.Core.Converts.ToStateRepresentations
{
    public interface IStateSettingsBuilder<TState, TTrigger>
    {
        IStateSettingsBuilder<TState, TTrigger> SetState(TState state);

        IStateSettingsBuilder<TState, TTrigger> SetTransition(Transition<TState, TTrigger> transition);

        IStateSettingsBuilder<TState, TTrigger> SetTransitions(IEnumerable<Transition<TState, TTrigger>> transitions);
    }
}
