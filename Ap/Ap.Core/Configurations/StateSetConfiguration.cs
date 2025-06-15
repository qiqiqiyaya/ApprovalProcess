using Ap.Core.Definitions.Actions;
using Ap.Core.Services;

namespace Ap.Core.Configurations
{
    public class StateSetConfiguration
    {
        /// <summary>
        /// Obtain services from subordinate approvers
        /// <see cref="AssignApproverService"/>
        /// </summary>
        public ApAction? AssignApprover { get; internal set; }
    }
}
