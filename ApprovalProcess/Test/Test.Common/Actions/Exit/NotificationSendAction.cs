using System;
using System.Threading.Tasks;
using Ap.Core.Actions.Exit;

namespace Test.Common.Actions.Exit
{
    public class NotificationSendAction : IExitAction
    {
        public string Id { get; set; }

        public string Name { get; }

        public ValueTask InvokeAsync(ExitActionContext context, Func<ExitActionContext, ValueTask> next)
        {
            return next(context);
        }
    }
}
