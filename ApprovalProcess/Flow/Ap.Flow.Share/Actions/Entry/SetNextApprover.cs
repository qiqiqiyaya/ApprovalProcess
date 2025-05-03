using Ap.Core.Actions;
using Ap.Core.Actions.Entry;
using Ap.Core.Share.Actions;
using Ap.Flow.Share.Actions.Models;
using System;
using System.Threading.Tasks;

namespace Ap.Flow.Share.Actions.Entry
{
	[ActionName(ExecutableActionNames.OnEntrySetNextApprover)]
	public class SetNextApprover : IEntryAction<string, string>
	{
		public string Id { get; set; }

		public string Name { get; }

		public ValueTask InvokeAsync(EntryActionContext<string, string> context, Func<EntryActionContext<string, string>, ValueTask> next)
		{
			var parameter = context.Propertry<NextApproverConfiguration>(PropertiesNames.NextApproverParameter);
			if (parameter == null)
			{
				return next(context);
			}

			if (parameter.Rule == ApprovalRule.ApprovedByOrg)
			{

			}

			return next(context);
		}
	}
}
