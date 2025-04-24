namespace ApprovalProcess.Core.Actions
{
	/// <summary>
	/// 当退出 state 时执行 Action
	/// </summary>
	public interface IExitAction : IExecutableAction<ExitActionContext>
	{
	}
}
