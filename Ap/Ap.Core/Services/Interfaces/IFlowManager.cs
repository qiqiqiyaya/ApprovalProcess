using Ap.Core.Definitions;
using Ap.Core.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IFlowManager
    {
        ValueTask<Node> GetFlowAsync(string flowId);

        ValueTask UpdateFlowAsync(Node node);

        ValueTask<UserFlow> CreateUserFlowAsync(IUser user, string rootStateSetId);

        ValueTask<UserFlow> CreateUserFlowAsync(IUser user, IStateSet stateSet);

        ValueTask AddRecordAsync(NodeRecord record);
    }
}
