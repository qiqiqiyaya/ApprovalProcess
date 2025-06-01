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

            Id = Guid.NewGuid().ToString("N");
        }

        public IDictionary<string, IStateSet> StateSets { get; } = new Dictionary<string, IStateSet>();

        public bool IsEnd => CheckIsEnding();

        public LogicalRelationship Relationship { get; set; }

        public void ExecuteTrigger(TriggerParameter trigger)
        {
            if (string.IsNullOrEmpty(trigger.StateSetId)) throw new ArgumentException("StateSetId cannot be null or empty.", nameof(trigger.StateSetId));
            IStateTrigger set = StateSets[trigger.StateSetId];
            set.ExecuteTrigger(trigger);

            if (IsEnd)
            {
                // Go directly to the next state
                _parent.ExecuteTrigger(new TriggerParameter() { StateSetId = _parent.Id, Trigger = TransitionConst.Direct });
                foreach (var stateSet in StateSets)
                {
                    stateSet.Value.Reset();
                }
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

        public override TriggerDictionary GetTrigger()
        {
            TriggerDictionary dic = new TriggerDictionary();
            foreach (var stateSet in StateSets.Values)
            {
                var nodeTriggers = stateSet.LinkedList.FirstState.GetTrigger();
                foreach (var item in nodeTriggers)
                {
                    item.Value.StateSetId = stateSet.Id;
                    dic.Add(item.Key, item.Value);
                }
            }

            return dic;
        }
    }
}
