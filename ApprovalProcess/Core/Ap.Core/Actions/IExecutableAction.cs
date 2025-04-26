using Ap.Core.Actions.Pipeline;

namespace Ap.Core.Actions
{
    public interface IExecutableAction<TContext> : IPipe<TContext>
    {
        public string Id { get; set; }

        string Name { get; }
    }
}
