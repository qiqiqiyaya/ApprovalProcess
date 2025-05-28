using ApNew.Nodes.Core;

namespace ApNew.Nodes.Builders
{
    public interface IStateSetBuilder : IStateBuilder
    {
        IDictionary<string, IState> StateDictionary { get; }
    }
}
