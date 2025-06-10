using Ap.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class MemoryStateSetService : IStateSetService
    {
        private static readonly Dictionary<string, StateSetConfig> Configurations = new Dictionary<string, StateSetConfig>();

        public MemoryStateSetService() { }

        public ValueTask Add(StateSetConfig config)
        {
            Configurations.Add(config.StateSet.Id, config);
            return new ValueTask();
        }

        public ValueTask<StateSetConfig> GetByIdAsync(string stateSetId)
        {
            var configuration = Configurations[stateSetId];
            return new ValueTask<StateSetConfig>(configuration);
        }

        public ValueTask<StateSetConfig> GetByNameAsync(string name)
        {
            var configuration = Configurations.Values.First(x => x.StateSet.Name == name);
            return new ValueTask<StateSetConfig>(configuration);
        }
    }
}
