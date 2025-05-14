using Sm.Core.Actions.Pipeline;

namespace Sm.Core.Actions
{
    public interface IExecutableAction<TContext> : IPipe<TContext>
    {
        public string Id { get; set; }

        string Name { get; }
    }
}
