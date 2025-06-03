using ApNew.Nodes.Core;

namespace ApNew.Nodes.Builders
{
    public interface IStateBuilder
    {
        StateLinkedList RootStateLinked { get; }

        bool IsConfigured(string state);
    }
}
