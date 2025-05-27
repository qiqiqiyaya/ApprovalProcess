using ApNew.States;

namespace ApNew.Nodes
{
    public class BranchContainer : StateBase, IStateSetContainer
    {
        private IStateSet _parent;

        public BranchContainer(string name, LogicalRelationship relationship, IStateSet parent) : base(name)
        {
            Relationship = relationship;
            _parent = parent;
        }

        public IDictionary<string, IStateSet> StateSets { get; } = new Dictionary<string, IStateSet>();

        public LogicalRelationship Relationship { get; set; }

        public void ExecuteTrigger(TriggerParameter trigger)
        {
            IStateTrigger set = StateSets[trigger.StateSetId];
            set.ExecuteTrigger(trigger);

            if (IsEnd())
            {
                // Go directly to the next state
                _parent.ExecuteTrigger(new TriggerParameter() { StateSetId = _parent.Id, Trigger = "Direct" });
            }
        }

        public void ExecuteTrigger(string trigger)
        {
            throw new NotImplementedException();
        }

        protected virtual bool IsEnd()
        {
            if (Relationship == LogicalRelationship.And)
            {
                //foreach (var item in StateSets)
                //{
                //    if (!item.Value.IsEnd) return false;
                //}
                //StateSets.Values.All(s => s.IsEnd); // Ensure all sets are in end state
                return StateSets.Values.All(s => s.IsEnd);// Ensure all sets are in end state
            }
            else if (Relationship == LogicalRelationship.Or)
            {
                //foreach (var item in StateSets)
                //{
                //    var set = item.Value;
                //    var state = set.GetState(set.CurrentState);
                //    if (state is EndState) return true;
                //}
                return StateSets.Values.Any(s => s.IsEnd);// Ensure all sets are in end state
            }

            return false;
        }
    }
}
