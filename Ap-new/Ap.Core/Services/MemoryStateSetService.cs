using Ap.Core.Definitions;
using Ap.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class MemoryStateSetService : IStateSetService
    {
        private static readonly List<IStateSet> Configurations = new();

        public MemoryStateSetService() { }

        public ValueTask Add(IStateSet configuration)
        {
            Configurations.Add(configuration);
            return new ValueTask();
        }

        public ValueTask<IStateSet> GetByIdAsync(string stateSetId)
        {
            var set = Configurations.FirstOrDefault(x => x.Id == stateSetId);
            return new ValueTask<IStateSet>(set);
        }

        public ValueTask<IStateSet> GetByNameAsync(string name)
        {
            var set = Configurations.FirstOrDefault(x => x.Name == name);
            return new ValueTask<IStateSet>(set);
        }
    }
}
