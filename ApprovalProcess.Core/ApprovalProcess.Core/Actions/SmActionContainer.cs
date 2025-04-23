using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ApprovalProcess.Core.Actions
{
	public class SmActionContainer
	{
		private readonly IDictionary<string, ActionConfiguration> _container;
		private readonly IServiceProvider _serviceProvider;

		public SmActionContainer(IDictionary<string, ActionConfiguration> container,
			IServiceProvider serviceProvider)
		{
			_container = container;
			_serviceProvider = serviceProvider;
		}

		public List<ISmAction> GetActions(params string[] names)
		{
			List<ISmAction> actions = new List<ISmAction>();
			foreach (var item in names)
			{
				var configuration = _container[item];
				configuration.SmAction ??= _serviceProvider.GetRequiredService(configuration.Type);
				actions.Add(configuration.SmAction as ISmAction);
			}

			return actions;
		}
	}
}
