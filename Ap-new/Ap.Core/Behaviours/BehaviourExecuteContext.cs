using Ap.Core.Definitions;

namespace Ap.Core.Behaviours
{
    public class BehaviourExecuteContext(StateSetBase rootSet, StateSetBase currentSet)
    {
        public StateSetBase RootSet { get; set; } = rootSet;

        public StateSetBase CurrentSet { get; set; } = currentSet;

    }
}
