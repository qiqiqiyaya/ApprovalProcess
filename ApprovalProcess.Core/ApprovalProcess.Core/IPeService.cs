using System.Threading.Tasks;
using ApprovalProcess.Share.Entities;

namespace ApprovalProcess.Core
{
    public interface IPeService
    {
        ValueTask<PeEntity> CreateAsync(int employeeId);
    }
}
