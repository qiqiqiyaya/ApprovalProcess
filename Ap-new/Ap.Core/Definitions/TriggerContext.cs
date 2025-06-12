using System;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Definitions;

public class TriggerContext
{
    internal TriggerContext(StateTrigger stateTrigger, IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        StateTrigger = stateTrigger;
    }

    public StateSetBase RootSet { get; internal set; }

    public StateSetBase CurrentSet { get; internal set; }

    public IServiceProvider ServiceProvider { get; set; }

    public StateTrigger StateTrigger { get; internal set; }

    public EntryContext CreateEntryContext()
    {
        var context = new EntryContext(ServiceProvider, RootSet, CurrentSet, StateTrigger);
        return context;
    }
}