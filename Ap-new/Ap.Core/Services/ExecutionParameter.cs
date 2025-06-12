using Ap.Core.Definitions;
using Ap.Core.Services.Interfaces;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Services
{
    public class ExecutionParameter
    {
        public string FlowId { get; set; }

        public string StateSetId { get; set; }

        public string? ChildStateSetId { get; set; }

        public IUser User { get; set; }

        public StateTrigger StateTrigger { get; set; }
    }
}
