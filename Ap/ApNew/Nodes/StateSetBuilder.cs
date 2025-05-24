using ApNew.Nodes.Transitions;

namespace ApNew.Nodes
{
    public sealed class StateSetBuilder
    {
        public IDictionary<string, IState> Nodes { get; } = new Dictionary<string, IState>();

        public LinkedList<IState> LinkedList { get; } = new LinkedList<IState>();

        private Action<string> _addTransition = destination => { };

        public StateSetBuilder(string state)
        {
            Start(state);
        }

        private void Start(string state)
        {
            if (!Nodes.TryGetValue(state, out IState? result))
            {
                result = new StateBase(state);

                _addTransition = destination =>
                {
                    result.AddTransition(new Approve(TransitionConst.Approve, destination));
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
            _addTransition(state);
            _addTransition = destination =>
            {
                result.AddTransition(new Approve(TransitionConst.Approve, destination));
                result.AddTransition(new Reject(TransitionConst.Reject, destination));
            };
            return this;
        }

        public StateSetBuilder Branch(string state)
        {
            if (!Nodes.TryGetValue(state, out IState? result))
            {
                result = new StateBase(state);
                Nodes.Add(state, result);
            }
            LinkedList.AddLast(result);
            _addTransition(state);
            return this;
        }
    }
}
