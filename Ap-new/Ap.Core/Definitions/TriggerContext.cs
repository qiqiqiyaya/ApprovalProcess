using System;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Definitions;

public class TriggerContext(StateTrigger stateTrigger, IServiceProvider serviceProvider)
{
    public StateSetBase RootSet { get; internal set; }

    public StateSetBase CurrentSet { get; internal set; }

    public IServiceProvider ServiceProvider { get; set; } = serviceProvider;

    public StateTrigger StateTrigger { get; internal set; } = stateTrigger;
}