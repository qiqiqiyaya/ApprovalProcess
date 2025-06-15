using Ap.Core.Configurations;
using Ap.Core.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Ap.Core.Services.Interfaces;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Definitions;

public class TriggerContext : BaseContext
{
    internal TriggerContext(IServiceProvider serviceProvider,
        StateTrigger stateTrigger,
        Flow flow)
    {
        ServiceProvider = serviceProvider;
        StateTrigger = stateTrigger;
        Flow = flow;
    }

    internal ExitContext CreateExitContext()
    {
        var context = new ExitContext();
        context.StateTrigger = StateTrigger;
        context.RootSet = RootSet;
        context.CurrentSet = CurrentSet;
        context.ServiceProvider = ServiceProvider;
        context.StateTrigger = StateTrigger;
        context.Properties = Properties;
        context.RootSetConfiguration = RootSetConfiguration;
        context.Flow = Flow;
        context.State = State;
        //context.User = User;

        return context;
    }

    internal EntryContext CreateEntryContext()
    {
        var context = new EntryContext();
        context.StateTrigger = StateTrigger;
        context.RootSet = RootSet;
        context.CurrentSet = CurrentSet;
        context.ServiceProvider = ServiceProvider;
        context.StateTrigger = StateTrigger;
        context.Properties = Properties;
        context.RootSetConfiguration = RootSetConfiguration;
        context.Flow = Flow;
        context.State = State;
        //context.User = User;

        return context;
    }
}

public abstract class BaseContext
{
    public IServiceProvider ServiceProvider { get; set; }

    public StateTrigger StateTrigger { get; internal set; }

    public Flow Flow { get; internal set; }

    public IUser User { get; set; }

    public StateSetBase RootSet { get; internal set; }

    public StateSetBase CurrentSet { get; internal set; }

    public Dictionary<string, object> Properties { get; set; } = new();

    public StateSetConfiguration RootSetConfiguration { get; internal set; }

    //public IState State { get; internal set; }

    protected T GetRequiredService<T>() where T : notnull
    {
        return ServiceProvider.GetRequiredService<T>();
    }
}