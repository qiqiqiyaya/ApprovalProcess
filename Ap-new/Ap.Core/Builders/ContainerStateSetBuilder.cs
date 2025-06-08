using Ap.Core.Behaviours;
using Ap.Core.Definitions;
using System;
using System.Linq;

namespace Ap.Core.Builders
{
    public class ContainerStateSetBuilder : StateSetBuilder<IContainerStateSetBuilder>, IContainerStateSetBuilder
    {
        internal ContainerStateSetBuilder(string name, StateLinkedList? rootStateLinked = null) : base(name, rootStateLinked)
        {
        }

        internal ContainerStateSetBuilder(string name, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null) : base(name, rootStateLinked, action)
        {
        }

        internal ContainerStateSetBuilder(string name, string id, StateLinkedList? rootStateLinked = null) : base(name, id, rootStateLinked)
        {
        }

        internal ContainerStateSetBuilder(string name, string id, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null) : base(name, id, rootStateLinked, action)
        {
        }

        public virtual IContainerStateSetBuilder JumpOut(string name, string destination)
        {
            CheckState(name);

            Then(name, (stateNode, next) =>
            {
                JumpAction.Add(() =>
                {
                    // Transfer from container to parent or parent of parent
                    if (!IsConfigured(destination))
                    {
                        throw new ArgumentException($"Destination state '{destination}' is not configured in the state set.", nameof(destination));
                    }

                    // must jump out of the current set , Jump to child or parent level
                    if (StateLinked.Any(x => x.Name == destination))
                    {
                        throw new ArgumentException($"Destination state '{destination}' is not configured in the state set.", nameof(destination));
                    }

                    var first = RootStateLinked.FirstState;
                    stateNode.AddTransition(new JumpOut(ApCoreTriggers.Jump, destination));
                    stateNode.AddTransition(new Approve(next));
                    stateNode.AddTransition(new Reject(first.Name));
                });
            });

            return this;
        }
    }
}
