namespace Ap.Core.Builders
{
    public interface IPreBuilder
    {
        IStateSetBuilder Build(IStateSetBuilderProvider builderProvider);
    }
}
