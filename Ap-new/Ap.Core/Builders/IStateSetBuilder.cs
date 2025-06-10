using Ap.Core.Definitions;
using Ap.Core.Definitions.Actions;
using System;
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


        void ConfigureEntry<TEntryAction>(string name) where TEntryAction : IEntryAction;

        void ConfigureEntry(string name, Func<EntryContext, ValueTask> entryAction);

        void ConfigureEntry<TEntryAction>(string name, params object[] parameters) where TEntryAction : IEntryAction;

        void ConfigureEntry(string name, ApAction action);

        void ConfigureExit<TExitAction>(string name) where TExitAction : IExitAction;

        void ConfigureExit<TExitAction>(string name, params object[] parameters) where TExitAction : IExitAction;

        void ConfigureExit(string name, ApAction action);

        IStateSet Build();
    }
}
