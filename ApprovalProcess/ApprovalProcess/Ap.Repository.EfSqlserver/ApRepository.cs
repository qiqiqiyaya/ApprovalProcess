using Ap.Share.Entities;
using Ap.Share.Models;
using Ap.Share.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ap.Repository.EfSqlserver
{
    public class ApRepository : IApRepository
    {
        private readonly ApDbContext _context;

        public ApRepository(ApDbContext context)
        {
            _context = context;
        }

        public async ValueTask<User> GetEmployeeAsync(string id)
        {
            var employee = await _context.Users.SingleAsync(s => s.Id == id);

            var organization = await _context.Organizations
                .Include(s => s.Managers)
                .SingleAsync(s => s.Id == employee.OrganizationId);

            return Convert(employee, organization);
        }

        protected User Convert(UserEntity user, OrganizationEntity organization)
        {
            return new User()
            {
                Id = user.Id,
                Organization = new Organization()
                {
                    Id = organization.Id,
                    ParentId = organization.ParentId,
                    Managers = organization.Managers.Select(s => new Manager()
                    {
                        Id = s.Id,
                        EmployeeId = s.EmployeeId,
                    }).ToList()
                }
            };
        }

        public async ValueTask<TriggeredRecordEntity> SaveTriggeredRecordAsync(TriggeredRecordEntity entity)
        {
            await _context.TriggeredRecords.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<TriggeredRecordEntity?> GetLastTriggeredRecordAsync(string stateMachineId)
        {
            return await _context.TriggeredRecords
                .Where(x => x.StateMachineId == stateMachineId)
                .OrderByDescending(s => s.CreateTime)
                .FirstOrDefaultAsync();
        }

        public async ValueTask SaveNextApproversAsync(IEnumerable<NextApproverEntity> entities)
        {
            await _context.NextApprovers.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
    }
}
