using Ap.Core.Share.Entities;
using Ap.Core.Share.Repositories;

namespace Ap.Repository.FreeSql.Repositories
{
	public class ApRepository(IFreeSql freeSql) : IApRepository
	{
		public async ValueTask<StateMachineEntity> GetStateMachine(string id)
		{
			var entity = await freeSql.Select<StateMachineEntity>()
			   .IncludeMany(s => s.StateSettings)
			   //.IncludeMany(s => s.StateSettings.Select(se => se.Actions))
			   //.IncludeMany(s => s.StateSettings.Select(se => se.Transitions))
			   .Where(x => x.Id == id)
			   .FirstAsync();


			if (entity == null)
			{
				throw new Exception($"Can't find Entity by {id}");
			}

			return entity;
		}

		public async ValueTask<StateSettingsEntity> GetStateSettings(string id)
		{
			var entity = await freeSql.Select<StateSettingsEntity>()
			   .Include(s => s.Actions)
			   .Include(s => s.Transitions)
			   .Where(x => x.Id == id)
			   .FirstAsync();

			if (entity == null)
			{
				throw new Exception($"Can't find Entity by {id}");
			}

			return entity;
		}

		public async ValueTask<List<ExecutableActionEntity>> GetExecutableActionAllAsync()
		{
			return await freeSql.Select<ExecutableActionEntity>()
				.ToListAsync();
		}

		public async ValueTask<ExecutableActionEntity> AddActionAsync(ExecutableActionEntity entity)
		{
			entity.Id = Guid.NewGuid().ToString("N");
			await freeSql.Insert(entity).ExecuteAffrowsAsync();
			return entity;
		}

		public async ValueTask<StateMachineEntity> SaveAsync(StateMachineEntity entity)
		{
			int count = await freeSql.Insert(entity).ExecuteAffrowsAsync();
			if (count == 0)
			{
				throw new Exception($"Can't save Entity {entity.Id} to database");
			}

			return entity;
		}
	}
}
