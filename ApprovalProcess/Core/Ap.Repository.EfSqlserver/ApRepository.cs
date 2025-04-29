using Ap.Core.Share.Entities;
using Ap.Core.Share.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ap.Repository.EfSqlserver
{
	public class ApRepository(ApDbContext context) : IApRepository
	{
		public async ValueTask<StateMachineEntity> GetStateMachine(string id)
		{
			var entity = await context.StateMachines
				.Include(x => x.StateSettings)
				.ThenInclude(s => s.Actions)
				.Include(x => x.StateSettings)
				.ThenInclude(s => s.Transitions)
				.SingleAsync(x => x.Id == id);

			return entity;
		}

		public async ValueTask<StateSettingsEntity> GetStateSettings(string id)
		{
			return await context.StateSettings
			   .Include(x => x.Actions)
			   .Include(x => x.Transitions)
			   .SingleAsync(x => x.Id == id);
		}

		public async ValueTask<List<ExecutableActionEntity>> GetExecutableActionAllAsync()
		{
			return await context.ExecutableActions.ToListAsync();
		}

		public async ValueTask<ExecutableActionEntity> AddActionAsync(ExecutableActionEntity entity)
		{
			await context.ExecutableActions.AddAsync(entity);
			await context.SaveChangesAsync();
			return entity;
		}

		public async ValueTask<StateMachineEntity> SaveAsync(StateMachineEntity entity)
		{
			await context.StateMachines.AddAsync(entity);
			await context.SaveChangesAsync();
			return entity;
		}
	}
}
