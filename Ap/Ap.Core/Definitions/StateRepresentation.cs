namespace Ap.Core.Definitions
{
    /// <summary>
    /// on state node
    /// </summary>
    public class StateRepresentation(string name, StateType stateType) : StateBase(name, stateType)
    {
        public StateRepresentation(string name)
            : this(name, StateType.General)
        {

        }
    }
}
