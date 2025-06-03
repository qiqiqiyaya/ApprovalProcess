namespace Ap.Core.Builders
{
    public interface IContainerStateSetBuilder : IStateSetBuilder<IContainerStateSetBuilder>
    {
        IContainerStateSetBuilder JumpOut(string state, string destination);
    }
}
