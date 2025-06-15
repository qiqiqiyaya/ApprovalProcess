using System.Threading.Tasks;

namespace Ap.Core.Pipeline
{
    public interface IPipeline<in TContext>
    {
        ValueTask RunAsync(TContext context);
    }
}
