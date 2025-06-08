using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ap.Core.Host
{
    /// <summary>
    /// 锁分发中心
    /// </summary>
    public class MainThread
    {
        private readonly IServiceProvider _serviceProvider;
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        public MainThread() { }

        public ValueTask GetAsync(CancellationToken stoppingToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);
            return new ValueTask(Task.CompletedTask);
        }
        private Task ExecuteAsync(CancellationToken token)
        {
            Task create = new Task<Task>(async () =>
            {
                //                var queue = _serviceProvider.GetRequiredService<IBlockingProjectQueue>();

                //                while (!token.IsCancellationRequested)
                //                {
                //                    await _semaphoreSlim.WaitAsync(token);
                //                    var project = await queue.Dequeue();

                //                    var scope = _serviceProvider.CreateScope();
                //                    ProjectContext context = new ProjectContext(scope,
                //                        new MainThreadTaskScheduler(1),
                //                        project);

                //                    Task<Task> task = new Task<Task>(() =>
                //                    {
                //                        var actuator = context.GetRequiredService<IProjectExecutor>();
                //                        _logger.LogInformation($"Project {project} is executing.");
                //                        var vt = actuator.ExecuteAsync(context, token);
                //                        return vt.AsTask();
                //                    });
                //                    var unwrap = task.Unwrap();

                //#pragma warning disable CS4014
                //                    unwrap.ContinueWith(tk =>
                //#pragma warning restore CS4014
                //                    {
                //                        context.Dispose();
                //                        _semaphoreSlim.Release();
                //                    }, token);

                //                    task.Start(context.TaskScheduler);
                //                }
            }, TaskCreationOptions.LongRunning);

            create.Start();
            //_logger.LogInformation($"{nameof(SalaryEngineHostService)} was starting.");
            return create;
        }

    }
}
