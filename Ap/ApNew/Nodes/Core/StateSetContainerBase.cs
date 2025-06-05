namespace ApNew.Nodes.Core
{
    public class StateSetContainerBase : StateBase, IStateSetContainer
    {
        public StateSetContainerBase(string state) : base(state)
        {
        }

        public virtual void ExecuteTrigger(TriggerParameter trigger)
        {

        }

        public virtual void ExecuteTrigger(string trigger)
        {

        }

        public IDictionary<string, IStateSet> StateSets { get; } = new Dictionary<string, IStateSet>();

        public bool IsEnd { get; }

        public bool IsConfigured(string state)
        {
            throw new NotImplementedException();
        }
    }
}
