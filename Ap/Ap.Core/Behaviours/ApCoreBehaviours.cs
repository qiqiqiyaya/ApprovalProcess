using System;
using System.Collections.Generic;

namespace Ap.Core.Behaviours
{
    public class ApCoreBehaviours
    {
        public static Dictionary<string, Type> Behaviours = new()
        {
            [ApCoreTriggers.Submit] = typeof(Submit),
            [ApCoreTriggers.Approve] = typeof(Approve),
            [ApCoreTriggers.Reject] = typeof(Reject),
            [ApCoreTriggers.Complete] = typeof(Complete),
            [ApCoreTriggers.Direct] = typeof(Direct),
            [ApCoreTriggers.Jump] = typeof(Jump),
        };
    }
}
