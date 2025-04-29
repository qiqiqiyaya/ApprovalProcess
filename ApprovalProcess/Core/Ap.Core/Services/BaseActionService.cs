using Ap.Core.Share.Entities;
using Ap.Core.Share.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class BaseActionService(
        IApRepository apRepository)
    {
        private static Dictionary<string, ExecutableActionEntity> _nameContainer;
        private static Dictionary<string, ExecutableActionEntity> _idContainer;

        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1);

        protected virtual async ValueTask<ActionResult> GetAllAsync()
        {
            await CheckContainer();
            return new ActionResult(_nameContainer, _idContainer);
        }

        protected virtual async ValueTask CheckContainer()
        {
            if (_nameContainer != null) return;
            await SingleAsync(async () =>
            {
                if (_nameContainer == null)
                {
                    var list = await apRepository.GetExecutableActionAllAsync();

                    _nameContainer = new Dictionary<string, ExecutableActionEntity>();
                    _idContainer = new Dictionary<string, ExecutableActionEntity>();
                    foreach (var entity in list)
                    {
                        _nameContainer.Add(entity.Name, entity);
                        _idContainer.Add(entity.Id, entity);
                    }
                }
            });
        }

        protected virtual async ValueTask SingleAsync(Func<ValueTask> func)
        {
            try
            {
                await SemaphoreSlim.WaitAsync();
                await func();
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }

        protected virtual bool IsExists(string name)
        {
            return _nameContainer!.ContainsKey(name);
        }

        protected void AddAction(ExecutableActionEntity entity)
        {
            _nameContainer!.Add(entity.Name, entity);
        }
    }
}
