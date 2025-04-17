#nullable enable
using ApprovalProcess.Share.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalProcess.Core
{
    public interface IEmployeeService
    {
        ValueTask<EmployeeEntity?> GetAsync(int employeeId);
    }
}
