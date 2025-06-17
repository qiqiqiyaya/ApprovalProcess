using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class MemoryExecutionFlowRepository : IExecutionFlowRepository
    {
        private static readonly Dictionary<string, Flow> Flows = new Dictionary<string, Flow>();

        public ValueTask CreateAsync(Flow flow)
        {
            Flows.Add(flow.Id, flow);
            return new ValueTask();
        }

        public ValueTask UpdateAsync(Flow flow)
        {
            Flows[flow.Id] = flow;
            return new ValueTask();
        }

        public ValueTask<Flow> GetAsync(string id)
        {
            return new ValueTask<Flow>(Flows[id]);
        }
    }
}
