using System.Collections.Generic;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Definitions;

public class TriggerContext : BaseContext
{
	internal TriggerContext(StateTrigger stateTrigger,
		Flow flow,
		IUser executor)
	{
		StateTrigger = stateTrigger;
		Flow = flow;
		Executor = executor;
	}

	internal TriggerContext(Flow flow,
		IUser executor)
	{
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

	public TriggerContext Clone()
	{
		return new TriggerContext(StateTrigger, Flow, Executor)
		{
			ServiceProvider = ServiceProvider,
			RootStateSet = RootStateSet,
			CurrentStateSet = CurrentStateSet,
			Properties = new Dictionary<string, object>(Properties),
			RootSetConfiguration = RootSetConfiguration,
			State = State
		};
	}
}
