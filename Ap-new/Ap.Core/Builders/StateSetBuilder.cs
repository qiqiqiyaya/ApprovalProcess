using Ap.Core.Behaviours;
using Ap.Core.Definitions;
using Ap.Core.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ap.Core.Builders
{
    public class StateSetBuilder : StateSetBuilder<StateSetBuilder>
    {
        internal StateSetBuilder(string state, StateLinkedList? rootStateLinked = null) : base(state, rootStateLinked)
        {

        }

        internal StateSetBuilder(string state, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null) : base(state, rootStateLinked, action)
        {

        }

        internal StateSetBuilder(string state, string id, StateLinkedList? rootStateLinked = null) : base(state, id, rootStateLinked)
        {

        }

        internal StateSetBuilder(string state, string id, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null) : base(state, id, rootStateLinked, action)
        {

        }
    }

    public class StateSetBuilder<TBuilder> : IStateSetBuilder<TBuilder>
        where TBuilder : class
    {
        public string Id { get; set; }

        public Dictionary<string, IState> StateDictionary { get; } = new Dictionary<string, IState>();

        public StateLinkedList StateLinked { get; private set; }

        public StateLinkedList RootStateLinked { get; }

        internal Action<string> AddTransition = _ => { };

        internal List<Action> JumpAction = new List<Action>();

        private readonly StateMachine _sm;

        internal StateSetBuilder(string state, StateLinkedList? rootStateLinked = null)
            : this(state, Guid.NewGuid().ToString(), rootStateLinked)
        {
        }

        internal StateSetBuilder(string state, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null)
            : this(state, Guid.NewGuid().ToString(), rootStateLinked, action)
        {

        }

        internal StateSetBuilder(string state, string id, StateLinkedList? rootStateLinked = null)
            : this(state, id, rootStateLinked, null)
        {
        }

        internal StateSetBuilder(string state, string id, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null)
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
            Start(state, action);
            _sm = new StateMachine(StateLinked.OriginFirst.Value, RootStateLinked, id);
        }

        private TBuilder Start()
        {
            var result = new StartState(Id);
            AddTransition = destination =>
            {
                result.AddTransition(new Direct(destination));
            };

            StateDictionary.Add(result.Name, result);
            StateLinked.AddFirst(result);
            return (this as TBuilder)!;
        }

        private TBuilder Start(string state, Action<IState, string>? action = null)
        {
            if (!StateDictionary.TryGetValue(state, out IState? result))
            {
                result = new StateRepresentation(state);

                AddTransition(state);
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
                StateDictionary.Add(state, result);
            }
            StateLinked.AddLast(result);
            return (this as TBuilder)!;
        }

        public TBuilder Then(string state)
        {
            CheckIsConfigured(state);

            var result = new StateRepresentation(state);
            StateDictionary.Add(state, result);
            StateLinked.AddLast(result);
            AddTransition(state);
            AddTransition = destination =>
            {
                var first = RootStateLinked.FirstState;
                result.AddTransition(new Approve(TransitionConst.Approve, destination));
                result.AddTransition(new Reject(TransitionConst.Reject, first.Name));
            };
            return (this as TBuilder)!;
        }

        public TBuilder Then(string state, Action<IState, string>? addTransition)
        {
            CheckIsConfigured(state);

            var result = new StateRepresentation(state);
            StateDictionary.Add(state, result);
            StateLinked.AddLast(result);
            AddTransition(state);
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

            StateDictionary.Add(branchBuilder.State, result);
            StateLinked.AddLast(result);

            return (this as TBuilder)!;
        }

        public void Complete(string state)
        {
            Then(state);
            Complete();
        }

        public void Complete()
        {
            var last = StateLinked.Last!.Value;
            if (last is EndState) return;

            var result = new EndState(Id);
            StateDictionary.Add(result.Name, result);
            StateLinked.AddLast(result);
            AddTransition(result.Name);
        }

        public TBuilder If(Func<bool> action, string @true, string @false)
        {

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
            Func<StateSetBuilderProvider, StateSetBuilder> @true,
            Func<StateSetBuilderProvider, StateSetBuilder> @false)
        {
            var trueBuilder = @true.Invoke(new StateSetBuilderProvider(RootStateLinked));
            var falseBuilder = @false.Invoke(new StateSetBuilderProvider(RootStateLinked));

            trueBuilder.Complete();

            var sm = new IfContainer(Id, _sm, action, trueBuilder.Build(), falseBuilder.Build());

            AddTransition(sm.Name);
            AddTransition = destination =>
            {
                sm.AddTransition(new Direct(destination));
            };
            StateDictionary.Add(sm.Name, sm);
            StateLinked.AddLast(sm);
            return (this as TBuilder)!;
        }

        /// <summary>
        /// Jumps to a specified state in the state set, cannot jump into any container
        /// </summary>
        /// <param name="state"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public TBuilder Jump(string state, string destination)
        {
            CheckIsConfigured(state);
            return Then(state, (stateNode, next) =>
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
            StateDictionary.Add(setContainer.Name, setContainer);
            StateLinked.AddLast(setContainer);
            return (this as TBuilder)!;
        }

        protected virtual void CheckIsConfigured(string state)
        {
            if (IsConfigured(state))
            {
                throw new ArgumentException($"State '{state}' is already configured in RootStateLinked.", nameof(state));
            }
        }

        public virtual bool IsConfigured(string state)
        {
            return RootStateLinked.TryGet(state, out _);
        }

        public IStateSet Build()
        {
            if (StateDictionary.Count == 0)
            {
                throw new ArgumentNullException(nameof(StateDictionary), "State set is empty");
            }

            Complete();
            JumpAction.ForEach(s => s());

            var value = StateDictionary.First().Value;
            StateDictionary.Remove(value.Name);

            foreach (var node in StateDictionary)
            {
                _sm.Configure(node.Value);
            }

            return _sm;
        }
    }
}
