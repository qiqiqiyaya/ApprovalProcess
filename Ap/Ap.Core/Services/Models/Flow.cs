using System;
using System.Collections.Generic;

namespace Ap.Core.Services.Models
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
}
