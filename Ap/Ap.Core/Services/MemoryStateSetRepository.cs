using Ap.Core.Definitions;
using Ap.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class MemoryStateSetRepository : IStateSetRepository
    {
        private static readonly List<IStateSet> Configurations = new();

        public MemoryStateSetRepository() { }

        public ValueTask Add(IStateSet set)
        {
            if (Configurations.Any(x => x.Name == set.Name))
            {
                throw new DuplicateNameException(set.Name);
            }

            Configurations.Add(set);
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
