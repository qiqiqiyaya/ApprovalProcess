using ApprovalProcess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using Ap.Core;
using Ap.Register;
using Core;
using Core.Workflows;
using Test.Service;

namespace TestProject1
{
    public class UnitTest1
    {
        readonly IServiceProvider _serviceProvider;

        public UnitTest1()
        {
            ServiceCollection service = new ServiceCollection();
            service.AddAp();
            _serviceProvider = service.BuildServiceProvider();
            var dbcontext = GetRequiredService<ApprovalProcessDbContext>();
            dbcontext.Database.EnsureCreated();
        }

        [Fact]
        public async Task ObjectCreateTest()
        {
            var peService = GetRequiredService<IPeService>();
            var employeeService = GetRequiredService<IEmployeeService>();
            var workflow = GetRequiredService<IWorkflow>();

            var emp = await employeeService.GetAsync(1);
            Assert.NotNull(emp);

            var pe = await peService.CreateAsync(emp.Id);
            Assert.NotNull(pe);

            var state = await workflow.GetStateAsync(pe.WorkflowId);
            Assert.NotNull(state);
            Assert.Equal("Edit", state);

            var actions = workflow.NextAction();
            Assert.NotNull(actions);
            var next = workflow.Execute("Submitted");
            Assert.NotNull(next);
        }

        /// <summary>
        /// 指定签核人
        /// </summary>
        /// <returns></returns>
        public async ValueTask SpecifyApproverTest()
        {

        }

        public T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
