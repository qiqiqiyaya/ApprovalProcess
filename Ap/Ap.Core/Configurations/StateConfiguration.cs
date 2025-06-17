using Ap.Core.Definitions.Actions;
using System.Collections.Generic;
using Ap.Core.Actions;

namespace Ap.Core.Configurations
{
    public class StateConfiguration
    {
        public List<ApAction> EntryTypes { get; } = new();

        public List<ApAction> ExitTypes { get; } = new();

        /// <summary>
        /// Obtain services from subordinate approvers
        /// <see cref="Actions.AssignApprover"/>
        /// </summary>
        public ApAction? AssignApprover { get; internal set; }
    }
}
