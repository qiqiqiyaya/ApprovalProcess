using Ap.Core.Definitions;
using Ap.Core.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IFlowManager
    {
        ValueTask<Flow> GetFlowAsync(string flowId);

        ValueTask UpdateFlowAsync(Flow flow);

        ValueTask<UserFlow> CreateUserFlowAsync(IUser user, string rootStateSetId);

        ValueTask<UserFlow> CreateUserFlowAsync(IUser user, IStateSet stateSet);

        ValueTask<UserFlow> CreateUserFlowAsync(IUser user, Flow flow);

        ValueTask<UserFlow> GetUserFlow(string flowId);

        ValueTask AddNodeAsync(Node node);
    }
}
