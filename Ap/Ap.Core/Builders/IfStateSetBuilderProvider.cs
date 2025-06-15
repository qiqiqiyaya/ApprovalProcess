using Ap.Core.Behaviours;
using Ap.Core.Definitions;
using System;

namespace Ap.Core.Builders
{
    public class IfBuilderProvider(IStateSetBuilderProvider builderProvider, StateLinkedList rootStateLinked)
    {
        public virtual IStateSetBuilder Create(string state)
        {
            return builderProvider.Create(state, (result, destination) =>
            {
                var first = rootStateLinked.FirstState;
                result.AddTransition(new Approve(destination));
                result.AddTransition(new Reject(first.Name));
            });
        }

        public virtual IStateSetBuilder Create(string state, Action<IState, string> action)
        {
            return builderProvider.Create(state, action);
        }
    }
}
