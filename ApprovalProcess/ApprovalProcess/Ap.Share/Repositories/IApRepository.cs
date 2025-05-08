using Ap.Share.Models;
using System.Threading.Tasks;
using Ap.Share.Entities;

namespace Ap.Share.Repositories
{
	public interface IApRepository
	{
		ValueTask<Employee> GetEmployeeAsync(string id);

		ValueTask SaveTriggeredRecordAsync(TriggeredRecordEntity entity);

		ValueTask<TriggeredRecordEntity?> GetLastTriggeredRecordAsync(string stateMachineId);
	}
}
