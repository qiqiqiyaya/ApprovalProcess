using Ap.Core.Definitions.Actions;
using Ap.Core.Services;
using System.Collections.Generic;

namespace Ap.Core.Configurations
{
    public class StateConfiguration
    {
        public List<ApAction> EntryTypes { get; } = new();

        public List<ApAction> ExitTypes { get; } = new();

        /// <summary>
        /// Obtain services from subordinate approvers
        /// <see cref="AssignApproverService"/>
        /// </summary>
        public ApAction? AssignApprover { get; internal set; }
    }
}
