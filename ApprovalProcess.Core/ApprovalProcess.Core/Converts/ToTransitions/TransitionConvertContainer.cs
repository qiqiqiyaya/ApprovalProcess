using System;
using System.Collections.Generic;
using System.Text;

namespace ApprovalProcess.Core.Converts.ToTransitions
{
    public class TransitionConvertContainer : ConvertContainer
    {
        public TransitionConvertContainer(Dictionary<string, ConvertConfiguration> converters, IServiceProvider serviceProvider)
            : base(converters, serviceProvider)
        {

        }
    }
}
