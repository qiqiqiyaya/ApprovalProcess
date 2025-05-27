using ApNew.Nodes.Behaviours;
using ApNew.States;

namespace ApNew.Nodes
{
    public class StateSetBuilder
    {
        public string Id { get; set; }

        public IDictionary<string, IState> Nodes { get; } = new Dictionary<string, IState>();

        public LinkedList<IState> LinkedList { get; } = new LinkedList<IState>();

        internal Action<string> AddTransition = destination => { };

        private readonly IStateSet _sm;

        public StateSetBuilder(string state) : this(state, Guid.NewGuid().ToString())
        {
        }

        public StateSetBuilder(string state, string id) : this(state, id, null)
        {
        }

        public StateSetBuilder(string state, string id, Action<IState, string>? action = null)
        {
            Id = id;
            Start().Start(state, action);
            var value = Nodes.First().Value;
            _sm = new StateMachine(value);
        }

        private StateSetBuilder Start()
        {
            var result = new StartState(Id);
            AddTransition = destination =>
            {
                result.AddTransition(new Direct(destination));
            };

            Nodes.Add(result.State, result);
            LinkedList.AddFirst(result);
            return this;
        }

        private StateSetBuilder Start(string state, Action<IState, string>? action = null)
        {
            if (!Nodes.TryGetValue(state, out IState? result))
            {
                result = new StateBase(state);

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
                Nodes.Add(state, result);
            }
            LinkedList.AddLast(result);
            return this;
        }

        public StateSetBuilder Then(string state)
        {
            if (!Nodes.TryGetValue(state, out IState? result))
            {
                result = new StateBase(state);
                Nodes.Add(state, result);
            }
            LinkedList.AddLast(result);
            AddTransition(state);
            AddTransition = destination =>
            {
                var first = LinkedList.First!.Value;

                result.AddTransition(new Approve(TransitionConst.Approve, destination));
                result.AddTransition(new Reject(TransitionConst.Reject, first.State));
            };
            return this;
        }

        public BranchJoinBuilder Branch(Action<BranchBuilder> branchAction)
        {
            string state = "Branch";
            BranchBuilder branchBuilder = new BranchBuilder(state, LogicalRelationship.And, this);

            branchAction.Invoke(branchBuilder);

            AddTransition(state);
            var result = branchBuilder.Build(_sm);

            AddTransition = destination =>
            {
                result.AddTransition(new Direct(destination));
            };

            Nodes.Add(state, result);
            LinkedList.AddLast(result);

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
            Nodes.Add(result.State, result);
            LinkedList.AddLast(result);
            AddTransition(result.State);

            return branchBuilder;
        }

        protected void HandleTransition(string state)
        {

        }

        protected void AddToLast(string currentState, Func<INodeBehaviour> creator)
        {
            var node = LinkedList.Last;
            if (node == null)
            {
                throw new InvalidOperationException("LinkedList is empty, cannot add to last.");
            }

            var nodeState = node.Value;
            var behaviour = creator();

        }
        protected void AddToCurrent(string state)
        {

        }

        internal IStateSet Build()
        {
            if (Nodes.Count == 0)
            {
                throw new ArgumentNullException(nameof(Nodes), "State set is empty");
            }

            var value = Nodes.First().Value;
            Nodes.Remove(value.State);

            foreach (var node in Nodes)
            {
                _sm.Configure(node.Value);
            }

            return _sm;
        }
    }
}
