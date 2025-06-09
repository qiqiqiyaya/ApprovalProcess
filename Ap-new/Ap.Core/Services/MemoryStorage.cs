using Ap.Core.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class MemoryStorage
    {
        private readonly Dictionary<string, IStateSet> _map = new Dictionary<string, IStateSet>();

        public MemoryStorage() { }

        public ValueTask<IStateSet> GetAsync(string id)
        {
            return new ValueTask<IStateSet>(_map[id]);
        }

        public ValueTask AddAsync(IStateSet set)
        {
            _map[set.Name] = set;
            return new ValueTask();
        }
    }
}
