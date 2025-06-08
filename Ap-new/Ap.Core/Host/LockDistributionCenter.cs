using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ap.Core.Host
{
    /// <summary>
    /// 锁分发中心
    /// </summary>
    public class LockDistributionCenter
    {
        private readonly Dictionary<string, string> _lockMap = new Dictionary<string, string>();
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private Task _executingTask;

        public LockDistributionCenter() { }


        public ValueTask StartAsync(CancellationToken stoppingToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);
            return new ValueTask(Task.CompletedTask);
        }

        private Task ExecuteAsync(CancellationToken token)
        {
            Task create = new Task<Task>(async () => { }, TaskCreationOptions.LongRunning);
            create.Start();
            return create;
        }

        public ValueTask StopAsync()
        {

        }
    }
}
