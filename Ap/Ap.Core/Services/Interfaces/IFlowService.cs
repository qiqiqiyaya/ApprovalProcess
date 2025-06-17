using Ap.Core.Definitions;
using Ap.Core.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IFlowService
    {
        ValueTask<Flow> GetAsync(string id);

        ValueTask<Flow> CreateAsync(IUser user, string rootStateSetId);

        ValueTask<Flow> CreateAsync(IUser user, IStateSet set);

        ValueTask UpdateAsync(ExecutionFlow flow);

        ValueTask AddRecordAsync(FlowRecord record);

        ValueTask<StateTriggerCollection> GetActionsAsync(string id);
    }
}
