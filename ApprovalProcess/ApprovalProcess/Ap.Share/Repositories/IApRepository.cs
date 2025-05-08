using Ap.Share.Models;
using System.Threading.Tasks;
using Ap.Share.Entities;
using System.Collections.Generic;

namespace Ap.Share.Repositories
{
    public interface IApRepository
    {
        ValueTask<Employee> GetEmployeeAsync(string id);

        ValueTask<TriggeredRecordEntity> SaveTriggeredRecordAsync(TriggeredRecordEntity entity);

        ValueTask<TriggeredRecordEntity?> GetLastTriggeredRecordAsync(string stateMachineId);

        ValueTask SaveNextApproversAsync(IEnumerable<NextApproverEntity> entities);
    }
}
