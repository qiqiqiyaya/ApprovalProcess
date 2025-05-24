namespace ApNew.Nodes
{
    /// <summary>
    /// State set
    /// </summary>
    public interface IStateSet : INode
    {
        /// <summary>
        /// Dictionary
        /// </summary>
        IDictionary<string, IState> Nodes { get; }

        /// <summary>
        /// line
        /// </summary>
        LinkedList<IState> LinkedList { get; }

        void Configure(IState state);
    }
}
