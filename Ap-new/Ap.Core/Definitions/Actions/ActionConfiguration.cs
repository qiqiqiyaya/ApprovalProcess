using System;
using System.Collections.Generic;

namespace Ap.Core.Definitions.Actions
{
    public class ActionConfiguration
    {
        public List<ApAction> EntryTypes { get; } = new List<ApAction>();

        public List<ApAction> ExitTypes { get; } = new List<ApAction>();
    }

    public class ApAction
    {
        public ApAction(Type type) : this(type, [])
        {

        }

        public ApAction(Type type, params object[] parameters)
        {
            Type = type;
            Parameters = parameters;
        }

        public Type Type { get; }

        public object[] Parameters { get; }
    }
}
