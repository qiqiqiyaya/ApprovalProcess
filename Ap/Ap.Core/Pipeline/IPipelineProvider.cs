using Ap.Core.Definitions.Actions;
using System.Collections.Generic;

namespace Ap.Core.Pipeline
{
    public interface IPipelineProvider
    {
        IPipeline<TContext> GetPipeline<TContext>(List<ApAction> maps);
    }
}
