using Ap.Core.Behaviours;
using Ap.Core.Definitions;
using System;

namespace Ap.Core.Builders
{
    public class IfBuilderProvider(IStateSetBuilderProvider stateSetBuilder, StateLinkedList rootStateLinked) : IIfBuilderProvider
    {
        public virtual IStateSetBuilder<IStateSetBuilder> Create(string state)
        {
            return stateSetBuilder.Create(state, (result, destination) =>
            {
                var first = rootStateLinked.FirstState;
                result.AddTransition(new Approve(TransitionConst.Approve, destination));
                result.AddTransition(new Reject(TransitionConst.Reject, first.Name));
            });
        }

        public virtual IStateSetBuilder<IStateSetBuilder> Create(string state, Action<IState, string> action)
        {
            return stateSetBuilder.Create(state, action);
        }
    }
}
