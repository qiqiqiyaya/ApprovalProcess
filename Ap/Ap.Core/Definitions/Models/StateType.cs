namespace Ap.Core.Definitions;
public enum StateType
{

    Initial = 0,
    /// <summary>
    /// Indicates that the state is start state.
    /// </summary>
    Start = 1,
    /// <summary>
    /// Indicates that the state is end state.
    /// </summary>
    End = 2,

    General = 3,

    Completed = 4,

    /// <summary>
    /// Indicates that the StateSet just have one state.
    /// </summary>
    SingleState = 4,
}
