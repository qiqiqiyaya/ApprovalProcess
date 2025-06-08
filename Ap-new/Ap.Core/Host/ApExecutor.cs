using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ap.Core.Host.Interfaces;

namespace Ap.Core.Host
{
    public class ApExecutor
    {
        private readonly IApAcq _apAcq;

        public ApExecutor(IApAcq apAcq)
        {
            _apAcq = apAcq;
        }

        public ValueTask InvokeAsync(ApParameter parameter)
        {
            // 向注册中心查看 是否存在锁
            //var wrap = _apAcq.Get(id);

            // create concurrent lock
            //wrap.ConcurrentLock ??= Guid.NewGuid().ToString("N");
            //wrap.StateSet.


        }
    }
}
