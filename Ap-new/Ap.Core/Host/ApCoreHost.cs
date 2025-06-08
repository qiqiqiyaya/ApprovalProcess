using System.Threading.Tasks;

namespace Ap.Core.Host
{
    public class ApCoreHost
    {
        public ValueTask StartAsync()
        {
            var main = new MainThreadTaskScheduler(1);


            return new ValueTask();
        }

        public ValueTask StopAsync()
        {
            return new ValueTask();
        }
    }
}
