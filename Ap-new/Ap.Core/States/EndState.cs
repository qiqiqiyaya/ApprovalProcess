using Ap.Core.Definitions;

namespace Ap.Core.States;

public class EndState(string builderId) : StateBase(EndStateName + builderId)
{
    private const string EndStateName = "End_";
}
