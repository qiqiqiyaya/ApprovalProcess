using System;
using System.Threading.Tasks;

namespace Ap.Core.Definitions;

public class GeneralEntryAction(Func<EntryContext, ValueTask> entryAction) : IEntryAction
{
    private readonly Func<EntryContext, ValueTask> _entryAction = entryAction ?? throw new ArgumentNullException(nameof(entryAction));

    public async ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
    {
        await _entryAction.Invoke(context);
        await next(context);
    }
}
