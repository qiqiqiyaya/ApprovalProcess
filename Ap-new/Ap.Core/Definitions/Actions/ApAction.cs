using System;

namespace Ap.Core.Definitions.Actions
{
    public class ApAction(Type type, params object[] parameters)
    {
        public ApAction(Type type) : this(type, [])
        {

        }

        public Type Type { get; } = type;

        public object[] Parameters { get; } = parameters;
    }
}
