using Ap.Share.Models;
using Ap.Share.Services;
using Sm.Core.Actions.Entry;
using System.Threading.Tasks;

namespace Ap.Share.Actions.Entry
{

    public static class EntryActionContextExtensions
    {
        public const string FirerInfoKey = nameof(FirerInfoKey);

        public static async ValueTask<Employee> GetEmployeeCacheAsync<TState, TTrigger>(
            this EntryActionContext<TState, TTrigger> context,
            string employeeId,
            string cacheKey)
        {
            var value = context.GetObject<Employee>(cacheKey);
            if (value == null)
            {
                value = await context.GetEmployeeAsync(employeeId);
                context.AddObject(cacheKey, value);
            }

            return value;
        }

        public static async ValueTask<Employee> GetEmployeeAsync<TState, TTrigger>(this EntryActionContext<TState, TTrigger> context,
            string id)
        {
            var service = context.LazyGetRequiredService<IEmployeeService>();
            return await service.GetAsync(id);
        }
    }
}
