using Ap.Core.Definitions;
using Ap.Core.Host.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Host
{
    public class ApMemoryCache : IApAcq, IApRegister
    {
        private readonly Dictionary<string, StateSetWrap> _stateSets = new Dictionary<string, StateSetWrap>();

        public ValueTask Register(IStateSet stateSet)
        {
            _stateSets.Add(stateSet.Id, new StateSetWrap(stateSet));
            return new ValueTask();
        }

        public StateSetWrap Get(string id)
        {
            if (_stateSets.TryGetValue(id, out var stateSet))
            {
                return stateSet;
            }

            throw new KeyNotFoundException($"StateSet with ID {id} not found.");
        }
    }

}
