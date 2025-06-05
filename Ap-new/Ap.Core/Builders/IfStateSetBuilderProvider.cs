using Ap.Core.Behaviours;
using Ap.Core.Definitions;
using System;

namespace Ap.Core.Builders
{
    public class IfBuilderProvider(StateLinkedList rootStateLinked)
    {
        private readonly StateSetBuilderProvider _builderProvider = new StateSetBuilderProvider(rootStateLinked);

        public virtual IStateSetBuilder<StateSetBuilder> Create(string state)
        {
            return _builderProvider.Create(state, (result, destination) =>
            {
                var first = rootStateLinked.FirstState;
                result.AddTransition(new Approve(TransitionConst.Approve, destination));
                result.AddTransition(new Reject(TransitionConst.Reject, first.Name));
            });
        }

        public virtual StateSetBuilder Create(string state, Action<IState, string> action)
        {
            return new StateSetBuilder(state, rootStateLinked, action);
        }
    }
}
