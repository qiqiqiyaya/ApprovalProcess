using ApNew.Nodes.Behaviours;
using ApNew.States;

namespace ApNew.Nodes
{
    public class StateSetBuilder
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public IDictionary<string, IState> Nodes { get; } = new Dictionary<string, IState>();

        public LinkedList<IState> LinkedList { get; } = new LinkedList<IState>();

        internal Action<string> AddTransition = destination => { };

        private readonly IStateSet _sm;

        public StateSetBuilder(string state)
        {
            Start(state);
            var value = Nodes.First().Value;
            _sm = new StateMachine(value);
        }

        public StateSetBuilder(string state, string id) : this(state)
        {
            Id = id;
        }

        private void Start(string state)
        {
            if (!Nodes.TryGetValue(state, out IState? result))
            {
                result = new StateBase(state);

                AddTransition = destination =>
                {
                    result.AddTransition(new Submit(TransitionConst.Submit, destination));
                };
                Nodes.Add(state, result);
            }
            LinkedList.AddFirst(result);
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
                result.AddTransition(new Approve(TransitionConst.Approve, destination));
                result.AddTransition(new Reject(TransitionConst.Reject, destination));
            };
            return this;
        }

        public BranchJoinBuilder Branch(Action<BranchBuilder> branchAction)
        {
            string state = "Branch";
            BranchBuilder branchBuilder = new BranchBuilder(state, LogicalRelationship.And);

            branchAction.Invoke(branchBuilder);

            AddTransition(state);
            var result = branchBuilder.Build(_sm);

            AddTransition = destination =>
            {
                result.AddTransition(new Direct("Direct", destination));
            };

            Nodes.Add(state, result);
            LinkedList.AddLast(result);

            return new BranchJoinBuilder(this);
        }

        public CompleteBuilder Complete(string state)
        {
            CompleteBuilder branchBuilder = new CompleteBuilder(this);

            if (!Nodes.TryGetValue(state, out IState? result))
            {
                result = new EndState(state);
                Nodes.Add(state, result);
            }
            LinkedList.AddLast(result);
            AddTransition(state);
            result.AddTransition(new Complete(TransitionConst.Approve, ""));
            return branchBuilder;
        }

        public void Complete(EndState state)
        {
            Nodes.Add(state.State, state);
            LinkedList.AddLast(state);
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
