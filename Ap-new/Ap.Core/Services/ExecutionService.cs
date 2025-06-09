using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class ExecutionService
    {
        private readonly MemoryStorage _storage;
        private readonly FlowService _flowService;

        public ExecutionService(MemoryStorage storage,
            FlowService flowService)
        {
            _storage = storage;
            _flowService = flowService;
        }

        public async ValueTask InvokeAsync(ExecutionParameter parameter)
        {
            // 状态机
            var set = await _storage.GetAsync(parameter.StateSetId);

            parameter.
            _flowService.Get()

            // 恢复状态机状态
        }
    }
}
