using Ap.Core.Configurations;
using System;
using System.Collections.Generic;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Definitions;

public class TriggerContext : BaseContext
{
    internal TriggerContext(StateTrigger stateTrigger, IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        StateTrigger = stateTrigger;
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

        return context;
    }
}

public abstract class BaseContext
{
    public StateSetBase RootSet { get; internal set; }

    public StateSetBase CurrentSet { get; internal set; }

    public IServiceProvider ServiceProvider { get; set; }

    public StateTrigger StateTrigger { get; internal set; }

    public Dictionary<string, object> Properties { get; set; } = new();

    public StateSetConfiguration RootSetConfiguration { get; internal set; }
}