using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiParallelApproval_2Stateless
{
    // 状态存储结构
    public class ProcessStateEntity
    {
        [Key]
        public Guid ProcessId { get; set; }

        [Required]
        public string CurrentState { get; set; }  // 存储状态机枚举值

        [Column(TypeName = "jsonb")]  // PostgreSQL JSONB类型
        public string ContextData { get; set; }  // 序列化后的ApprovalProcessContext
    }

    //// 状态恢复逻辑
    //    public async Task ResumeProcess(Guid processId)
    //        {
    //            var entity = await _dbContext.ProcessStates.FindAsync(processId);
    //            var context = JsonConvert.DeserializeObject<ApprovalProcessContext>(entity.ContextData);

    //            var stateMachine = new ApprovalStateMachine(context);
    //            stateMachine.RestoreState(entity.CurrentState);
    //        }
}
