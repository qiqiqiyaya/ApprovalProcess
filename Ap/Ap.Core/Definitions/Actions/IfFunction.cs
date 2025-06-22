using System;
using System.Threading.Tasks;

namespace Ap.Core.Definitions;

public class PredicateContext(IServiceProvider serviceProvider)
{
    public IServiceProvider ServiceProvider { get; set; } = serviceProvider;
}

public interface IIfPredicate
{
    public ValueTask<bool> InvokeAsync(PredicateContext context);
}

public class IfFunction(Func<PredicateContext, ValueTask<bool>> predicate) : IIfPredicate
{
    public async ValueTask<bool> InvokeAsync(PredicateContext context)
    {
        return await predicate(context);
    }
}