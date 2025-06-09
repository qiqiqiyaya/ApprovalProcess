using Ap.Core.Configurations;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IApproverConfigService
    {
        ValueTask<ApproverConfiguration> GetByConfigNameAsync(string configName);

        ValueTask<ApproverConfiguration> GetByStateSetIdAsync(string stateSetId);
    }
}
