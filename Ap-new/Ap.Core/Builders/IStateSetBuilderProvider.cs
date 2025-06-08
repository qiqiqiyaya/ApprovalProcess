using Ap.Core.Definitions;
using System;

namespace Ap.Core.Builders
{
	public delegate IStateSetBuilderProvider CreateStateSetBuilderProvider(IServiceProvider serviceProvider, StateLinkedList? rootStateLinked = null);

	public interface IStateSetBuilderProvider
	{
		IServiceProvider ServiceProvider { get; }

		IStateSetBuilder Create(string state);

		IStateSetBuilder Create(string state, Action<IState, string> action);

		IStateSetBuilder Create(string state, string id);

		IStateSetBuilder Create(string state, string id, Action<IState, string> action);

		TBuilder Create<TBuilder>(Func<IServiceProvider, StateLinkedList?, TBuilder> createAction)
		  where TBuilder : class;
	}
}
