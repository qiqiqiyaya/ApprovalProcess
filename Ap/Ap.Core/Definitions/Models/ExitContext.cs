using Ap.Core.Actions;
using Ap.Core.Configurations;
using Ap.Core.Definitions.Actions;
using Ap.Core.Pipeline;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Definitions;

public class ExitContext : BaseContext
{
    internal ExitContext() { }

    public virtual async ValueTask ActionRunAsync(StateConfiguration stateConfiguration)
    {
        List<ApAction> actions = [.. stateConfiguration.ExitTypes];
        actions.InsertRange(0, RootSetConfiguration.CommonExitTypes);

        await ActionRunAsync(actions);
    }

    public async ValueTask ActionRunAsync(List<ApAction> actions)
    {
        if (actions.Count == 0) return;
        var provider = GetRequiredService<IPipelineProvider>();

        var pipeline = provider.GetPipeline<ExitContext>(actions);
        await pipeline.RunAsync(this);
    }
}
