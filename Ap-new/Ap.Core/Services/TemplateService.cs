using Ap.Core.Builders;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Services;
public class TemplateService
{
    private readonly MemoryStorage _storage;
    private readonly IStateSetBuilderProvider _stateSetBuilderProvider;

    public TemplateService(MemoryStorage storage, IStateSetBuilderProvider stateSetBuilderProvider)
    {
        _storage = storage;
        _stateSetBuilderProvider = stateSetBuilderProvider;
    }

    public ValueTask CreateAsync(Func<IStateSetBuilderProvider, IStateSetBuilder> createAction)
    {
        var builder = createAction(_stateSetBuilderProvider);
        return CreateAsync(builder);
    }

    public ValueTask CreateAsync(IStateSetBuilder builder)
    {
        var set = builder.Build();
        return _storage.AddAsync(set);
    }
}
