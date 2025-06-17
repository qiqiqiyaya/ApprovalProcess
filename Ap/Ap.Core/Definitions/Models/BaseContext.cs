using Ap.Core.Configurations;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Definitions;

public abstract class BaseContext
{
    public IServiceProvider ServiceProvider { get; set; }

    public StateTrigger StateTrigger { get; internal set; }

    public Flow Flow { get; internal set; }

    public IUser Executor { get; set; }

    public StateSetBase RootStateSet { get; internal set; }

    public StateSetBase CurrentStateSet { get; internal set; }

    public Dictionary<string, object> Properties { get; set; } = new();

    public StateSetConfiguration RootSetConfiguration { get; internal set; }

    public IState State { get; internal set; }

    public T GetRequiredService<T>() where T : notnull
    {
        return ServiceProvider.GetRequiredService<T>();
    }
}
