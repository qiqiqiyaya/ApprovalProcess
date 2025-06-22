using Ap.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IFlowRecordRepository
    {
        ValueTask<FlowRecord> InsertAsync(FlowRecord record);

        ValueTask<List<FlowRecord>> GetListAsync(string flowId);
    }
}
