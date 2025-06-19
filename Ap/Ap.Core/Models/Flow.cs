using Ap.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public class Flow
    {
        public string Id { get; set; }

        public string RootStateSetId { get; set; }

        public string CurrentStateSetId { get; set; }

        public string StateName { get; set; }

        public string StateId { get; set; }

        public string? StateTrigger { get; set; }

        /// <summary>
        /// The ID of the executor who triggered the flow. see <see cref="IUser.Id"/> for more details.
        /// </summary>
        public string ExecutorId { get; set; }

        public DateTime CreateTime { get; set; }

        public List<NextExecutor> NextExecutors { get; set; } = new();
    }


}
