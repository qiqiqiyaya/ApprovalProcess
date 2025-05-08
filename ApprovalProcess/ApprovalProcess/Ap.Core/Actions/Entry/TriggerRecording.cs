using Ap.Share.Actions;
using Ap.Share.Entities;
using Ap.Share.Repositories;
using Sm.Core.Actions.Entry;
using Sm.Share.Actions;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Actions.Entry
{
	[ActionName(ExecutableActionNames.OnEntryTriggerRecording)]
	public class TriggerRecording : IEntryAction<string, string>
	{
		public string Id { get; set; }
		public string Name { get; }

		public async ValueTask InvokeAsync(EntryActionContext<string, string> context, Func<EntryActionContext<string, string>, ValueTask> next)
		{
			var entity = new TriggeredRecordEntity()
			{
				Id = Guid.NewGuid().ToString("N"),
				StateMachineId = context.StateMachineId,
				SourceState = context.TransitionDescription.Source,
				DtState = context.TransitionDescription.Destination,
				Trigger = context.TransitionDescription.Trigger,
				CurrentState = context.CurrentState
			};

			await context.LazyGetRequiredService<IApRepository>().SaveTriggeredRecordAsync(entity);
		}
	}
}
