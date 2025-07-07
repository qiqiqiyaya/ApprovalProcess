using Ap.Core.Actions;
using Ap.Core.Configurations;
using Ap.Core.Definitions.Actions;
using Ap.Core.Pipeline;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ap.Core.Models;

namespace Ap.Core.Definitions;

public class ExitContext : BaseContext
{
    public const string ExitActionsProperty = "ExitActions";

    internal ExitContext() { }

    public virtual async ValueTask ActionRunAsync(StateConfiguration stateConfiguration)
    {
        List<ApAction> actions = [.. stateConfiguration.ExitTypes];
        List<ApAction> commons = [.. CommonConfiguration.CommonExitTypes];
        commons.Insert(0, new ApAction(typeof(ExceptionHandler)));

        actions.InsertRange(0, commons);

        Properties.Remove(ExitActionsProperty);
        Properties.Add(ExitActionsProperty, actions);
        await ActionRunAsync(actions);
    }

    public async ValueTask ActionRunAsync(List<ApAction> actions)
    {
        if (actions.Count == 0) return;

        var pipeline = GetRequiredService<IPipelineProvider>().GetPipeline<ExitContext>(actions);
        await pipeline.RunAsync(this);
    }

    public async ValueTask StateSetActionRunAsync(StateConfiguration stateConfiguration)
    {
        List<ApAction> actions = [.. stateConfiguration.ExitTypes];
        actions.Insert(0, new ApAction(typeof(ExceptionHandler)));
        await ActionRunAsync(actions);
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
