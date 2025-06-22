using Ap.Core.Actions;
using Ap.Core.Definitions;
using Ap.Core.Definitions.Actions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Builders
{
    public interface IStateSetBuilder : IStateSetBuilder<IStateSetBuilder>
    {

    }

    public interface IStateSetBuilder<TStateSetBuilder>
    {
        /// <summary>
        /// IStateSet name
        /// </summary>
        public string Name { get; set; }

        string Id { get; set; }

        StateLinkedList StateLinked { get; }

        StateLinkedList RootStateLinked { get; }

        TStateSetBuilder Then(string name);

        TStateSetBuilder Then(string name, Action<IState, string>? addTransition);

        TStateSetBuilder BranchAnd(Action<BranchBuilder> branchAction);

        TStateSetBuilder BranchOr(Action<BranchBuilder> branchAction);

        TStateSetBuilder Branch(LogicalRelationship relationship, Action<BranchBuilder> branchAction);

        void Complete(string name);

        void Complete();

        TStateSetBuilder If(Func<PredicateContext, bool> predicate,
           Func<IfBuilderProvider, TStateSetBuilder> @true,
           Func<IfBuilderProvider, TStateSetBuilder> @false);

        TStateSetBuilder If(Func<PredicateContext, ValueTask<bool>> predicate,
            Func<IfBuilderProvider, TStateSetBuilder> @true,
            Func<IfBuilderProvider, TStateSetBuilder> @false);
        TStateSetBuilder If<TIIfPredicate>(
            Func<IfBuilderProvider, TStateSetBuilder> @true,
            Func<IfBuilderProvider, TStateSetBuilder> @false)
            where TIIfPredicate : IIfPredicate;

        TStateSetBuilder If(ApAction apAction,
                    Func<IfBuilderProvider, TStateSetBuilder> @true,
                    Func<IfBuilderProvider, TStateSetBuilder> @false);

        TStateSetBuilder Jump(string name, string destination);

        TStateSetBuilder Children(Action<ContainerBuilder> builderAction);

        /// <summary>
        /// Check if the state is configured in the root state set (including children state set).
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        bool IsConfigured(string state);

        void AssignApprover<TAssignApproverService>()
           where TAssignApproverService : AssignApprover;
        void AssignApprover(Func<EntryContext, ValueTask<List<string>>> assignAction);

        void AssignApprover<TAssignApproverService>(string stateName)
           where TAssignApproverService : AssignApprover;

        void AssignApprover(string stateName, Func<EntryContext, ValueTask<List<string>>> assignAction);

        void EntryAction<TEntryAction>(string stateName) where TEntryAction : IEntryAction;

        void EntryAction(string stateName, Func<EntryContext, ValueTask> entryAction);

        void EntryAction(string stateName, Action<EntryContext> entryAction);

        void EntryAction<TEntryAction>(string stateName, params object[] parameters) where TEntryAction : IEntryAction;

        void EntryAction(string stateName, ApAction action);

        void ExitAction<TExitAction>(string name) where TExitAction : IExitAction;

        void ExitAction<TExitAction>(string name, params object[] parameters) where TExitAction : IExitAction;

        void ExitAction(string name, ApAction action);

        IStateSet Build();
    }
}
