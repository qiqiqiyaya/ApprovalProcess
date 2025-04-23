using System;
using System.Threading.Tasks;

namespace ApprovalProcess.Core.Actions
{

	public interface ISmAction
	{
		string Name { get; }

		string Description { get; }

		abstract ValueTask InvokeAsync(ActionContext context, Func<ActionContext, ValueTask> next);

	}
}
