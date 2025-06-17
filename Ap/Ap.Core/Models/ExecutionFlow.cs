using System.Collections.Generic;

namespace Ap.Core.Models
{
    public class ExecutionFlow : Flow
    {
        public new List<ExecutionNextApprover> Approvers { get; set; } = new();
    }
}
