using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiParallelApproval
{
    public class ApprovalService
    {
        public async Task<IActionResult> ApproveTask(ApproveRequest request)
        {
            using var transaction = BeginTransaction();

            var task = await _taskRepository.GetTaskWithVersion(request.TaskId, request.Version);
            ValidateApprovalAuthority(task, request.User);

            task.Status = request.Decision;
            task.Comments = request.Comments;
            task.ModifiedTime = DateTime.UtcNow;

            try
            {
                await _taskRepository.UpdateWithVersion(task);
                await PublishApprovalEvent(task);
                transaction.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                transaction.Rollback();
                throw new ConcurrencyException("审批状态已变更，请刷新后重试");
            }
        }
    }
}
