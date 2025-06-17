using Ap.Core.Models;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IFlowRecordRepository
    {
        ValueTask<FlowRecord> InsertAsync(FlowRecord record);
    }
}
