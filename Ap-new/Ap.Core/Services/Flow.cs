using System;
using System.Collections.Generic;

namespace Ap.Core.Services
{
    public class Flow
    {
        public string Id { get; set; }

        public string StateSetId { get; set; }

        public string StateName { get; set; }

        public string UserId { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual List<NextApprover> Approvers { get; set; }
    }

    public class NextApprover
    {
        public string Id { get; set; }

        public string FlowId { get; set; }

        public string ObjectId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
