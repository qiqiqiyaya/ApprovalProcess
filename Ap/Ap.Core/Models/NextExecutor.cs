using System;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public class NextExecutor
    {
        public string Id { get; set; }

        public string FlowId { get; set; }

        public string ObjectId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
