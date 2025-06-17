using Ap.Core.Services.Interfaces;
using Ap.Core.Services.Models;
using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Definitions;

public class TriggerContext : BaseContext
{
	internal TriggerContext(IServiceProvider serviceProvider,
		StateTrigger stateTrigger,
		Flow flow,
		IUser user)
	{
		ServiceProvider = serviceProvider;
		StateTrigger = stateTrigger;
		Flow = flow;
		User = user;
	}

	internal ExitContext CreateExitContext()
	{
		var context = new ExitContext();
		context.StateTrigger = StateTrigger;
		context.RootSet = RootSet;
		context.CurrentSet = CurrentSet;
		context.ServiceProvider = ServiceProvider;
		context.StateTrigger = StateTrigger;
		context.Properties = Properties;
		context.RootSetConfiguration = RootSetConfiguration;
		context.Flow = Flow;
		context.State = State;
		context.User = User;

		return context;
	}

	internal EntryContext CreateEntryContext()
	{
		var context = new EntryContext();
		context.StateTrigger = StateTrigger;
		context.RootSet = RootSet;
		context.CurrentSet = CurrentSet;
		context.ServiceProvider = ServiceProvider;
		context.StateTrigger = StateTrigger;
		context.Properties = Properties;
		context.RootSetConfiguration = RootSetConfiguration;
		context.Flow = Flow;
		context.State = State;
		context.User = User;

		return context;
	}
}
