﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Core.StateMachine
{
    /// <summary>
    /// trigger with some conditions
    /// </summary>
    public class ConditionalTrigger
    {
        public ConditionalOperator Operator { get; set; }

        public ICollection<ICondition> Conditions { get; set; }
    }

    public interface ICompositeCondition : ICondition
    {
        ConditionalOperator Operator { get; set; }

        ICollection<ICondition> Conditions { get; set; }
    }

    public enum ConditionalOperator
    {
        And,
        Or,
        Composite
    }

    /// <summary>
    /// Condition
    /// </summary>
    public interface ICondition
    {
        ValueTask<bool> ExecuteAsync();
    }
}
