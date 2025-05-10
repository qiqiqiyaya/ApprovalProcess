using Ap.Share.Actions.Entry;
using Ap.Share.Actions.Entry.NextApprover;
using Ap.Share.Entities;
using Ap.Share.Repositories;
using Sm.Core.Actions.Entry;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Actions.Entry.NextApprover
{
    public class ApprovedByOrgService : IApprovedByOrgService
    {
        public ApprovedByOrgService()
        {

        }

        public async ValueTask InvokeAsync(EntryActionContext<string, string> context)
        {
            var record = (TriggeredRecordEntity)context.Properties[EntryActionContextExtensions.TriggeredRecord];

            // 下一级审批人为 Firer 上级
            //var employee = await context.GetEmployeeCacheAsync(context.Firer, EntryActionContextExtensions.FirerInfoKey);
            var employee = await context.GetEmployeeCacheAsync("5b020718b77941288e38699ed3e8a928", EntryActionContextExtensions.FirerInfoKey);
            var managers = employee.Organization.Managers;

            var list = new List<NextApproverEntity>();
            foreach (var item in managers)
            {
                var next = new NextApproverEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ApproverId = item.EmployeeId,
                    Definition = "Employee",
                    TriggeredRecordId = record.Id,
                };
                list.Add(next);
            }

            var repository = context.LazyGetRequiredService<IApRepository>();
            await repository.SaveNextApproversAsync(list);
        }
    }
}
