using Ap.Core.Services.Interfaces;
using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public abstract class NodeBase
    {
        public string Id { get; set; }

        public string RootStateSetId { get; set; }

        public string StateSetId { get; set; }

        public string StateName { get; set; }

        public string StateId { get; set; }

        public OutputTrigger? OutputTrigger { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// The ID of the executor who triggered the flow. see <see cref="IUser.Id"/> for more details.
        /// </summary>
        public string ExecutorId { get; set; }

        /// <summary>
        /// Whether triggered
        /// </summary>
        public bool IsTriggered { get; internal set; } = false;

        public string? ParentNodeId { get; set; }

        public override string ToString()
        {
            return $"StateName: {StateName}  StateId: {StateId}";
        }
    }
}
