namespace ApNew.Nodes
{
    public interface IStateTrigger
    {
        void ExecuteTrigger(TriggerParameter trigger);

        void ExecuteTrigger(string trigger);
    }
}
