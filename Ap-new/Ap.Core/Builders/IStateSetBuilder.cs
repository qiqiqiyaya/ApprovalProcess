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

        Dictionary<string, IState> StateDictionary { get; }

        StateLinkedList StateLinked { get; }

        StateLinkedList RootStateLinked { get; }

        TStateSetBuilder Then(string state);

        TStateSetBuilder Then(string state, Action<IState, string>? addTransition);

        TStateSetBuilder BranchAnd(Action<BranchBuilder> branchAction);

        TStateSetBuilder BranchOr(Action<BranchBuilder> branchAction);

        TStateSetBuilder Branch(LogicalRelationship relationship, Action<BranchBuilder> branchAction);

        void Complete(string state);

        void Complete();

        TStateSetBuilder If(Func<bool> action, string @true, string @false);

        TStateSetBuilder If(Func<bool> action,
            Func<StateSetBuilderProvider, StateSetBuilder> @true,
            Func<StateSetBuilderProvider, StateSetBuilder> @false);

        TStateSetBuilder Jump(string state, string destination);

        TStateSetBuilder Children(Action<ContainerBuilder> builderAction);

        bool IsConfigured(string state);

        IStateSet Build();
    }
}
