namespace ApNew.Nodes
{
    /// <summary>
    /// State set
    /// </summary>
    public interface IStateSet : INode, IStateTrigger
    {
        /// <summary>
        /// Initial state
        /// </summary>
        string InitialState { get; }

        string CurrentState { get; }

        /// <summary>
        /// Dictionary
        /// </summary>
        IDictionary<string, IState> Nodes { get; }

        /// <summary>
        /// line
        /// </summary>
        LinkedList<IState> LinkedList { get; }

        bool IsEnd { get; }

        IState GetState(string state);

        void Configure(IState state);
    }
}
