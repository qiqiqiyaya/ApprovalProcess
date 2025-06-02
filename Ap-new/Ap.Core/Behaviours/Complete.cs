namespace Ap.Core.Behaviours
{
    /// <summary>
    /// Indicates completion
    /// </summary>
    /// <param name="trigger"></param>
    public class Complete(string trigger) : BehaviourBase(trigger, TransitionConst.Complete)
    {


    }
}
