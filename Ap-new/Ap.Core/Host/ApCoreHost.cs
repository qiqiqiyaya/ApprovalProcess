using System.Threading.Tasks;

namespace Ap.Core.Host
{
    public class ApCoreHost
    {
        public ValueTask StartAsync()
        {
            return new ValueTask();
        }

        public ValueTask StopAsync()
        {
            return new ValueTask();
        }
    }
}
