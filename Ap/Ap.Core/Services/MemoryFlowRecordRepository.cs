using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class MemoryFlowRecordRepository : IFlowRecordRepository
    {
        private static readonly Dictionary<string, List<NodeRecord>> Flows = new();

        public ValueTask<NodeRecord> InsertAsync(NodeRecord record)
        {
            if (!Flows.ContainsKey(record.FlowId))
            {
                Flows[record.FlowId] = new List<NodeRecord>();
            }

            Flows[record.FlowId].Add(record);
            return new ValueTask<NodeRecord>(record);
        }

        public ValueTask<List<NodeRecord>> GetListAsync(string flowId)
        {
            if (Flows.TryGetValue(flowId, out var records))
            {
                return new ValueTask<List<NodeRecord>>(records);
            }
            return new ValueTask<List<NodeRecord>>(new List<NodeRecord>());
        }
    }
}
