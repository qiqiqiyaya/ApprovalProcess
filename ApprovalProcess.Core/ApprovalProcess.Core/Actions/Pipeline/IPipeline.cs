using System.Threading.Tasks;

namespace ApprovalProcess.Core.Actions.Pipeline
{
	public interface IPipeline<in TContext>
	{
		ValueTask ProcessAsync(TContext context);
	}
}
