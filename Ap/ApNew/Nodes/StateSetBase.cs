namespace ApNew.Nodes
{
    public abstract class StateSetBase : NodeBase, IStateSet
    {
        public IDictionary<string, IState> Nodes { get; } = new Dictionary<string, IState>();

        public LinkedList<IState> LinkedList { get; } = new LinkedList<IState>();

        public void Configure(IState state)
        {
            if (Nodes.Keys.Contains(state.Name)) throw new InvalidOperationException($"State {state.Name} already exists in the state set.");

            Nodes.Add(state.Name, state);
        }

        public IState? GetState(string name)
        {
            if (Nodes.TryGetValue(name, out var state))
            {
                return state;
            }

            return null;
        }
    }
}
