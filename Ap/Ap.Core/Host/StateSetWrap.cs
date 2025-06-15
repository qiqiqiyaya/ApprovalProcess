using Ap.Core.Definitions;

namespace Ap.Core.Host
{
    public class StateSetWrap(IStateSet stateSet)
    {
        public IStateSet StateSet { get; set; } = stateSet;

        public string? ConcurrentLock { get; set; }
    }
}
