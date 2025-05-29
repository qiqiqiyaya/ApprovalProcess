namespace Ap.Flow.Behaviours
{
	public class ReturnToStart(string destination)
		: TriggerBehaviour(BehaviourConst.Reject, destination)
	{
	}
}
