using Ap.Core.Definitions;
using Ap.Core.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IExecutionService
    {
        ValueTask InvokeAsync(IUser user, string flowId, StateTrigger stateTrigger);

        ValueTask InvokeAsync(IUser user, Flow flow, StateTrigger stateTrigger);

        ValueTask InvokeAsync(IUser user, Flow flow, StateTrigger stateTrigger, IStateSet set);
    }
}
