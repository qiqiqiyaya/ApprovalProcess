using Ap.Core.Definitions.Actions;
using System.Collections.Generic;

namespace Ap.Core.Configurations
{
    public class StateSetConfiguration
    {
        /// <summary>
        /// Obtain services from subordinate approvers
        /// <see cref="Actions.AssignApprover"/>
        /// </summary>
        public ApAction? AssignApprover { get; internal set; }

        public List<ApAction> CommonEntryTypes { get; set; } = new List<ApAction>();

        public List<ApAction> CommonExitTypes { get; set; } = new List<ApAction>();
    }
}
