using Ap.Core.Builders;
using Ap.Core.Definitions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ap.Core
{
	public static class ApCoreExtensions
	{
		public static void AddApCore(this IServiceCollection services)
		{
			CreateStateSetBuilderProvider creator =
				(IServiceProvider serviceProvider, StateLinkedList? rootStateLinked = null) => new StateSetBuilderProvider(serviceProvider, rootStateLinked);
			services.AddTransient(_ => creator);
			services.AddTransient<IStateSetBuilderProvider, StateSetBuilderProvider>();
		}
	}
}
