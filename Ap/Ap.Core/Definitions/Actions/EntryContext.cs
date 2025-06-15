using Ap.Core.Definitions.Actions;
using Ap.Core.Pipeline;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Definitions;

public class EntryContext : BaseContext
{
    internal EntryContext() { }

    public List<string> NextApproverList { get; } = new();

    public async ValueTask ActionRunAsync(List<ApAction> actions)
    {
        if (actions.Count == 0) return;
        var provider = GetRequiredService<IPipelineProvider>();

        var pipeline = provider.GetPipeline<EntryContext>(actions);
        await pipeline.RunAsync(this);
    }
}
