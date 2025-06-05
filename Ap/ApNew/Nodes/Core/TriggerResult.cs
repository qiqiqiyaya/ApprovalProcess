namespace ApNew.Nodes.Core
{
    public class TriggerResult(string trigger, string? stateSetId = null)
    {
        public string? StateSetId { get; set; } = stateSetId;

        public string Trigger { get; set; } = trigger;


        public TriggerParameter ToParameter()
        {
            return new TriggerParameter()
            {
                StateSetId = StateSetId!,
                Trigger = Trigger
            };
        }
    }
}
