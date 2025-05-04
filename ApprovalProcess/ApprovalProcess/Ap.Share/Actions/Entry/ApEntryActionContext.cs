using Ap.Share.Services;
using Sm.Core.Actions.Entry;
using System;
using System.Threading.Tasks;
using Ap.Share.Models;

namespace Ap.Share.Actions.Entry
{
    public class ApEntryActionContext<TState, TTrigger> : EntryActionContext<TState, TTrigger>
    {
        public ApEntryActionContext(IServiceProvider serviceProvider,
            TTrigger trigger,
            TState state,
            TState dtState)
            : base(serviceProvider, trigger, state, dtState)
        {

        }

        private string _firerInfoKey = nameof(_firerInfoKey);
        public async ValueTask<Employee> GetFirerInfo()
        {
            var value = GetObject<Employee>(_firerInfoKey);
            if (value == null)
            {
                value = await GetEmployeeAsync(Firer);

                AddObject(_firerInfoKey, value);
            }

            return value;
        }

        public async ValueTask<Employee> GetEmployeeAsync(string id)
        {
            var service = LazyGetRequiredService<IEmployeeService>();
            return await service.GetAsync(id);
        }
    }
}
