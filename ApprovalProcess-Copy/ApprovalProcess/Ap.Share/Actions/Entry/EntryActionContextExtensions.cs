﻿using Ap.Share.Models;
using Ap.Share.Services;
using Sm.Core.Actions.Entry;
using System.Threading.Tasks;

namespace Ap.Share.Actions.Entry
{

    public static class EntryActionContextExtensions
    {
        public const string FirerInfoKey = nameof(FirerInfoKey);
        public const string TriggeredRecord = nameof(TriggeredRecord);

        public static async ValueTask<User> GetEmployeeCacheAsync<TState, TTrigger>(
            this EntryActionContext<TState, TTrigger> context,
            string employeeId,
            string cacheKey)
        {
            var value = context.GetObject<User>(cacheKey);
            if (value == null)
            {
                value = await context.GetEmployeeAsync(employeeId);
                context.AddObject(cacheKey, value);
            }

            return value;
        }

        public static async ValueTask<User> GetEmployeeAsync<TState, TTrigger>(this EntryActionContext<TState, TTrigger> context,
            string id)
        {
            var service = context.LazyGetRequiredService<IEmployeeService>();
            return await service.GetAsync(id);
        }
    }
}
