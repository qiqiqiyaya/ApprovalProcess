using ApNew.Nodes.Behaviours;

namespace ApNew.Nodes.Core
{
    public class BranchContainer : StateBase, IStateSetContainer
    {
        private readonly StateSetBase _parent;

        public BranchContainer(string name, LogicalRelationship relationship, StateSetBase parent) : base(name)
        {
            Relationship = relationship;
            _parent = parent;
        }

        public IDictionary<string, IStateSet> StateSets { get; } = new Dictionary<string, IStateSet>();

        public bool IsEnd => CheckIsEnding();

        public LogicalRelationship Relationship { get; set; }

        public void ExecuteTrigger(TriggerParameter trigger)
        {
            IStateTrigger set = StateSets[trigger.StateSetId];
            set.ExecuteTrigger(trigger);

            if (IsEnd)
            {
                // Go directly to the next state
                _parent.ExecuteTrigger(new TriggerParameter() { StateSetId = _parent.Id, Trigger = TransitionConst.Direct });
            }
        }

        public void ExecuteTrigger(string trigger)
        {
            ExecuteTrigger(new TriggerParameter() { StateSetId = _parent.Id, Trigger = trigger });
        }

        private bool CheckIsEnding()
        {
            return Relationship == LogicalRelationship.And ?
                // Ensure all sets are in end state
                StateSets.Values.All(s => s.IsEnd) : StateSets.Values.Any(s => s.IsEnd);
        }

        public bool IsConfigured(string state)
        {
            return StateSets.Any(x => x.Value.Nodes.ContainsKey(state));
        }
    }
}
