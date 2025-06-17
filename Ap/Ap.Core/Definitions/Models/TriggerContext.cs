using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Definitions;

public class TriggerContext : BaseContext
{
	internal TriggerContext(IServiceProvider serviceProvider,
		StateTrigger stateTrigger,
		Flow flow,
		IUser executor)
	{
		ServiceProvider = serviceProvider;
		StateTrigger = stateTrigger;
		Flow = flow;
		Executor = executor;
	}

	internal ExitContext CreateExitContext()
	{
		var context = new ExitContext();
		context.StateTrigger = StateTrigger;
		context.RootStateSet = RootStateSet;
		context.CurrentStateSet = CurrentStateSet;
		context.ServiceProvider = ServiceProvider;
		context.StateTrigger = StateTrigger;
		context.Properties = Properties;
		context.RootSetConfiguration = RootSetConfiguration;
		context.Flow = Flow;
		context.State = State;
		context.Executor = Executor;

		return context;
	}

	internal EntryContext CreateEntryContext()
	{
		var context = new EntryContext();
		context.StateTrigger = StateTrigger;
		context.RootStateSet = RootStateSet;
		context.CurrentStateSet = CurrentStateSet;
		context.ServiceProvider = ServiceProvider;
		context.StateTrigger = StateTrigger;
		context.Properties = Properties;
		context.RootSetConfiguration = RootSetConfiguration;
		context.Flow = Flow;
		context.State = State;
		context.Executor = Executor;

		return context;
	}
}
