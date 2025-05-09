﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sm.Core.Actions;
using Sm.Share.Entities;
using Sm.Share.Repositories;

namespace Sm.Core.Services
{
    public class ExecutableActionService(
        ISmRepository smRepository,
        ExecutableActionContainer actionContainer)
        : BaseActionService(smRepository), IExecutableActionService
    {
        private readonly ISmRepository _smRepository = smRepository;

        public async ValueTask<Dictionary<string, ExecutableActionEntity>> GetListByNameAsync(params string[] actionNames)
        {
            var container = await GetAllAsync();

            //actionContainer.GetEntryActions()

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

        public async ValueTask<ExecutableActionEntity> AddAsync(string name, string description, ExecutableActionType type, Type actionType)
        {
            await CheckContainer();

            string typeString = actionType.FullName + "," + actionType.Assembly.FullName;
            var entity = new ExecutableActionEntity
            {
                Name = name,
                Description = description,
                EventType = type,
                Id = Guid.NewGuid().ToString("N"),
                ActionType = typeString
            };

            await SingleAsync(async () =>
            {
                if (IsExists(name))
                {
                    return;
                }

                entity = await _smRepository.AddActionAsync(entity);
                AddAction(entity);
            });

            return entity;
        }
    }
}
