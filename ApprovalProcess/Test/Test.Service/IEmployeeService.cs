#nullable enable
using Test.Share.Entities;

namespace Test.Service
{
    public interface IEmployeeService
    {
        ValueTask<EmployeeEntity?> GetAsync(int employeeId);
    }
}
