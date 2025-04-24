using System;
using System.Collections.Generic;

namespace ApprovalProcess.Core.Converts.ToTransitions
{
    public class TransitionConContainer(Dictionary<string, ConvertMap> converters,
        IServiceProvider serviceProvider)
        : ToTransitionContainer(converters, serviceProvider);
}
