using System.Threading.Tasks;
using Ap.Share.Services;

namespace Ap.Core.Services
{
    public class ApFlowService : IApFlowService
    {
        public ApFlowService()
        {
        }

        public ValueTask ConfigureAsync()
        {
            return new ValueTask();
        }
    }
}
