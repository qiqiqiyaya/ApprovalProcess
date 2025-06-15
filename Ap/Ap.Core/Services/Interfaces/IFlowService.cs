using Ap.Core.Definitions;
using Ap.Core.Services.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IFlowService
    {
        ValueTask<Flow> GetAsync(string id);

        ValueTask<Flow> CreateAsync(IUser user, IStateSet set);

        ValueTask<StateTriggerCollection> GetActionsAsync(string id);
    }
}
