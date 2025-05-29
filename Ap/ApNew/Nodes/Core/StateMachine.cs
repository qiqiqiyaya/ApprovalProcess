using ApNew.Nodes.Builders;

namespace ApNew.Nodes.Core
{
    public class StateMachine : StateSetBase
    {
        public StateMachine(IState state, StateLinkedList rootLinkedList)
            : base(state, rootLinkedList)
        {

        }
    }
}
