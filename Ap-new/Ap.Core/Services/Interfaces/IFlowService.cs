using Ap.Core.Definitions;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IFlowService
    {
        ValueTask<Flow> GetAsync(string id);

        ValueTask<Flow> CreateAsync(IUser user, IStateSet set);
    }
}
