using System;
using System.Collections.Generic;
using Ap.Core.Behaviours;
using Ap.Core.Definitions;

namespace Ap.Core.Builders
{
    public class ContainerBuilder : IContainerBuilder
    {
        public string Id { get; }

        public Dictionary<string, object> StateSetBuilderDic { get; } = new();

        public string State { get; protected set; }

        public StateLinkedList RootStateLinked { get; }

        public const string ContainerStateNamePrefix = "Container_";

        public ContainerBuilder(StateLinkedList rootStateLinked) : this(Guid.NewGuid().ToString("N"), rootStateLinked)
        {

        }

        public ContainerBuilder(string id, StateLinkedList rootStateLinked)
        {
            Id = id;

            State = ContainerStateNamePrefix + Id;
            RootStateLinked = rootStateLinked;
        }

        public virtual IContainerStateSetBuilder New(string state, string id)
        {
            var provider = new StateSetBuilderProvider(RootStateLinked);

            var containerBuilder = provider.Create<IContainerStateSetBuilder>(
                () => new ContainerStateSetBuilder(state, id, RootStateLinked,
                    (result, destination) =>
                    {
                        var first = RootStateLinked.FirstState;
                        result.AddTransition(new Approve(TransitionConst.Approve, destination));
                        result.AddTransition(new Reject(TransitionConst.Reject, first.Name));
                    }));

            StateSetBuilderDic.Add(containerBuilder.Id, containerBuilder);
            return containerBuilder;
        }

        public virtual IContainerStateSetBuilder New(string state)
        {
            return New(state, Guid.NewGuid().ToString("N"));
        }

        public virtual IStateSetContainer Build(StateSetBase parent)
        {
            StateSetContainer container = new StateSetContainer(State, parent);

            foreach (var builder in StateSetBuilderDic)
            {
                var setBuilder = (ContainerStateSetBuilder)builder.Value;
                setBuilder.Complete();
                var set = setBuilder.Build();

                container.StateSets.Add(builder.Key, set);
            }

            return container;
        }
    }
}
