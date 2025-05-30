using ApNew.Nodes.Core;

namespace ApNew.Nodes.Builders
{
    public interface IStateSetBuilder
    {
        IDictionary<string, IState> StateDictionary { get; }
    }
}
