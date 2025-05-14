using System.Threading.Tasks;

namespace Sm.Core.Actions.Pipeline
{
    public interface IPipeline<in TContext>
    {
        ValueTask RunAsync(TContext context);
    }
}
