using ApNew.Nodes.Builders;

namespace ApNew.Nodes.Core
{
    public class StateMachine(IState state, StateLinkedList rootLinkedList, string id)
        : StateSetBase(state, rootLinkedList, id);
}
