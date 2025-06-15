using System.Collections.Generic;

namespace Ap.Core.Definitions
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

        bool IsInitial { get; }

        IState CurrentStateNode { get; }

        /// <summary>
        /// Dictionary
        /// </summary>
        Dictionary<string, IState> StateDictionary { get; }

        StateLinkedList LinkedList { get; }

        StateLinkedList RootLinkedList { get; }

        bool IsEnd { get; }

        void AddState(IState state);

        void Recover(string stateName);

        /// <summary>
        /// reset ot initial state
        /// </summary>
        void Reset();
    }
}
