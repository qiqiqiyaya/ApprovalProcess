using System.Collections.Generic;

namespace ApprovalProcess.Core.Actions.Pipeline
{
	public interface IPipelineProvider
	{
		IPipeline<TContext> GetPipeline<TContext>(string pipeLineName);

		IPipeline<TContext> GetPipeline<TContext>(List<ExecutableActionMap> maps, string pipeLineName = "fdsfds");
	}
}
