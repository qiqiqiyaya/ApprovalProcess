namespace Ap.Core.Behaviours
{
    /// <summary>
    /// transition to destination state
    /// </summary>
    /// <param name="destination"></param>
    public class Approve(string destination) : BehaviourBase(ApCoreTriggers.Approve, destination)
    {

    }
}
