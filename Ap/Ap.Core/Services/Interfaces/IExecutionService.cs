using Ap.Core.Definitions;
using Ap.Core.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IExecutionService
    {
        ValueTask InvokeAsync(IUser user, Node node, StateTrigger stateTrigger);

        ValueTask InvokeAsync(IUser user, Node node, StateTrigger stateTrigger, IStateSet set);

        ValueTask<StateTriggerCollection> GetTriggerAsync(Node node);
    }
}
