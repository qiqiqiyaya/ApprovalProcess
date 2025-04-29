using Ap.Core.Share.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public interface IExecutableActionService
    {
        ValueTask<Dictionary<string, ExecutableActionEntity>> GetListByNameAsync(params string[] actionNames);

        ValueTask<Dictionary<string, ExecutableActionEntity>> GetListByIdAsync(params string[] ids);

        ValueTask<ExecutableActionEntity> AddAsync(string name, string description, ExecutableActionType type);
    }
}
