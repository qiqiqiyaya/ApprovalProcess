using Ap.Core.Behaviours;
using Ap.Core.Definitions;
using Ap.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ap.Core.Builders
{
    public class ContainerBuilder : IContainerBuilder
    {
        public string Id { get; }

        public Dictionary<string, object> StateSetBuilderDic { get; } = new();

        public string State { get; protected set; }

        public StateLinkedList RootStateLinked { get; }

        public const string ContainerStateNamePrefix = "Container_";

        protected readonly IStateSetBuilderProvider StateSetBuilderProvider;

        public ContainerBuilder(IStateSetBuilderProvider stateSetBuilderProvider, StateLinkedList rootStateLinked)
            : this(stateSetBuilderProvider, Guid.NewGuid().ToString("N"), rootStateLinked)
        {

        }

        public ContainerBuilder(IStateSetBuilderProvider stateSetBuilderProvider, string id, StateLinkedList rootStateLinked)
        {
            Id = id;
            StateSetBuilderProvider = stateSetBuilderProvider;
            State = ContainerStateNamePrefix + Id;
            RootStateLinked = rootStateLinked;
        }

        public virtual IContainerStateSetBuilder New(string state, string id)
        {
            var containerBuilder = StateSetBuilderProvider.Create<IContainerStateSetBuilder>((_, _) =>
                {
                    var builder = new ContainerStateSetBuilder(state, id, RootStateLinked,
                        (result, destination) =>
                        {
                            var first = RootStateLinked.FirstState;
                            result.AddTransition(new Approve(destination));
                            result.AddTransition(new Reject(first.Name));
                        });

                    builder.Initial(StateSetBuilderProvider.ServiceProvider);
                    return builder;
                });

            StateSetBuilderDic.Add(containerBuilder.Name, containerBuilder);
            return containerBuilder;
        }

        public virtual IContainerStateSetBuilder New(string state)
        {
            return New(state, Guid.NewGuid().ToString("N"));
        }

        public virtual IStateSetContainer Build(StateSetBase parent)
        {
            CheckState();

            StateSetContainer container = new StateSetContainer(State, parent);
            foreach (var builder in StateSetBuilderDic)
            {
                var setBuilder = (ContainerStateSetBuilder)builder.Value;
                var set = setBuilder.Build();
                container.StateSets.Add(builder.Key, set);
            }

            return container;
        }

        /// <summary>
        /// state name must be unique
        /// </summary>
        protected void CheckState()
        {
            foreach (var builder in StateSetBuilderDic)
            {
                var setBuilder = (ContainerStateSetBuilder)builder.Value;
                foreach (var item in setBuilder.StateLinked)
                {
                    CheckState(item);
                }
            }
        }

        /// <summary>
        /// state name must be unique
        /// </summary>
        /// <param name="state"><see cref="IState"/> state</param>
        /// <exception cref="ApAlreadyExistsException"></exception>
        protected void CheckState(IState state)
        {
            foreach (var builder in StateSetBuilderDic)
            {
                var setBuilder = (ContainerStateSetBuilder)builder.Value;
                if (setBuilder.StateLinked.Has(s => s.Name == state.Name && s.Id != state.Id)
                    || RootStateLinked.Has(s => s.Name == state.Name && s.Id != state.Id))
                {
                    var linked = StateSetBuilderDic
                        .Select(s => (ContainerStateSetBuilder)s.Value)
                        .Select(s => s.StateLinked)
                        .ToList();

                    throw new ApAlreadyExistsException<List<StateLinkedList>>($"There already exists a state named '{state.Name}'", linked);
                }
            }
        }
    }
}
