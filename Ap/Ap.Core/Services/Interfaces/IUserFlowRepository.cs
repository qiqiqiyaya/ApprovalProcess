using Ap.Core.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IUserFlowRepository
    {
        ValueTask<UserFlow> CreateAsync(UserFlow userFlow);

        ValueTask UpdateAsync(UserFlow userFlow);

        ValueTask UpdateAsync(Node node);

        ValueTask<UserFlow> GetByIdAsync(string userId);

        ValueTask<Node> GetFlowAsync(string flowId);
    }
}
