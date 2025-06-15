using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ap.Core.Host
{
    public class MainThreadTaskScheduler : TaskScheduler, IDisposable
    {
        private readonly CancellationTokenSource _disposeCancellation = new CancellationTokenSource();

        private readonly BlockingCollection<Task> _blockingTaskQueue;

        private readonly ThreadLocal<bool> _taskProcessingThread = new ThreadLocal<bool>();

        public override int MaximumConcurrencyLevel { get; }

        public MainThreadTaskScheduler(int threadCount)
        {
            MaximumConcurrencyLevel = threadCount;
            _blockingTaskQueue = new BlockingCollection<Task>();

            var threads = new Thread[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                threads[i] = new Thread(ThreadBasedDispatchLoop)
                {
                    Priority = ThreadPriority.Normal,
                    IsBackground = true,
                    Name = $"threadName ({i})"
                };
            }

            // start
            foreach (var thread in threads)
            {
                thread.Start();
            }
        }

        private void ThreadBasedDispatchLoop()
        {
            _taskProcessingThread.Value = true;

            try
            {
                // If a thread abort occurs, we'll try to reset it and continue running.
                while (true)
                {
                    try
                    {
                        // For each task queued to the scheduler, try to execute it.
                        foreach (var task in _blockingTaskQueue.GetConsumingEnumerable(_disposeCancellation.Token))
                        {
                            TryExecuteTask(task);
                        }
                    }
                    catch (ThreadAbortException)
                    {
                        // If we received a thread abort, and that thread abort was due to shutting down
                        // or unloading, let it pass through.  Otherwise, reset the abort so we can
                        // continue processing work items.
                        if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
                        {
#pragma warning disable SYSLIB0006
                            Thread.ResetAbort();
#pragma warning restore SYSLIB0006
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // If the scheduler is disposed, the cancellation token will be set and
                // we'll receive an OperationCanceledException.  That OCE should not crash the process.
            }
            finally
            {
                _taskProcessingThread.Value = false;
            }
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _blockingTaskQueue.ToList();
        }

        protected override void QueueTask(Task task)
        {
            if (_disposeCancellation.IsCancellationRequested)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            _blockingTaskQueue.Add(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            // If we're already running tasks on this threads, enable inlining
            return _taskProcessingThread.Value && TryExecuteTask(task);
        }

        public void Dispose()
        {
            _disposeCancellation.Cancel();
        }
    }
}
