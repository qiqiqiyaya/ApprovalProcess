using System.Threading.Tasks;

namespace Ap.Core.Actions.Pipeline
{
    public interface IPipeline<in TContext>
    {
        ValueTask RunAsync(TContext context);
    }
}
