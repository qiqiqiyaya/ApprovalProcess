namespace TestProject1
{
    public class EmployeeApFlowTest : BaseTest
    {


        [Fact]
        public async Task CreateApFlowTest()
        {
            var sm = StateMachineTestData.TwoLevelApprovalProcess();

            //var org = new Organization()
            //{
            //    Name = "测试组织",
            //    Code = "1",
            //    ParentCode = null
            //};

            //var emp = new Employee()
            //{
            //    Code = "1",
            //    //Organization = org
            //};

            //var flow = new EmployeeCreateApFlow()
            //{
            //    Employee = emp,
            //    StateMachine = sm
            //};


        }
    }
}
