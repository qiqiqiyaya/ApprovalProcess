using Ap.Core.Actions;
using Ap.Core.Configurations;
using Ap.Core.Definitions.Actions;
using Ap.Core.Pipeline;
using System;
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
        List<ApAction> commons = [.. StateSetConfiguration.CommonEntryTypes];
        commons.Insert(0, new ApAction(typeof(ExceptionHandler)));

        var assignApprover = stateConfiguration.AssignApprover ?? StateSetConfiguration.AssignApprover;
        assignApprover ??= new ApAction(typeof(DefaultAssignApprover));
        actions.Add(assignApprover);
        actions.InsertRange(0, commons);

        Properties.Remove(EntryActionsProperty);
        Properties.Add(EntryActionsProperty, actions);
        await ActionRunAsync(actions);
    }

    internal async ValueTask ContainerActionRunAsync(StateConfiguration stateConfiguration)
    {
        List<ApAction> actions = [.. stateConfiguration.EntryTypes];
        actions.Insert(0, new ApAction(typeof(ExceptionHandler)));
        await ActionRunAsync(actions);
    }

    public async ValueTask ActionRunAsync(List<ApAction> actions)
    {
        if (actions.Count == 0) return;

        var pipeline = GetRequiredService<IPipelineProvider>().GetPipeline<EntryContext>(actions);
        await pipeline.RunAsync(this);
    }
}
