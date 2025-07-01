using System.Collections.Generic;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Definitions;

public class TriggerContext : BaseContext
{
    internal TriggerContext(StateTrigger stateTrigger,
        Flow flow,
        IUser executor)
    {
        StateTrigger = stateTrigger;
        Flow = flow;
        Executor = executor;
    }

    internal TriggerContext(Flow flow,
        IUser executor)
    {
        Flow = flow;
        Executor = executor;
    }

    internal ExitContext CreateExitContext()
    {
        var context = new ExitContext
        {
            StateTrigger = StateTrigger,
            RootStateSet = RootStateSet,
            CurrentStateSet = CurrentStateSet,
            ServiceProvider = ServiceProvider,
            TriggeredTime = TriggeredTime
        };
        context.StateTrigger = StateTrigger;
        context.Properties = Properties;
        context.StateSetConfiguration = StateSetConfiguration;
        context.Flow = Flow;
        context.State = State;
        context.Executor = Executor;

        return context;
    }

    internal EntryContext CreateEntryContext()
    {
        var context = new EntryContext
        {
            StateTrigger = StateTrigger,
            RootStateSet = RootStateSet,
            CurrentStateSet = CurrentStateSet,
            ServiceProvider = ServiceProvider,
            TriggeredTime = TriggeredTime
        };
        context.StateTrigger = StateTrigger;
        context.Properties = Properties;
        context.StateSetConfiguration = StateSetConfiguration;
        context.Flow = Flow;
        context.State = State;
        context.Executor = Executor;

        return context;
    }

    public TriggerContext Clone()
    {
        return new TriggerContext(StateTrigger, Flow, Executor)
        {
            ServiceProvider = ServiceProvider,
            RootStateSet = RootStateSet,
            CurrentStateSet = CurrentStateSet,
            Properties = new Dictionary<string, object>(Properties),
            StateSetConfiguration = StateSetConfiguration,
            State = State
        };
    }
}
