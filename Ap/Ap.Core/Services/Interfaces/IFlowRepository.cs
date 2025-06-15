using Ap.Core.Services.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IFlowRepository
    {
        ValueTask CreateAsync(Flow flow);

        ValueTask<Flow> GetAsync(string id);
    }
}
