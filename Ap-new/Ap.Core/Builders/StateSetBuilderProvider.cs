using Ap.Core.Definitions;
using System;

namespace Ap.Core.Builders
{
	public class StateSetBuilderProvider(IServiceProvider serviceProvider, StateLinkedList? rootStateLinked = null) : IStateSetBuilderProvider
	{
		public IServiceProvider ServiceProvider { get; } = serviceProvider;

		public virtual IStateSetBuilder Create(string state)
		{
			var builder = new StateSetBuilder(state, rootStateLinked);
			builder.Initial(ServiceProvider);
			return builder;
		}

		public virtual IStateSetBuilder Create(string state, Action<IState, string> action)
		{
			var builder = new StateSetBuilder(state, rootStateLinked, action);
			builder.Initial(ServiceProvider);
			return builder;
		}

		public virtual IStateSetBuilder Create(string state, string id)
		{
			var builder = new StateSetBuilder(state, id, rootStateLinked);
			builder.Initial(ServiceProvider);
			return builder;
		}

		public virtual IStateSetBuilder Create(string state, string id, Action<IState, string> action)
		{
			var builder = new StateSetBuilder(state, id, rootStateLinked, action);
			builder.Initial(ServiceProvider);
			return builder;
		}

		public virtual TBuilder Create<TBuilder>(Func<IServiceProvider, StateLinkedList?, TBuilder> createAction)
				where TBuilder : class
		{
			return createAction(ServiceProvider, rootStateLinked);
		}
	}
}
