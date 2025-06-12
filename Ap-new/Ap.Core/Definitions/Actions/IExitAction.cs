using Ap.Core.Pipeline;

namespace Ap.Core.Definitions;

public class ExitContext
{

}

public interface IExitAction : IPipe<ExitContext>
{
}
