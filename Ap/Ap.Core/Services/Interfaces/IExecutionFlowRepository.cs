using Ap.Core.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IExecutionFlowRepository
    {
        ValueTask CreateAsync(Flow flow);
        ValueTask UpdateAsync(Flow flow);

        ValueTask<Flow> GetAsync(string id);
    }
}
