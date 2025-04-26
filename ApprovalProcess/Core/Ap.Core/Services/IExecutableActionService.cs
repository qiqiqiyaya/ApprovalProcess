using Ap.Core.Share.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public interface IExecutableActionService
    {
        ValueTask<List<ExecutableActionEntity>> GetListByNameAsync(params string[] actionNames);

        ValueTask<List<ExecutableActionEntity>> GetListByIdAsync(params string[] ids);

        ValueTask<ExecutableActionEntity> AddAsync(string name, string description, ExecutableActionType type);
    }
}
