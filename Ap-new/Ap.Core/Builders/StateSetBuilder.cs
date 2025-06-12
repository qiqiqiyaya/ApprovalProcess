using Ap.Core.Behaviours;
using Ap.Core.Definitions;
using Ap.Core.Definitions.Actions;
using Ap.Core.Definitions.States;
using Ap.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ap.Core.Builders
{
    public class StateSetBuilder : StateSetBuilder<IStateSetBuilder>, IStateSetBuilder
    {
        internal StateSetBuilder(string name, StateLinkedList? rootStateLinked = null)
            : base(name, rootStateLinked)
        {

        }

        internal StateSetBuilder(string name, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null)
            : base(name, rootStateLinked, action)
        {

        }

        internal StateSetBuilder(string name, string id, StateLinkedList? rootStateLinked = null)
            : base(name, id, rootStateLinked)
        {

        }

        internal StateSetBuilder(string name, string id, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null)
            : base(name, id, rootStateLinked, action)
        {

        }
    }

    public class StateSetBuilder<TBuilder> : IStateSetBuilder<TBuilder>
        where TBuilder : class
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public StateLinkedList StateLinked { get; private set; }

        public StateLinkedList RootStateLinked { get; }

        internal Action<string> AddTransition = _ => { };

        internal List<Action> JumpAction = new List<Action>();

        private readonly StateMachine _sm;
        protected IServiceProvider ServiceProvider;
        protected IStateSetBuilderProvider StateSetBuilderProvider;

        internal StateSetBuilder(string name, StateLinkedList? rootStateLinked = null)
            : this(name, Guid.NewGuid().ToString(), rootStateLinked)
        {
        }

        internal StateSetBuilder(string name, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null)
            : this(name, Guid.NewGuid().ToString(), rootStateLinked, action)
        {

        }

        internal StateSetBuilder(string name, string id, StateLinkedList? rootStateLinked = null)
            : this(name, id, rootStateLinked, null)
        {
        }

#pragma warning disable CS8618, CS9264
        internal StateSetBuilder(string name, string id, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null)
#pragma warning restore CS8618, CS9264
        {
            Id = id;
            if (rootStateLinked == null)
            {
                StateLinked = new StateLinkedList();
                RootStateLinked = StateLinked;
            }
            else
            {
                RootStateLinked = rootStateLinked;
                StateLinked = new StateLinkedList();
            }

            Start();
            Start(name, action);
            _sm = new StateMachine(StateLinked.OriginFirst.Value, RootStateLinked, id);
            Name = _sm.Name;
        }

        internal void Initial(IServiceProvider serviceProvider)
        {
            var creator = serviceProvider.GetRequiredService<CreateStateSetBuilderProvider>();
            ServiceProvider = serviceProvider;
            StateSetBuilderProvider = creator(serviceProvider, RootStateLinked);
        }

        private void Start()
        {
            var result = new StartState(Id);
            AddTransition = destination =>
            {
                result.AddTransition(new Direct(destination));
            };

            StateLinked.AddFirst(result);
        }

        private void Start(string name, Action<IState, string>? action = null)
        {
            CheckState(name);

            var result = new StateRepresentation(name);
            AddTransition(name);
            AddTransition = destination =>
            {
                if (action == null)
                {
                    result.AddTransition(new Submit(destination));
                }
                else
                {
                    action.Invoke(result, destination);
                }
            };
            StateLinked.AddLast(result);
        }

        public TBuilder Then(string name)
        {
            CheckState(name);

            var result = new StateRepresentation(name);
            StateLinked.AddLast(result);
            AddTransition(name);
            AddTransition = destination =>
            {
                var first = RootStateLinked.FirstState;
                result.AddTransition(new Approve(destination));
                result.AddTransition(new Reject(first.Name));
            };
            return (this as TBuilder)!;
        }

        public TBuilder Then(string name, Action<IState, string>? addTransition)
        {
            CheckState(name);

            var result = new StateRepresentation(name);
            StateLinked.AddLast(result);
            AddTransition(name);
            if (addTransition == null)
            {
                AddTransition = destination =>
                {
                    var first = RootStateLinked.FirstState;
                    result.AddTransition(new Approve(destination));
                    result.AddTransition(new Reject(first.Name));
                };
            }
            else
            {
                AddTransition = destination => addTransition(result, destination);
            }
            return (this as TBuilder)!;
        }

        public TBuilder BranchAnd(Action<BranchBuilder> branchAction)
        {
            return Branch(LogicalRelationship.And, branchAction);
        }

        public TBuilder BranchOr(Action<BranchBuilder> branchAction)
        {
            return Branch(LogicalRelationship.Or, branchAction);
        }

        public TBuilder Branch(LogicalRelationship relationship, Action<BranchBuilder> branchAction)
        {
            BranchBuilder branchBuilder = new BranchBuilder(StateSetBuilderProvider, relationship, RootStateLinked);

            branchAction.Invoke(branchBuilder);
            AddTransition(branchBuilder.State);
            var result = branchBuilder.Build(_sm);

            AddTransition = destination =>
            {
                result.AddTransition(new Direct(destination));
            };
            StateLinked.AddLast(result);
            return (this as TBuilder)!;
        }

        public void Complete(string name)
        {
            CheckState(name);
            Then(name);
            Complete();
        }

        public void Complete()
        {
            var last = StateLinked.OriginLast;
            if (last is EndState) return;

            var result = new EndState(Id);
            StateLinked.AddLast(result);
            AddTransition(result.Name);
        }

        public TBuilder If(Func<bool> action, string @true, string @false)
        {
            CheckState(@true);
            CheckState(@false);

            return If(action,
            provider => (TBuilder)provider.Create(@true, (result, destination) =>
                {
                    var first = RootStateLinked.FirstState;
                    result.AddTransition(new Approve(destination));
                    result.AddTransition(new Reject(first.Name));
                }),
                provider => (TBuilder)provider.Create(@false, (result, destination) =>
                {
                    var first = RootStateLinked.FirstState;
                    result.AddTransition(new Approve(destination));
                    result.AddTransition(new Reject(first.Name));
                }));
        }

        public TBuilder If(Func<bool> action,
            Func<IfBuilderProvider, TBuilder> @true,
            Func<IfBuilderProvider, TBuilder> @false)
        {
            var trueBuilder = (IStateSetBuilder<TBuilder>)@true
                .Invoke(new IfBuilderProvider(StateSetBuilderProvider, RootStateLinked));
            var falseBuilder = (IStateSetBuilder<TBuilder>)@false
                .Invoke(new IfBuilderProvider(StateSetBuilderProvider, RootStateLinked));

            trueBuilder.Complete();

            var sm = new IfContainer(Id, _sm, action, trueBuilder.Build(), falseBuilder.Build());

            AddTransition(sm.Name);
            AddTransition = destination =>
            {
                sm.AddTransition(new Direct(destination));
            };
            StateLinked.AddLast(sm);
            return (this as TBuilder)!;
        }

        /// <summary>
        /// Jumps to a specified state in the current set, cannot jump to child or parent level
        /// </summary>
        /// <param name="name"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public TBuilder Jump(string name, string destination)
        {
            CheckState(name);

            return Then(name, (stateNode, next) =>
            {
                JumpAction.Add(() =>
                {
                    // Cannot jump into any container
                    if (StateLinked.All(x => x.Name != destination))
                    {
                        throw new ArgumentException($"Destination state '{destination}' is not configured in the state set.", nameof(destination));
                    }

                    var first = RootStateLinked.FirstState;
                    stateNode.AddTransition(new Jump(destination));
                    stateNode.AddTransition(new Approve(next));
                    stateNode.AddTransition(new Reject(first.Name));
                });
            });
        }

        public TBuilder Children(Action<ContainerBuilder> builderAction)
        {
            ContainerBuilder container = new ContainerBuilder(StateSetBuilderProvider, RootStateLinked);
            builderAction(container);

            var setContainer = container.Build(_sm);

            AddTransition(setContainer.Name);
            AddTransition = destination =>
            {
                setContainer.AddTransition(new Direct(destination));
            };
            StateLinked.AddLast(setContainer);
            return (this as TBuilder)!;
        }

        /// <summary>
        /// Repeatedly entering a certain state
        /// </summary>
        /// <returns></returns>
        public TBuilder Reentry(string destination)
        {
            if (!StateLinked.Has(destination))
            {
                throw new ApNotFindException<StateLinkedList>($"Can't find '{destination}' of state in StateLikedList", StateLinked);
            }

            var state = StateLinked.Get(destination);
            state.AddTransition(new Reentry(destination));
            return (this as TBuilder)!;
        }

        /// <summary>
        /// Check if the state is configured in the root state set (including children state set).
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public virtual bool IsConfigured(string state)
        {
            return RootStateLinked.Has(state);
        }


        #region Entry
        public void ConfigureEntry<TEntryAction>(string stateName)
            where TEntryAction : IEntryAction
        {
            ConfigureEntry(stateName, new ApAction(typeof(TEntryAction)));
        }

        public void ConfigureEntry(string stateName, Func<EntryContext, ValueTask> entryAction)
        {
            ConfigureEntry(stateName, new ApAction(typeof(GeneralEntryAction), entryAction));
        }

        public void ConfigureEntry(string stateName, Action<EntryContext> entryAction)
        {
            ConfigureEntry(stateName, context =>
            {
                entryAction(context);
                return new ValueTask();
            });
        }

        public void ConfigureEntry<TEntryAction>(string stateName, params object[] parameters)
            where TEntryAction : IEntryAction
        {
            ConfigureEntry(stateName, new ApAction(typeof(TEntryAction), parameters));
        }

        public void ConfigureEntry(string stateName, ApAction action)
        {
            var actionType = typeof(IEntryAction);
            if (action.Type.GetInterfaces().All(type => type != actionType))
            {
                throw new Exception("the action.Type is not subclass of IEntryAction");
            }

            var state = RootStateLinked.Get(stateName);
            state.ActionConfiguration.EntryTypes.Add(action);
        }
        #endregion

        #region Exit
        public void ConfigureExit<TExitAction>(string name)
            where TExitAction : IExitAction
        {
            var state = RootStateLinked.Get(name);
            state.ActionConfiguration.ExitTypes.Add(new ApAction(typeof(TExitAction)));
        }

        public void ConfigureExit<TExitAction>(string name, params object[] parameters)
            where TExitAction : IExitAction
        {
            var state = RootStateLinked.Get(name);
            state.ActionConfiguration.ExitTypes.Add(new ApAction(typeof(TExitAction), parameters));
        }

        public void ConfigureExit(string name, ApAction action)
        {
            if (!action.Type.IsSubclassOf(typeof(IExitAction)))
            {
                throw new Exception("the action.Type is not subclass of IEntryAction");
            }

            var state = RootStateLinked.Get(name);
            state.ActionConfiguration.ExitTypes.Add(action);
        }
        #endregion

        public IStateSet Build()
        {
            if (StateLinked.Count == 0)
            {
                throw new ArgumentNullException($"State set is empty");
            }

            Complete();
            JumpAction.ForEach(s => s());

            foreach (var node in StateLinked.Skip(1))
            {
                _sm.Configure(node);
            }

            _sm.Name = Name;
            return _sm;
        }

        /// <summary>
        /// state name must be unique
        /// </summary>
        protected void CheckState(string name)
        {
            if (StateLinked.Has(name))
            {
                throw new ApAlreadyExistsException($"There already exists a state named '{name}'");
            }
        }
    }
}
