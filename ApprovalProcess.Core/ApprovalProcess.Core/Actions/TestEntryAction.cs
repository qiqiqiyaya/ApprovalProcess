using System;
using System.Threading.Tasks;

namespace ApprovalProcess.Core.Actions
{
	public class TestEntryAction : IEntryAction<string, string>
	{
		public string Id { get; set; }

		public string Name { get; }

		public ValueTask InvokeAsync(EntryActionContext<string, string> context, Func<EntryActionContext<string, string>, ValueTask> next)
		{
			return next(context);
		}
	}
}
