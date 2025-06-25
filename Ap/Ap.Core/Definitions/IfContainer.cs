using Ap.Core.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Ap.Core.Definitions.Actions;

namespace Ap.Core.Definitions
{
    public class IfContainer : StateSetContainerBase
    {
        private readonly ApAction? _predicate;
        private bool _isEnd;

        public const string TrueState = "@true";
        public const string FalseState = "@false";

        public IfContainer(string name, StateSetBase parent,
            ApAction predicate, IStateSet @true, IStateSet @false) : base(name, parent)
        {
            _predicate = predicate;

            StateSets.Add(TrueState, @true ?? throw new ArgumentNullException(nameof(@true)));
            StateSets.Add(FalseState, @false ?? throw new ArgumentNullException(nameof(@false)));
        }

        public override ValueTask InitialEntry(TriggerContext context)
        {
            throw new NotImplementedException();
        }

        public override ValueTask CompletedExit(TriggerContext context)
        {
            throw new NotImplementedException();
        }

        public override async ValueTask ExecuteTrigger(TriggerContext context)
        {
            IStateSet set = await GetStateSet();
            set.ServiceProvider = ServiceProvider;
            await set.ExecuteTrigger(context);

            _isEnd = set.IsEnd;
            if (_isEnd)
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

        public override bool IsEnd => _isEnd;

        private async ValueTask<IStateSet> GetStateSet()
        {
            var trueSet = StateSets[TrueState];
            var falseSet = StateSets[FalseState];

            IStateSet set;
            if (trueSet.IsInitial && falseSet.IsInitial)
            {
                if (_predicate != null)
                {
                    var func = (IfFunction)ActivatorUtilities.CreateInstance(ServiceProvider, _predicate.Type, _predicate.Parameters);
                    set = await func.InvokeAsync(new PredicateContext(ServiceProvider)) ? trueSet : falseSet;
                    return set;
                }
            }
            else
            {
                set = StateSets.First(s => !s.Value.IsInitial).Value;
                return set;
            }

            throw new InvalidOperationException("Can't get right IStateSet");
        }

        public override async ValueTask<StateTriggerCollection> GetTrigger()
        {
            IStateSet set = await GetStateSet();
            return await set.GetTrigger();
        }
    }
}
