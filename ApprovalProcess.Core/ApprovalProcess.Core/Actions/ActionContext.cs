using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ApprovalProcess.Core.Actions
{
	public class ActionContext(IServiceProvider serviceProvider)
	{
		protected readonly IServiceProvider ServiceProvider = serviceProvider;

		public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

		public T GetRequiredService<T>()
		{
			return ServiceProvider.GetRequiredService<T>();
		}
	}
}
