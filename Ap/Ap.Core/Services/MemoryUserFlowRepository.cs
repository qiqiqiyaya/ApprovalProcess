using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class MemoryUserFlowRepository : IUserFlowRepository
    {
        private static readonly List<UserFlow> _userFlows = new();

        public MemoryUserFlowRepository() { }

        public ValueTask<UserFlow> CreateAsync(UserFlow userFlow)
        {
            _userFlows.Add(userFlow);
            return new ValueTask<UserFlow>(userFlow);
        }

        public ValueTask<UserFlow> CreateAsync(string userId)
        {
            var uf = _userFlows.Find(x => x.UserId == userId);
            return new ValueTask<UserFlow>(uf);
        }
    }
}
