using Ap.Core.Actions;
using Ap.Core.Configurations;
using Ap.Core.Definitions.Actions;
using Ap.Core.Models;
using Ap.Core.Pipeline;
using Ap.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Definitions;

public class EntryContext : BaseContext
{
    public const string EntryActionsProperty = "EntryActions";
    internal EntryContext() { }

    public List<string> NextApproverList { get; } = new();

    public virtual async ValueTask StateActionRunAsync(StateConfiguration stateConfiguration)
    {
        List<ApAction> actions = [.. stateConfiguration.EntryTypes];
        List<ApAction> commons = [.. CommonConfiguration.CommonEntryTypes];
        commons.Insert(0, new ApAction(typeof(ExceptionHandler)));

        var assignApprover = stateConfiguration.AssignApprover ?? CommonConfiguration.AssignApprover;
        assignApprover ??= new ApAction(typeof(DefaultAssignApprover));
        actions.Add(assignApprover);
        actions.InsertRange(0, commons);

        Properties.Remove(EntryActionsProperty);
        Properties.Add(EntryActionsProperty, actions);
        await ActionRunAsync(actions);
    }

    public async ValueTask ContainerActionRunAsync(StateConfiguration stateConfiguration)
    {
        List<ApAction> actions = [.. stateConfiguration.EntryTypes];
        actions.Insert(0, new ApAction(typeof(ExceptionHandler)));
        await ActionRunAsync(actions);
    }

    public async ValueTask StateSetActionRunAsync(StateConfiguration stateConfiguration)
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

    public async ValueTask AddNode(Node node)
    {
        var flow = GetCurrentFlow();
        flow.Nodes.Add(node);
        await GetRequiredService<IFlowManager>().UpdateFlowAsync(RootFlow);
    }

    public Flow GetFlow(IStateSet set)
    {
        return GetFlow(RootFlow, set);
    }

    public Flow GetCurrentFlow()
    {
        return GetFlow(RootFlow, CurrentStateSet);
    }

    public Flow GetFlow(Flow flow, IStateSet set)
    {
        if (flow.StateSetId == set.Id) return flow;

        foreach (var node in flow.Nodes)
        {
            switch (node)
            {
                case FlowContainer flowContainer:
                    foreach (var item in flowContainer.Flows)
                    {
                        return GetFlow(item, set);
                    }
                    break;
            }
        }

        return flow;
    }
}
