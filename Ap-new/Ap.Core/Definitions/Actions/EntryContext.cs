using Ap.Core.Definitions.Actions;
using Ap.Core.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Definitions;

public class EntryContext(IServiceProvider serviceProvider,
    StateSetBase rootSet,
    StateSetBase currentSet,
    StateTrigger stateTrigger)
{
    public IServiceProvider ServiceProvider { get; set; } = serviceProvider;

    public Dictionary<string, object> Properties { get; set; } = new();

    public StateSetBase RootSet { get; internal set; } = rootSet;

    public StateSetBase CurrentSet { get; internal set; } = currentSet;

    public StateTrigger StateTrigger { get; internal set; } = stateTrigger;

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
