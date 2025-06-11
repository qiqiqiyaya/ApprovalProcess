using Ap.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ap.Core.Definitions;

namespace Ap.Core.Services
{
    public class MemoryStateSetService : IStateSetService
    {
        private static readonly List<StateSetConfig> Configurations = new();

        public MemoryStateSetService() { }

        public ValueTask Add(StateSetConfig config)
        {
            Configurations.Add(config);
            return new ValueTask();
        }

        public async ValueTask<IStateSet> GetByIdAsync(string stateSetId)
        {
            var config = await GetConfig(stateSetId);
            return config.StateSet;
        }

        public ValueTask<IStateSet> GetByNameAsync(string name)
        {
            var configuration = Configurations.FirstOrDefault(x => x.StateSet.Name == name);
            return new ValueTask<IStateSet>(configuration?.StateSet!);
        }

        public ValueTask<StateSetConfig> GetConfig(string stateSetId)
        {
            var configuration = Configurations.FirstOrDefault(x => x.StateSet.Id == stateSetId);
            return new ValueTask<StateSetConfig>(configuration);
        }
    }
}
