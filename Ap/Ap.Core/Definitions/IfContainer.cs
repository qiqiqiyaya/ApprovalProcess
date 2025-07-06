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
		private IStateSet? _selectedStateSet = null;

		public IfContainer(string name, StateSetBase parent,
			ApAction predicate, IStateSet @true, IStateSet @false) : base(IfContainerName + "_" + name, parent)
		{
			_predicate = predicate;

			StateSets.Add(TrueState, @true ?? throw new ArgumentNullException(nameof(@true)));
			StateSets.Add(FalseState, @false ?? throw new ArgumentNullException(nameof(@false)));
		}

		public override async ValueTask ExecuteTrigger(TriggerContext context)
		{
			IStateSet set = await GetStateSet();
			set.ServiceProvider ??= ServiceProvider;

			await set.ExecuteTrigger(context);
			if (set.IsEnd) await set.CompletedExit(context);

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
			if (_selectedStateSet != null) return _selectedStateSet;

			var trueSet = StateSets[TrueState];
			var falseSet = StateSets[FalseState];

			IStateSet set = null!;
			if (trueSet.IsInitial && falseSet.IsInitial)
			{
				if (_predicate != null)
				{
					var func = (IfFunction)ActivatorUtilities.CreateInstance(ServiceProvider, _predicate.Type, _predicate.Parameters);
					set = await func.InvokeAsync(new PredicateContext(ServiceProvider)) ? trueSet : falseSet;
					_selectedStateSet = set;
				}
			}
			else
			{
				set = StateSets.First(s => !s.Value.IsInitial).Value;
			}

			set.ServiceProvider = ServiceProvider;
			return set;
		}

		public override async ValueTask<StateTriggerCollection> GetTrigger()
		{
			IStateSet set = await GetStateSet();
			return await set.GetTrigger();
		}

		public override async ValueTask Entry(EntryContext context)
		{
			await base.Entry(context);

			IStateSet set = await GetStateSet();
			context.CurrentStateSet = set;
			context.State = set;
			await context.StateSetActionRunAsync(set.StateConfiguration);

			var triggerContext = context.CreateTriggerContext();
			if (set.IsInitial) await set.InitialEntry(triggerContext);
		}
	}
}
