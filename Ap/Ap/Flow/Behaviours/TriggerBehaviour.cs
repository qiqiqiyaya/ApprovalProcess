namespace Ap.Flow.Behaviours
{
	/// <summary>
	/// 状态转换触发器
	/// </summary>
	public abstract class TriggerBehaviour(string trigger, string destination)
		: ITriggerBehaviour
	{
		public string Trigger { get; set; } = trigger;

		/// <summary>
		/// DestinationState
		/// </summary>
		public string Destination { get; set; } = destination;

		public virtual void Invoke(IBehaviourContext context)
		{

		}

		public ValueTask InvokeAsync(IBehaviourContext context)
		{
			return new ValueTask();
		}
	}
}
