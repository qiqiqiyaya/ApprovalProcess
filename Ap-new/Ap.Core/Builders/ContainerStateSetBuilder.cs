using System;
using System.Linq;
using Ap.Core.Behaviours;
using Ap.Core.Definitions;

namespace Ap.Core.Builders
{
    public class ContainerStateSetBuilder : StateSetBuilder
    {
        internal ContainerStateSetBuilder(string state, StateLinkedList? rootStateLinked = null) : base(state, rootStateLinked)
        {
        }

        internal ContainerStateSetBuilder(string state, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null) : base(state, rootStateLinked, action)
        {
        }

        internal ContainerStateSetBuilder(string state, string id, StateLinkedList? rootStateLinked = null) : base(state, id, rootStateLinked)
        {
        }

        internal ContainerStateSetBuilder(string state, string id, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null) : base(state, id, rootStateLinked, action)
        {
        }

        public virtual void JumpOut(string state, string destination)
        {
            CheckIsConfigured(state);
            Then(state, (stateNode, next) =>
            {
                JumpAction.Add(() =>
                {
                    // Transfer from container to parent or parent of parent
                    if (IsConfigured(destination))
                    {
                        throw new ArgumentException($"Destination state '{destination}' is not configured in the state set.", nameof(destination));
                    }

                    // Can only jump out of the current set
                    if (StateLinked.Any(x => x.Name == destination))
                    {
                        throw new ArgumentException($"Destination state '{destination}' is not configured in the state set.", nameof(destination));
                    }

                    var first = RootStateLinked.FirstState;
                    stateNode.AddTransition(new JumpOut(TransitionConst.Jump, destination));
                    stateNode.AddTransition(new Approve(TransitionConst.Approve, next));
                    stateNode.AddTransition(new Reject(TransitionConst.Reject, first.Name));
                });
            });

        }
    }
}
