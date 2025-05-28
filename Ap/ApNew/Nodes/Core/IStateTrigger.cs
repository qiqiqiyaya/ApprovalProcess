namespace ApNew.Nodes.Core
{
	public interface IStateTrigger
	{
		void ExecuteTrigger(TriggerParameter trigger);

		void ExecuteTrigger(string trigger);
	}
}
