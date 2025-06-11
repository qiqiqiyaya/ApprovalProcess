using Ap.Core.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ap.Core.Definitions
{
    public class IfContainer : StateSetContainerBase
    {
        private readonly StateSetBase _parent;
        private readonly Func<bool> _action;

        public const string TrueState = "@true";
        public const string FalseState = "@false";

        public IfContainer(string name, StateSetBase parent,
            Func<bool> action, IStateSet @true, IStateSet @false) : base(name, parent)
        {
            _parent = parent;
            _action = action ?? throw new ArgumentNullException(nameof(action));

            StateSets.Add(TrueState, @true ?? throw new ArgumentNullException(nameof(@true)));
            StateSets.Add(FalseState, @false ?? throw new ArgumentNullException(nameof(@false)));
        }

        public override void ExecuteTrigger(StateTrigger trigger)
        {
            IStateSet set = GetStateSet();
            set.ExecuteTrigger(trigger);

            if (IsEnd)
            {
                // Go directly to the next state
                _parent.ExecuteTrigger(new StateTrigger() { StateSetId = _parent.Id, Trigger = ApCoreTriggers.Direct });
                set.Reset();
            }
        }

        public override bool IsEnd => GetStateSet().IsEnd;

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

        public override StateTriggerCollection GetTrigger()
        {
            IStateSet set = GetStateSet();
            return set.GetTrigger();
        }
    }
}
