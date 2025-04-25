using Microsoft.EntityFrameworkCore;
using Test.Repository;
using Test.Share.Entities;

namespace Test.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly TestDbContext _dbContext;

        public EmployeeService(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<EmployeeEntity?> GetAsync(int employeeId)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
        }
    }
}
