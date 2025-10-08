using System.Threading.Tasks;
using OAApproval.DTOs;

namespace OAApproval.Services
{
    public interface IApprovalEngine
    {
        Task<ApprovalResult> StartProcessAsync(StartProcessRequest request);
        Task<ApprovalResult> ProcessTaskAsync(ProcessTaskRequest request);
        Task<bool> WithdrawAsync(int instanceId, string userId);
    }

    public class ApprovalResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int InstanceId { get; set; }

        public static ApprovalResult SuccessResult(int instanceId) => new ApprovalResult { Success = true, InstanceId = instanceId };
        public static ApprovalResult FailureResult(string message) => new ApprovalResult { Success = false, Message = message };
    }
}