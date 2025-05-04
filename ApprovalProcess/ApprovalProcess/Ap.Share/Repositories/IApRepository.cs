using Ap.Share.Models;
using System.Threading.Tasks;

namespace Ap.Share.Repositories
{
    public interface IApRepository
    {
        ValueTask<Employee> GetEmployeeAsync(string id);
    }
}
