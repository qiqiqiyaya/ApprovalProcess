using Ap.Core.Actions;
using Ap.Core.Configurations;
using Ap.Core.Definitions.Actions;
using Ap.Core.Pipeline;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Definitions;

public class EntryContext : BaseContext
{
	public const string EntryActionsProperty = "EntryActions";
	internal EntryContext() { }

	public List<string> NextApproverList { get; } = new();

	public virtual async ValueTask ActionRunAsync(StateConfiguration stateConfiguration)
	{
		List<ApAction> actions = [.. stateConfiguration.EntryTypes];

		var assignApprover = stateConfiguration.AssignApprover ?? RootSetConfiguration.AssignApprover;
		assignApprover ??= new ApAction(typeof(DefaultAssignApprover));
		actions.Add(assignApprover);
		actions.InsertRange(0, RootSetConfiguration.CommonEntryTypes);

		Properties.Add(EntryActionsProperty, actions);
		await ActionRunAsync(actions);
	}

	public async ValueTask ActionRunAsync(List<ApAction> actions)
	{
		if (actions.Count == 0) return;

		var pipeline = GetRequiredService<IPipelineProvider>().GetPipeline<EntryContext>(actions);
		await pipeline.RunAsync(this);
	}
}
