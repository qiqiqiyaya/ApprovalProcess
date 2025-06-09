using System;
using System.Collections.Generic;

namespace Ap.Core.Services
{
    public class HistoryFlow : Flow
    {
        public DateTime UpdateTime { get; set; }

        public override List<NextApprover> Approvers { get; set; }
    }

    public class HistoryNextApprover : NextApprover
    {
        public DateTime UpdateTime { get; set; }
    }
}
