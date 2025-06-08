using System.Threading.Tasks;

namespace Ap.Core.Host.Interfaces
{
    public interface IBlockingLockQueue
    {
        ValueTask Enqueue();

        /// <summary>
        /// 专案出列
        /// </summary>
        /// <returns></returns>
        ValueTask Dequeue();

        /// <summary>
        /// 停止接受新任务
        /// </summary>
        void CompleteAdding();
    }
}
