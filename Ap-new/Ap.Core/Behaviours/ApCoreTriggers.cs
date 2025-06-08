namespace Ap.Core.Behaviours
{
    public class ApCoreTriggers
    {
        /// <summary>
        /// Approved and moved to the next state
        /// </summary>
        public const string Approve = "Approve";

        /// <summary>
        /// Rejected and moved to the initial state or a specific state
        /// </summary>
        public const string Reject = "Reject";

        /// <summary>
        /// Approved and moved to the next state
        /// </summary>
        public const string Submit = "Submit";

        /// <summary>
        /// Completed the current state
        /// </summary>
        public const string Complete = "Complete";

        /// <summary>
        /// Directly transition to the destination state without waiting for the current state to complete.
        /// </summary>
        public const string Direct = "Direct";

        /// <summary>
        /// Can only jump in the current state set, cannot jump into sub levels
        /// </summary>
        public const string Jump = "Jump";
    }
}
