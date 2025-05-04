using System.Threading.Tasks;

namespace Ap.Share.Services
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
