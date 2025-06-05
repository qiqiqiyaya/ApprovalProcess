using Ap.Core.Behaviours;
using Ap.Core.Definitions;
using Ap.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ap.Core.Builders
{
    public class StateSetBuilder : StateSetBuilder<StateSetBuilder>
    {
        internal StateSetBuilder(string name, StateLinkedList? rootStateLinked = null) : base(name, rootStateLinked)
        {

        }

        internal StateSetBuilder(string name, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null) : base(name, rootStateLinked, action)
        {

        }

        internal StateSetBuilder(string name, string id, StateLinkedList? rootStateLinked = null) : base(name, id, rootStateLinked)
        {

        }

        internal StateSetBuilder(string name, string id, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null) : base(name, id, rootStateLinked, action)
        {

        }
    }

    public class StateSetBuilder<TBuilder> : IStateSetBuilder<TBuilder>
        where TBuilder : class
    {
        public string Id { get; set; }

        //public Dictionary<string, IState> StateDictionary { get; } = new Dictionary<string, IState>();

        public StateLinkedList StateLinked { get; private set; }

        public StateLinkedList RootStateLinked { get; }

        internal Action<string> AddTransition = _ => { };

        internal List<Action> JumpAction = new List<Action>();

        private readonly StateMachine _sm;

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

        internal StateSetBuilder(string name, string id, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null)
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
                    result.AddTransition(new Submit(TransitionConst.Submit, destination));
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
                result.AddTransition(new Approve(TransitionConst.Approve, destination));
                result.AddTransition(new Reject(TransitionConst.Reject, first.Name));
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
                    result.AddTransition(new Approve(TransitionConst.Approve, destination));
                    result.AddTransition(new Reject(TransitionConst.Reject, first.Name));
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
            BranchBuilder branchBuilder = new BranchBuilder(relationship, RootStateLinked);

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
            var last = StateLinked.Last!.Value;
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
            provider => provider.Create(@true, (result, destination) =>
                {
                    var first = RootStateLinked.FirstState;
                    result.AddTransition(new Approve(TransitionConst.Approve, destination));
                    result.AddTransition(new Reject(TransitionConst.Reject, first.Name));
                }),
                provider => provider.Create(@false, (result, destination) =>
                {
                    var first = RootStateLinked.FirstState;
                    result.AddTransition(new Approve(TransitionConst.Approve, destination));
                    result.AddTransition(new Reject(TransitionConst.Reject, first.Name));
                }));
        }

        public TBuilder If(Func<bool> action,
            Func<IfBuilderProvider, StateSetBuilder> @true,
            Func<IfBuilderProvider, StateSetBuilder> @false)
        {
            var trueBuilder = @true.Invoke(new IfBuilderProvider(RootStateLinked));
            var falseBuilder = @false.Invoke(new IfBuilderProvider(RootStateLinked));

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
                    stateNode.AddTransition(new Jump(TransitionConst.Jump, destination));
                    stateNode.AddTransition(new Approve(TransitionConst.Approve, next));
                    stateNode.AddTransition(new Reject(TransitionConst.Reject, first.Name));
                });
            });
        }

        public TBuilder Children(Action<ContainerBuilder> builderAction)
        {
            ContainerBuilder container = new ContainerBuilder(RootStateLinked);
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
        /// Check if the state is configured in the root state set (including children state set).
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public virtual bool IsConfigured(string state)
        {
            return RootStateLinked.Has(state);
        }

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
