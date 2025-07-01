using Ap.Core.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IUserFlowRepository
    {
        ValueTask<UserFlow> CreateAsync(UserFlow userFlow);

        ValueTask UpdateAsync(UserFlow userFlow);

        ValueTask UpdateAsync(Flow flow);

        ValueTask<UserFlow> GetByIdAsync(string userId);

        ValueTask<Flow> GetFlowAsync(string flowId);

        ValueTask<UserFlow> GetByFlowIdAsync(string flowId);

        ValueTask AddNodeAsync(Node node);
    }
}
