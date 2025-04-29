using Ap.Core.Share.Entities;
using Ap.Core.Share.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class ExecutableActionService(
        IApRepository apRepository)
        : BaseActionService(apRepository), IExecutableActionService
    {
        private readonly IApRepository _apRepository = apRepository;

        public async ValueTask<Dictionary<string, ExecutableActionEntity>> GetListByNameAsync(params string[] actionNames)
        {
            var container = await GetAllAsync();

            var actions = new Dictionary<string, ExecutableActionEntity>();
            foreach (var name in actionNames)
            {
                if (container.NameContainer.TryGetValue(name, out var act))
                {
                    actions.Add(name, act);
                }
            }

            return actions;
        }

        public async ValueTask<Dictionary<string, ExecutableActionEntity>> GetListByIdAsync(params string[] ids)
        {
            var container = await GetAllAsync();

            var actions = new Dictionary<string, ExecutableActionEntity>();
            foreach (var id in ids)
            {
                if (container.IdContainer.TryGetValue(id, out var act))
                {
                    actions.Add(id, act);
                }
            }

            return actions;
        }

        public async ValueTask<ExecutableActionEntity> AddAsync(string name, string description, ExecutableActionType type)
        {
            await CheckContainer();

            var entity = new ExecutableActionEntity
            {
                Name = name,
                Description = description,
                Type = type,
                Id = Guid.NewGuid().ToString("N")
            };

            await SingleAsync(async () =>
            {
                if (IsExists(name))
                {
                    return;
                }

                entity = await _apRepository.AddActionAsync(entity);
                AddAction(entity);
            });

            return entity;
        }
    }
}
