using Sm.Core.StateMachine;

namespace Test.Common
{
    public class EmployeeCreateApFlow : EmployeeApFlow<string, string>
    {
        public Employee Employee { get; set; }

        public StateMachine<string, string> StateMachine { get; set; }
    }
}
