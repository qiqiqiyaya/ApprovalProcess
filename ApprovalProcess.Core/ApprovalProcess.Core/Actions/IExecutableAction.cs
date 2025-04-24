using ApprovalProcess.Core.Actions.Pipeline;

namespace ApprovalProcess.Core.Actions
{
	public interface IExecutableAction<TContext> : IPipe<TContext>
	{
		public string Id { get; set; }

		string Name { get; }
	}
}
