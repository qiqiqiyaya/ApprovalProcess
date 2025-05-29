using Ap.Flow.State;

namespace Ap.Flow
{
    public interface IStateContainer
    {
        string Id { get; }

        IDictionary<string, IState> StateConfiguration { get; }
    }
}
