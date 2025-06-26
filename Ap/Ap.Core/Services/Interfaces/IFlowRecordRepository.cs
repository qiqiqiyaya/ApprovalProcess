using Ap.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IFlowRecordRepository
    {
        ValueTask<NodeRecord> InsertAsync(NodeRecord record);

        ValueTask<List<NodeRecord>> GetListAsync(string flowId);
    }
}
