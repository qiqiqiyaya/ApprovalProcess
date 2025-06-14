using Ap.Core.Definitions.Actions;
using Ap.Core.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Definitions;

public class EntryContext : BaseContext
{
    internal EntryContext() { }

    public List<string> NextApproverList { get; set; } = new();

    public async ValueTask PipelineRunAsync(List<ApAction> actions)
    {
        if (actions.Count == 0) return;
        var provider = GetRequiredService<IPipelineProvider>();

        var pipeline = provider.GetPipeline<EntryContext>(actions);
        await pipeline.RunAsync(this);
    }

    protected T GetRequiredService<T>() where T : notnull
    {
        return ServiceProvider.GetRequiredService<T>();
    }
}
