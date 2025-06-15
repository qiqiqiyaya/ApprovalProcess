using System;

namespace Ap.Core.Services.Models
{
    public class NextApprover
    {
        public string Id { get; set; }

        public string FlowId { get; set; }

        public string ObjectId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
