using Ap.Core.Definitions;

namespace Ap.Core.Builders
{
    public interface IStateBuilder
    {
        StateLinkedList RootStateLinked { get; }

        bool IsConfigured(string state);
    }
}
