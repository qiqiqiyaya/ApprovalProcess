namespace Ap.Core.Definitions
{
    public class StateMachine(IState state, StateLinkedList rootLinkedList, string id)
        : StateSetBase(state, rootLinkedList, id);
}
