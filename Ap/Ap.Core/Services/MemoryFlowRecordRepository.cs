﻿using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class MemoryFlowRecordRepository : IFlowRecordRepository
    {
        private static readonly Dictionary<string, List<FlowRecord>> Flows = new();

        public ValueTask<FlowRecord> InsertAsync(FlowRecord record)
        {
            if (!Flows.ContainsKey(record.FlowId))
            {
                Flows[record.FlowId] = new List<FlowRecord>();
            }

            Flows[record.FlowId].Add(record);
            return new ValueTask<FlowRecord>(record);
        }

        public ValueTask<List<FlowRecord>> GetListAsync(string flowId)
        {
            if (Flows.TryGetValue(flowId, out var records))
            {
                return new ValueTask<List<FlowRecord>>(records);
            }
            return new ValueTask<List<FlowRecord>>(new List<FlowRecord>());
        }
    }
}
