using Ap.Core.Definitions;
using System;
using System.Collections.Generic;

namespace Ap.Core.Builders
{
    public interface IStateSetBuilder : IStateSetBuilder<IStateSetBuilder>
    {

    }

    public interface IStateSetBuilder<out TStateSetBuilder>
    {
        string Id { get; }

        StateLinkedList StateLinked { get; }

        StateLinkedList RootStateLinked { get; }

        TStateSetBuilder Then(string name);

        TStateSetBuilder Then(string name, Action<IState, string>? addTransition);

        TStateSetBuilder BranchAnd(Action<BranchBuilder> branchAction);

        TStateSetBuilder BranchOr(Action<BranchBuilder> branchAction);

        TStateSetBuilder Branch(LogicalRelationship relationship, Action<BranchBuilder> branchAction);

        void Complete(string name);

        void Complete();

        TStateSetBuilder If(Func<bool> action, string @true, string @false);

        TStateSetBuilder If(Func<bool> action,
            Func<StateSetBuilderProvider, StateSetBuilder> @true,
            Func<StateSetBuilderProvider, StateSetBuilder> @false);

        TStateSetBuilder Jump(string name, string destination);

        TStateSetBuilder Children(Action<ContainerBuilder> builderAction);

        /// <summary>
        /// Check if the state is configured in the root state set (including children state set).
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        bool IsConfigured(string state);

        IStateSet Build();
    }
}
