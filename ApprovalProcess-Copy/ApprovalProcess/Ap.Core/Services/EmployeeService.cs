using Ap.Share.Models;
using Ap.Share.Repositories;
using Ap.Share.Services;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IApRepository _repository;

        public EmployeeService(IApRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<User> GetAsync(string id)
        {
            var employee = await _repository.GetEmployeeAsync(id);
            return employee;
        }
    }
}
