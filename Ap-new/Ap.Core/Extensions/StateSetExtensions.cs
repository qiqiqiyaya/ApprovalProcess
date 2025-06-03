using System;

namespace Ap.Core.Definitions;

public static class StateSetExtensions
{
    public static void ExecuteTrigger(this IStateSet set, string trigger)
    {
        if (string.IsNullOrWhiteSpace(trigger)) throw new ArgumentNullException(nameof(trigger));
        set.ExecuteTrigger(new StateTrigger() { Trigger = trigger, StateSetId = set.Id });
    }


    public static void ExecuteTrigger(this IStateSet set, string stateSetId, string trigger)
    {
        if (string.IsNullOrWhiteSpace(trigger)) throw new ArgumentNullException(nameof(trigger));
        set.ExecuteTrigger(new StateTrigger() { Trigger = trigger, StateSetId = stateSetId });
    }
}