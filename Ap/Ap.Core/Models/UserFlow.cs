using Ap.Core.Services.Interfaces;
using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public class UserFlow
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FlowId { get; set; }

        public DateTime CreateTime { get; set; }

        public Flow ExecutionFlow { get; set; }

        public UserFlow(Flow flow, IUser user)
        {
            ExecutionFlow = flow ?? throw new ArgumentNullException(nameof(flow));
            UserId = user?.Id ?? throw new ArgumentNullException(nameof(user));
            FlowId = flow.Id;
            CreateTime = DateTime.Now;
        }
    }
}
