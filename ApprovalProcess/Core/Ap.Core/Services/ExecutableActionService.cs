using Ap.Core.Share.Entities;
using Ap.Core.Share.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
	public class ExecutableActionService(
		IApRepository apRepository,
		ILogger<ExecutableActionService> logger)
		: BaseActionService(apRepository, logger), IExecutableActionService
	{
		private readonly IApRepository _apRepository = apRepository;

		public async ValueTask<List<ExecutableActionEntity>> GetListByNameAsync(params string[] actionNames)
		{
			var container = await GetAllAsync();

			List<ExecutableActionEntity> actions = new List<ExecutableActionEntity>();
			foreach (var name in actionNames)
			{
				var act = container.NameContainer[name];
				if (act == null)
				{
					throw new Exception($"Can't get action {name}");
				}

				actions.Add(act);
			}

			return actions;
		}

		public async ValueTask<List<ExecutableActionEntity>> GetListByIdAsync(params string[] ids)
		{
			var container = await GetAllAsync();

			List<ExecutableActionEntity> actions = new List<ExecutableActionEntity>();
			foreach (var id in ids)
			{
				var act = container.IdContainer[id];
				if (act == null)
				{
					throw new Exception($"Can't get action {id}");
				}

				actions.Add(act);
			}

			return actions;
		}

		public async ValueTask<ExecutableActionEntity> AddAsync(string name, string description, ExecutableActionType type)
		{
			if (Check(name))
			{

			}

			var entity = new ExecutableActionEntity
			{
				Name = name,
				Description = description,
				Type = type
			};

			await SingleAsync(async () =>
			{
				entity = await _apRepository.AddActionAsync(entity);
				AddAction(entity);
			});

			return entity;
		}
	}
}
