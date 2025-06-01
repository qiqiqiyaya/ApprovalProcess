using ApNew.Nodes.Behaviours;

namespace ApNew.Nodes.Core
{
    public class IfContainer : StateBase, IStateSetContainer
    {
        private readonly StateSetBase _parent;
        private readonly Func<bool> _action;

        public const string TrueState = "@true";
        public const string FalseState = "@false";

        public IfContainer(string state, StateSetBase parent,
            Func<bool> action, IStateSet @true, IStateSet @false) : base(state)
        {
            _parent = parent;
            _action = action ?? throw new ArgumentNullException(nameof(action));

            StateSets.Add(TrueState, @true ?? throw new ArgumentNullException(nameof(@true)));
            StateSets.Add(FalseState, @false ?? throw new ArgumentNullException(nameof(@false)));
        }

        public void ExecuteTrigger(TriggerParameter trigger)
        {
            IStateSet set = GetStateSet();
            set.ExecuteTrigger(trigger);

            if (IsEnd)
            {
                // Go directly to the next state
                _parent.ExecuteTrigger(new TriggerParameter() { StateSetId = _parent.Id, Trigger = TransitionConst.Direct });
                set.Reset();
            }
        }

        public void ExecuteTrigger(string trigger)
        {

        }

        private IStateSet GetStateSet()
        {
            var trueSet = StateSets[TrueState];
            var falseSet = StateSets[FalseState];

            IStateSet set;
            if (trueSet.IsInitial && falseSet.IsInitial)
            {
                set = _action() ? trueSet : falseSet;
            }
            else
            {
                set = StateSets.First(s => !s.Value.IsInitial).Value;
            }

            return set;
        }

        public IDictionary<string, IStateSet> StateSets { get; } = new Dictionary<string, IStateSet>();

        public bool IsEnd => StateSets.Values.Any(s => s.IsEnd);

        public bool IsConfigured(string state)
        {
            return StateSets.Any(x => x.Value.Nodes.ContainsKey(state));
        }

        public override TriggerDictionary GetTrigger()
        {
            IStateSet set = GetStateSet();
            var list = set.LinkedList.FirstState.GetTrigger().Select(t =>
            {
                t.Value.StateSetId = set.Id;
                return t;
            }).ToList();

            return new TriggerDictionary(list);
        }
    }
}
