using ApNew.Nodes.Behaviours;

namespace ApNew.Nodes.Core
{
    public class StateSetContainer : StateBase, IStateSetContainer
    {
        private readonly StateSetBase _parent;

        public StateSetContainer(string state, StateSetBase parent) : base(state)
        {
            _parent = parent;

            Id = Guid.NewGuid().ToString("N");
        }

        public virtual void ExecuteTrigger(TriggerParameter trigger)
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

        public virtual void ExecuteTrigger(string trigger)
        {

        }

        public virtual IDictionary<string, IStateSet> StateSets { get; } = new Dictionary<string, IStateSet>();

        public virtual bool IsEnd => CheckIsEnding();

        public virtual bool IsConfigured(string state)
        {
            return StateSets.Any(x => x.Value.Nodes.ContainsKey(state));
        }

        protected virtual bool CheckIsEnding()
        {
            return StateSets.Values.All(s => s.IsEnd);
        }

        public override TriggerDictionary GetTrigger()
        {
            var dic = new TriggerDictionary();

            foreach (var stateSet in StateSets)
            {
                var set = stateSet.Value;
                if (!set.IsEnd)
                {
                    var result = set.GetTrigger();
                    foreach (var item in result)
                    {
                        item.Value.StateSetId = set.Id;
                        dic.Add(item.Key, item.Value);
                    }
                }
            }

            return dic;
        }
    }
}
