using ApNew.Nodes.Builders;

namespace ApNew.Nodes.Core
{
    /// <summary>
    /// State set
    /// </summary>
    public interface IStateSet : IState, IStateTrigger
    {
        /// <summary>
        /// Initial state
        /// </summary>
        string InitialState { get; }

        string CurrentState { get; }

        IState CurrentStateNode { get; }

        /// <summary>
        /// Dictionary
        /// </summary>
        IDictionary<string, IState> Nodes { get; }

        StateLinkedList LinkedList { get; }

        StateLinkedList RootLinkedList { get; }

        bool IsEnd { get; }

        void Configure(IState state);

        /// <summary>
        /// reset ot initial state
        /// </summary>
        void Reset();
    }
}
