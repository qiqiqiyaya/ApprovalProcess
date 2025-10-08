using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OAApproval.Data;
using OAApproval.DTOs;
using OAApproval.Models;
using TaskStatus = OAApproval.Models.TaskStatus;

namespace OAApproval.Services
{
    public class ApprovalEngine : IApprovalEngine
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApprovalEngine> _logger;

        public ApprovalEngine(ApplicationDbContext context, ILogger<ApprovalEngine> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApprovalResult> StartProcessAsync(StartProcessRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 创建实例
                var instance = new ApprovalInstance
                {
                    TemplateId = request.TemplateId,
                    Title = request.Title,
                    ApplicantId = request.ApplicantId,
                    ApplicantName = request.ApplicantName,
                    FormData = JsonSerializer.Serialize(request.FormData),
                    Status = ApprovalStatus.Processing,
                    ApplyTime = DateTime.Now,
                    InstanceNo = GenerateInstanceNo()
                };

                _context.ApprovalInstances.Add(instance);
                await _context.SaveChangesAsync();

                // 获取模板和流程定义
                var template = await _context.ApprovalTemplates.FindAsync(request.TemplateId);
                var processDefinition = JsonSerializer.Deserialize<ProcessDefinition>(template.ProcessDefinition);

                // 创建初始任务
                var firstStep = processDefinition.Steps.OrderBy(s => s.Step).First();
                var task = new ApprovalTask
                {
                    InstanceId = instance.Id,
                    TaskName = firstStep.Name,
                    AssigneeId = firstStep.AssigneeId,
                    AssigneeName = firstStep.AssigneeName,
                    Step = firstStep.Step,
                    Status = TaskStatus.Pending,
                    CreateTime = DateTime.Now
                };

                _context.ApprovalTasks.Add(task);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                // 发送通知（略）

                return ApprovalResult.SuccessResult(instance.Id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "启动流程失败");
                return ApprovalResult.FailureResult("启动流程失败");
            }
        }

        public Task<ApprovalResult> ProcessTaskAsync(ProcessTaskRequest request)
        {
            // 实现审批任务处理逻辑
            throw new System.NotImplementedException();
        }

        public Task<bool> WithdrawAsync(int instanceId, string userId)
        {
            // 实现撤回逻辑
            throw new System.NotImplementedException();
        }

        private string GenerateInstanceNo()
        {
            return "APP" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000, 9999);
        }
    }

    // 流程定义类（用于反序列化）
    public class ProcessDefinition
    {
        public StepInfo[] Steps { get; set; }
    }

    public class StepInfo
    {
        public int Step { get; set; }
        public string Name { get; set; }
        public string AssigneeId { get; set; }
        public string AssigneeName { get; set; }
        public int? NextStep { get; set; }
    }
}