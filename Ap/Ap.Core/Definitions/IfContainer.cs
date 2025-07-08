using Ap.Core.Behaviours;
using Ap.Core.Definitions.Actions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
    public class IfContainer : StateSetContainerBase
    {
        private readonly ApAction? _predicate;
        private bool _isEnd;

        public const string TrueState = "@true";
        public const string FalseState = "@false";
        public const string IfContainerName = "If";

        public IfContainer(string name, StateSetBase parent,
            ApAction predicate, IStateSet @true, IStateSet @false) : base(IfContainerName + "_" + name, parent)
        {
            _predicate = predicate;

            StateSets.Add(TrueState, @true ?? throw new ArgumentNullException(nameof(@true)));
            StateSets.Add(FalseState, @false ?? throw new ArgumentNullException(nameof(@false)));
        }

        protected async ValueTask<IStateSet> GetCurrentStateSet()
        {
            var trueSet = StateSets[TrueState];
            var falseSet = StateSets[FalseState];

            IStateSet set = null!;
            if (trueSet.IsInitial && falseSet.IsInitial)
            {
                if (_predicate != null)
                {
                    var func = (IfFunction)ActivatorUtilities.CreateInstance(ServiceProvider, _predicate.Type, _predicate.Parameters);
                    set = await func.InvokeAsync(new PredicateContext(ServiceProvider)) ? trueSet : falseSet;
                }
            }
            else
            {
                set = StateSets.First(s => !s.Value.IsInitial).Value;
            }

            return set;
        }

        public override async ValueTask ExecuteTrigger(TriggerContext context)
        {
            // The current StateSet is only known during execution
            IStateSet set = CurrentStateSet ??= await GetCurrentStateSet();
            set.ServiceProvider ??= ServiceProvider;
            context.Properties[StateSetContainerIdProperty] = Id;

            if (set.IsInitial)
            {
                // to create CurrentStateSet's flow
                await set.Entry(context.CreateEntryContext());
            }

            await set.ExecuteTrigger(context);
            if (set.IsEnd)
            {
                await set.Exit(context.CreateExitContext());
            }

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

        public override async ValueTask<StateTriggerCollection> GetTrigger()
        {
            // At the initial entry ， CurrentStateSet is null
            CurrentStateSet ??= await GetCurrentStateSet();
            return await CurrentStateSet.GetTrigger();
        }
    }
}
