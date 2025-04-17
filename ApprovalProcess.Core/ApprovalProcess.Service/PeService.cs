using ApprovalProcess.Core;
using ApprovalProcess.Core.Workflows;
using ApprovalProcess.Repository;
using ApprovalProcess.Share.Entities;

namespace ApprovalProcess.Service
{
    public class PeService : IPeService
    {
        private readonly IWorkflow _peWorkflow;
        private readonly ApprovalProcessDbContext _dbContext;

        public PeService(IWorkflow peWorkflow,
            ApprovalProcessDbContext dbContext)
        {
            _peWorkflow = peWorkflow;
            _dbContext = dbContext;
        }

        public async ValueTask<PeEntity> CreateAsync(int employeeId)
        {
            var pe = PeEntity.Create(employeeId);
            var flow = await _peWorkflow.CreateAsync();
            pe.WorkflowId = flow.Id;

            await _dbContext.Pes.AddAsync(pe);
            await _dbContext.SaveChangesAsync();
            return pe;
        }

        public void Trigger(string eventName)
        {
            _peWorkflow.Execute(eventName);
        }
    }
}
