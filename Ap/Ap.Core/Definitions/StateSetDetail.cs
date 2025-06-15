using System.Collections.Generic;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Definitions
{
    internal class StateSetDetail
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string InitialState { get; set; }

        public string CurrentState { get; set; }

        public Dictionary<string, IState> StateConfiguration { get; set; }

        public StateLinkedList LinkedList { get; set; }

        public StateLinkedList RootLinkedList { get; set; }
    }
}
