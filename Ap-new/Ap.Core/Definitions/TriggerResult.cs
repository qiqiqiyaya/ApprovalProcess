namespace Ap.Core.Definitions
{
    public class TriggerResult(string trigger, string? stateSetId = null)
    {
        public string? StateSetId { get; set; } = stateSetId;

        public string Trigger { get; set; } = trigger;


        public StateTrigger ToParameter()
        {
            return new StateTrigger()
            {
                StateSetId = StateSetId!,
                Trigger = Trigger
            };
        }
    }
}
