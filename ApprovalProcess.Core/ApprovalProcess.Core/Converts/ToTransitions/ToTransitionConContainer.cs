using System;
using System.Collections.Generic;

namespace ApprovalProcess.Core.Converts.ToTransitions
{
    public class ToTransitionConContainer(Dictionary<string, ConvertMap> converters,
        IServiceProvider serviceProvider)
        : ToTransitionContainer(converters, serviceProvider);
}
