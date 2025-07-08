using Ap.Core.Definitions;
using Ap.Core.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IExecutionService
    {
        /// <summary>
        /// will to create flow for current user and flow
        /// </summary>
        /// <param name="user"></param>
        /// <param name="flowName"></param>
        /// <returns></returns>
        ValueTask<Flow> InvokeAsync(IUser user, string flowName);

        ValueTask InvokeAsync(IUser user, Flow flow, StateTrigger stateTrigger);

        ValueTask InvokeAsync(IUser user, Flow flow, StateTrigger stateTrigger, IStateSet set);

        ValueTask<StateTriggerCollection> GetTriggerAsync(Flow flow);
    }
}
