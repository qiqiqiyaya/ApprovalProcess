using Ap.Core.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Common;

namespace TestProject1
{
	public class EmployeeApFlowTest : BaseTest
	{
		[Fact]
		public async Task CreateApFlowTest()
		{
			var sm = StateMachineTestData.TwoLevelApprovalProcess();

			var org = new Organization()
			{
				Name = "测试组织",
				Code = "1",
				ParentCode = null
			};

			var emp = new Employee()
			{
				Code = "1",
				Organization = org
			};

			var flow = new EmployeeCreateApFlow()
			{
				Employee = emp,
				StateMachine = sm
			};


		}
	}
}
