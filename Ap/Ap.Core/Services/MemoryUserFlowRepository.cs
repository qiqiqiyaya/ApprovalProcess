using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class MemoryUserFlowRepository : IUserFlowRepository
    {
        private static readonly List<UserFlow> Flows = new();

        public ValueTask<UserFlow> CreateAsync(UserFlow userFlow)
        {
            Flows.Add(userFlow);
            return new ValueTask<UserFlow>(userFlow);
        }

        public async ValueTask UpdateAsync(UserFlow userFlow)
        {
            var uf = await GetByIdAsync(userFlow.UserId);
            Flows.Remove(uf);
            Flows.Add(userFlow);
        }

        public async ValueTask UpdateAsync(Node node)
        {
            var uf = await GetByFlowIdAsync(node.Id);
            uf.ExecutionNode = node;
        }

        public ValueTask<UserFlow> GetByIdAsync(string userId)
        {
            return new ValueTask<UserFlow>(Flows.Find(x => x.UserId == userId));
        }

        public ValueTask<UserFlow> GetByFlowIdAsync(string flowId)
        {
            return new ValueTask<UserFlow>(Flows.Find(x => x.FlowId == flowId));
        }

        public ValueTask<Node> GetFlowAsync(string flowId)
        {
            return new ValueTask<Node>(Flows.Find(x => x.FlowId == flowId).ExecutionNode);
        }
    }
}
