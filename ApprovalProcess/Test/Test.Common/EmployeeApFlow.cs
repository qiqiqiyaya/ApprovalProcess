using Sm.Core.StateMachine;

namespace Test.Common
{
    /// <summary>
    /// 审批流程
    /// </summary>
    public class EmployeeApFlow<TState, TTrigger>
    {
        public Employee Employee { get; set; }

        public StateMachine<TState, TTrigger> StateMachine { get; set; }
    }

    public class EmployeeApFlow : EmployeeApFlow<string, string>
    {

    }
}
