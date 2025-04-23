using System;
using System.Threading.Tasks;

namespace ApprovalProcess.Core.Actions
{
	public abstract class SmAction : ISmAction
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public abstract ValueTask InvokeAsync(ActionContext context, Func<ActionContext, ValueTask> next);
	}
}
