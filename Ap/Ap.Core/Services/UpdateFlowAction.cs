using Ap.Core.Exceptions;
using Ap.Core.Services.Interfaces;
using Ap.Core.Services.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Definitions;

public class UpdateFlowAction : IEntryAction
{
	public async ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
	{
		var flow = context.Flow;
		flow.StateName = context.State.Name;
		flow.UpdateTime = DateTime.UtcNow;

		await next(context);

		if (context.NextApproverList.Count == 0)
		{
			throw new ApException("No approvers assigned for the flow.");
		}

		flow.Approvers = context.NextApproverList.Select(s =>
		{
			var np = new NextApprover
			{
				Id = Guid.NewGuid().ToString("N"),
				ObjectId = s,
				FlowId = flow.Id,
				CreateTime = DateTime.UtcNow
			};
			return np;
		}).ToList();

		await context.GetRequiredService<IFlowService>().UpdateAsync(flow);
	}
}
