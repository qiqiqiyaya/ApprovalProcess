using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IExecutionService
    {
        ValueTask InvokeAsync(ExecutionParameter parameter);
    }
}
