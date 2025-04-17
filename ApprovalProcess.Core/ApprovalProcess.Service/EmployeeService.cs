using ApprovalProcess.Core;
using ApprovalProcess.Repository;
using ApprovalProcess.Share.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApprovalProcess.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApprovalProcessDbContext _dbContext;

        public EmployeeService(ApprovalProcessDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<EmployeeEntity?> GetAsync(int employeeId)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
        }
    }
}
