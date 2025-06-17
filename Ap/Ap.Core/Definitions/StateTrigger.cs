// ReSharper disable UnusedMember.Global

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace Ap.Core.Definitions
{
    public class StateTrigger
    {
        internal StateTrigger(string trigger, StateDetail stateDetail)
        {
            Trigger = trigger;
            StateDetail = stateDetail;
        }

        public string? StateSetId { get; set; }

        public string Trigger { get; set; }

        public StateDetail StateDetail { get; set; }
    }
}
