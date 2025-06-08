using Ap.Core;
using Ap.Core.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace ApTest
{
	public class Base
	{
		private readonly IServiceProvider _provider;

		public Base()
		{
			var service = new ServiceCollection();
			service.AddApCore();
			_provider = service.BuildServiceProvider();
		}

		protected IStateSetBuilderProvider StateSetBuilderProvider => GetService<IStateSetBuilderProvider>();

		protected T GetService<T>() where T : class
		{
			return _provider.GetRequiredService<T>();
		}
	}
}
