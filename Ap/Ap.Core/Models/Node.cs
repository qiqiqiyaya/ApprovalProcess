using Ap.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public class Node
    {
        public string Id { get; set; }

        public string RootStateSetId { get; set; }

        public string StateSetId { get; set; }

        public string StateName { get; set; }

        public string StateId { get; set; }

        /// <summary>
        /// The last trigger that executed the flow.
        /// </summary>
        public string? LastExecTrigger { get; set; }

        /// <summary>
        /// The ID of the executor who triggered the flow. see <see cref="IUser.Id"/> for more details.
        /// </summary>
        public string ExecutorId { get; set; }

        public FlowStatus FlowStatus { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// List of next executors for the flow. These are the users who will be responsible for the next steps in the flow.
        /// </summary>
        public List<NextExecutor> NextExecutors { get; set; } = new();
    }

    public enum FlowStatus
    {
        Initial = 1,
        Running = 2,
        Completed = 3,
    }

}
