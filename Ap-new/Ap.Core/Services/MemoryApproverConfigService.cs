using Ap.Core.Configurations;
using Ap.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class MemoryApproverConfigService : IApproverConfigService
    {
        public Dictionary<string, ApproverConfiguration> Configurations = new Dictionary<string, ApproverConfiguration>();

        public MemoryApproverConfigService() { }

        public void Add(ApproverConfiguration configuration)
        {
            Configurations.Add(configuration.Name, configuration);
        }

        public ValueTask<ApproverConfiguration> GetByConfigNameAsync(string configName)
        {
            return new ValueTask<ApproverConfiguration>(Configurations[configName]);
        }

        public ValueTask<ApproverConfiguration> GetByStateSetIdAsync(string stateSetId)
        {
            var configuration = Configurations.Values.Single(x => x.StateSetId == stateSetId);
            return new ValueTask<ApproverConfiguration>(configuration);
        }
    }
}
