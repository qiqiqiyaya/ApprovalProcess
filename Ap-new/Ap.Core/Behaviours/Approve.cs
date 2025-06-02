namespace Ap.Core.Behaviours
{
    /// <summary>
    /// transition to destination state
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="destination"></param>
    public class Approve(string trigger, string destination) : BehaviourBase(trigger, destination)
    {
    }
}
