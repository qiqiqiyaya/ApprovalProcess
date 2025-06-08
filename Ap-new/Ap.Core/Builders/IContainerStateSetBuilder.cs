namespace Ap.Core.Builders
{
	public interface IContainerStateSetBuilder : IStateSetBuilder<IContainerStateSetBuilder>
	{
		IContainerStateSetBuilder JumpOut(string name, string destination);
	}
}
