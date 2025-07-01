using System.Threading.Tasks;

namespace Ap.Core.Definitions.States;

public class EndState(string builderId) : StateBase(EndStateName + builderId)
{
    private const string EndStateName = "End_";

    public override ValueTask Entry(EntryContext context)
    {
        return new ValueTask();
    }

    public override ValueTask Exit(ExitContext context)
    {
        return new ValueTask();
    }
}
