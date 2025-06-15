using System;
using System.Threading.Tasks;

namespace Ap.Core.Host
{
    public class MainThreadInteractive
    {
        private readonly TaskFactory _taskFactory;

        public MainThreadInteractive(MainThreadTaskScheduler mainThread)
        {
            _taskFactory = new TaskFactory(mainThread);
        }

        public Task InvokeAsync(Action action)
        {
            return _taskFactory.StartNew(action);
        }

        public void Invoke(Action action)
        {
            _taskFactory.StartNew(action);
        }

        public Task Invoke(Func<Task> action)
        {
            return _taskFactory.StartNew(action);
        }

        protected void CheckThread()
        {
            //var id = Thread.CurrentThread.ManagedThreadId;
            //if (_mainThreadId != id)
            //{
            //	throw new Exception("not in main thread");
            //}
        }
    }
}
