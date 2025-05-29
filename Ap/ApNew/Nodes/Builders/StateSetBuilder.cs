using ApNew.Nodes.Behaviours;
using ApNew.Nodes.Core;
using ApNew.Nodes.States;

namespace ApNew.Nodes.Builders
{
    public class StateSetBuilder : IStateSetBuilder
    {
        public string Id { get; set; }

        public IDictionary<string, IState> StateDictionary { get; } = new Dictionary<string, IState>();

        public StateLinkedList StateLinked { get; private set; }

        public StateLinkedList RootStateLinked { get; }

        internal Action<string> AddTransition = _ => { };

        private readonly StateMachine _sm;

        public StateSetBuilder(string state)
            : this(state, Guid.NewGuid().ToString())
        {
        }

        public StateSetBuilder(string state, string id)
            : this(state, id, null, null)
        {
        }

        public StateSetBuilder(string state, string id, StateLinkedList? rootStateLinked = null, Action<IState, string>? action = null)
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

            Start().Start(state, action);
            _sm = new StateMachine(StateLinked.OriginFirst.Value, RootStateLinked);
        }

        private StateSetBuilder Start()
        {
            var result = new StartState(Id);
            AddTransition = destination =>
            {
                result.AddTransition(new Direct(destination));
            };

            StateDictionary.Add(result.State, result);
            StateLinked.AddFirst(result);
            return this;
        }

        private StateSetBuilder Start(string state, Action<IState, string>? action = null)
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
                        action?.Invoke(result, destination);
                    }
                };
                StateDictionary.Add(state, result);
            }
            StateLinked.AddLast(result);
            return this;
        }

        public StateSetBuilder Then(string state)
        {
            CheckIsConfigured(state);

            var result = new StateRepresentation(state);
            StateDictionary.Add(state, result);
            StateLinked.AddLast(result);
            AddTransition(state);
            AddTransition = destination =>
            {
                var first = RootStateLinked.FirstState!;
                result.AddTransition(new Approve(TransitionConst.Approve, destination));
                result.AddTransition(new Reject(TransitionConst.Reject, first.State));
            };
            return this;
        }

        public BranchJoinBuilder BranchAnd(Action<BranchBuilder> branchAction)
        {
            return Branch(LogicalRelationship.And, branchAction);
        }

        public BranchJoinBuilder BranchOr(Action<BranchBuilder> branchAction)
        {
            return Branch(LogicalRelationship.Or, branchAction);
        }

        public BranchJoinBuilder Branch(LogicalRelationship relationship, Action<BranchBuilder> branchAction)
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

            return new BranchJoinBuilder(this);
        }

        public CompleteBuilder Complete(string state)
        {
            return Then(state).Complete();
        }

        public CompleteBuilder Complete()
        {
            CompleteBuilder branchBuilder = new CompleteBuilder(this);

            var result = new EndState(Id);
            StateDictionary.Add(result.State, result);
            StateLinked.AddLast(result);
            AddTransition(result.State);

            return branchBuilder;
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
            foreach (var item in RootStateLinked)
            {
                switch (item)
                {
                    case IStateSet set:
                        if (set.Nodes.ContainsKey(state)) return true;
                        break;
                    case IStateSetContainer container:
                        if (container.IsConfigured(state)) return true;
                        break;
                    default:
                        if (item.State == state) return true;
                        break;
                }
            }

            return false;
        }

        internal IStateSet Build()
        {
            if (StateDictionary.Count == 0)
            {
                throw new ArgumentNullException(nameof(StateDictionary), "State set is empty");
            }

            var value = StateDictionary.First().Value;
            StateDictionary.Remove(value.State);

            foreach (var node in StateDictionary)
            {
                _sm.Configure(node.Value);
            }

            return _sm;
        }
    }
}
