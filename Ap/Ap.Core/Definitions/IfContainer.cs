using Ap.Core.Behaviours;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
    public class IfContainer : StateSetContainerBase
    {
        private readonly Func<bool> _action;

        public const string TrueState = "@true";
        public const string FalseState = "@false";

        public IfContainer(string name, StateSetBase parent,
            Func<bool> action, IStateSet @true, IStateSet @false) : base(name, parent)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));

            StateSets.Add(TrueState, @true ?? throw new ArgumentNullException(nameof(@true)));
            StateSets.Add(FalseState, @false ?? throw new ArgumentNullException(nameof(@false)));
        }

        public override async ValueTask ExecuteTrigger(TriggerContext context)
        {
            IStateSet set = GetStateSet();
            await set.ExecuteTrigger(context);

            if (IsEnd)
            {
                // Go directly to the next state
                var stateTrigger = new StateTrigger(ApCoreTriggers.Direct, ToDetail())
                {
                    StateSetId = Parent.Id
                };
                context.StateTrigger = stateTrigger;
                context.CurrentStateSet = Parent;

                await Parent.ExecuteTrigger(context);
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
