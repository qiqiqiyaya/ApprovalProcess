using Ap.Core.Definitions;
using Ap.Core.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IFlowManager
    {
        ValueTask<Flow> CreateAsync(IUser user, string rootStateSetId);

        ValueTask<Flow> CreateAsync(IUser user, IStateSet stateSet);
    }
}
