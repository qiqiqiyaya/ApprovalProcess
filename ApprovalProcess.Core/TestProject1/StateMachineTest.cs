using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using ApprovalProcess.Core;

namespace TestProject1
{
    public enum PeState
    {
        /// <summary>
        /// 正在编辑
        /// </summary>
        Edit,
        /// <summary>
        /// 第一次审批
        /// </summary>
        FirstApprove,
        /// <summary>
        /// 第二次审批
        /// </summary>
        SecondApprove,
        /// <summary>
        /// 退回
        /// </summary>
        Return,
        /// <summary>
        /// 审批完成
        /// </summary>
        Completed,
    }

    /// <summary>
    /// 触发器
    /// </summary>
    public enum PeStateTrigger
    {
        /// <summary>
        /// 提交
        /// </summary>
        Submitted,
        /// <summary>
        /// 第一次审批通过
        /// </summary>
        FirstApprovedPass,
        /// <summary>
        /// 第二次审批通过
        /// </summary>
        SecondApprovedPass,
        /// <summary>
        /// 拒绝
        /// </summary>
        Reject,
        /// <summary>
        /// 重写
        /// </summary>
        Rewrite,
    }

    public class StateMachineTest
    {

        [Fact]
        public void StateMachine_InitialState_Test()
        {

            var _machine = new StateMachine<PeState, PeStateTrigger>(PeState.Edit);

            // 编辑 -> 提交
            _machine.Configure(PeState.Edit)
                .Permit(PeStateTrigger.Submitted, PeState.FirstApprove);

            // 退回后，重写 -> 编辑状态
            _machine.Configure(PeState.Return)
                .Permit(PeStateTrigger.Rewrite, PeState.Edit);

            // 第一级审批
            _machine.Configure(PeState.FirstApprove)
                .Permit(PeStateTrigger.FirstApprovedPass, PeState.SecondApprove)
                .Permit(PeStateTrigger.Reject, PeState.Return);

            // 第二级审批
            _machine.Configure(PeState.SecondApprove)
                .Permit(PeStateTrigger.SecondApprovedPass, PeState.Completed)
                .Permit(PeStateTrigger.Reject, PeState.Return);

            _machine.Fire(PeStateTrigger.Submitted);

            Assert.Equal(PeState.FirstApprove, _machine.CurrentState);
        }
    }
}
