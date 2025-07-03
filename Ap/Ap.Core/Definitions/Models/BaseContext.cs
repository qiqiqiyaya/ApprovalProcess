using Ap.Core.Configurations;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Definitions;

public abstract class BaseContext
{
    public IServiceProvider ServiceProvider { get; set; }

    public StateTrigger StateTrigger { get; internal set; }

    public Flow Flow { get; internal set; }

    public IUser Executor { get; set; }

    public IStateSet RootStateSet { get; internal set; }

    public IStateSet CurrentStateSet { get; internal set; }

    public Dictionary<string, object> Properties { get; set; } = new();

    public StateSetConfiguration StateSetConfiguration { get; internal set; }

    public IState State { get; internal set; }

    /// <summary>
    ///  The time when it is triggered
    /// </summary>
    public DateTime TriggeredTime { get; set; }

    public T GetRequiredService<T>() where T : notnull
    {
        return ServiceProvider.GetRequiredService<T>();
    }

    /// <summary>
    /// this will get the latest flow from the flow manager. Wil update the Flow property.
    /// </summary>
    /// <returns></returns>
    public async ValueTask<Flow> RefreshAsync()
    {
        var flowManager = GetRequiredService<IFlowManager>();
        Flow = await flowManager.GetFlowAsync(Flow.Id);
        return Flow;
    }

    public TriggerContext CreateTriggerContext()
    {
        return new TriggerContext(StateTrigger, Flow, Executor)
        {
            ServiceProvider = ServiceProvider,
            RootStateSet = RootStateSet,
            //CurrentStateSet = CurrentStateSet,
            //StateSetConfiguration = StateSetConfiguration,
            //State = State,
            //TriggeredTime = TriggeredTime
        };
    }
}
