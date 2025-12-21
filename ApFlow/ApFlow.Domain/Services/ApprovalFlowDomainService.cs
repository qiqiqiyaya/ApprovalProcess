using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ApFlow.Domain.Models;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ApFlow.Domain.Services
{
    /// <summary>
    /// 审批流程领域服务实现，处理审批流程的核心业务逻辑
    /// </summary>
    public class ApprovalFlowDomainService : DomainService, IApprovalFlowDomainService
    {
        private readonly IRepository<ApprovalFlow, Guid> _approvalFlowRepository;
        private readonly IRepository<Form, Guid> _formRepository;
        private readonly IRepository<Graph, Guid> _graphRepository;
        private readonly IRepository<ApprovalFlowInstance, Guid> _instanceRepository;

        public ApprovalFlowDomainService(
            IRepository<ApprovalFlow, Guid> approvalFlowRepository,
            IRepository<Form, Guid> formRepository,
            IRepository<Graph, Guid> graphRepository,
            IRepository<ApprovalFlowInstance, Guid> instanceRepository)
        {
            _approvalFlowRepository = approvalFlowRepository;
            _formRepository = formRepository;
            _graphRepository = graphRepository;
            _instanceRepository = instanceRepository;
        }

        /// <summary>
        /// 创建新的审批流程
        /// </summary>
        public async Task<ApprovalFlow> CreateAsync(
            string name,
            string description,
            string identity,
            Guid formId,
            Guid graphId)
        {
            // 验证表单是否存在
            var form = await _formRepository.FirstOrDefaultAsync(x => x.Id == formId);
            if (form == null)
            {
                throw new InvalidOperationException($"表单ID {formId} 不存在");
            }

            // 验证流程图是否存在
            var graph = await _graphRepository.FirstOrDefaultAsync(x => x.Id == graphId);
            if (graph == null)
            {
                throw new InvalidOperationException($"流程图ID {graphId} 不存在");
            }

            // 验证流程标识是否已存在
            var existingFlow = await _approvalFlowRepository.FirstOrDefaultAsync(x => x.BasicInfo.Identity == identity);
            if (existingFlow != null)
            {
                throw new InvalidOperationException($"流程标识 {identity} 已存在");
            }

            // 创建流程基本信息
            var flowBasicInfoId = GuidGenerator.Create();
            var approvalFlowId = GuidGenerator.Create();
            var flowBasicInfo = new FlowBasicInfo(
                flowBasicInfoId,
                name,
                description,
                identity,
                approvalFlowId);

            // 创建审批流程
            var approvalFlow = new ApprovalFlow(
                approvalFlowId,
                flowBasicInfo,
                formId,
                graphId);
            flowBasicInfo,
                formId,
                graphId);

            return await _approvalFlowRepository.InsertAsync(approvalFlow, autoSave: true);
        }

        /// <summary>
        /// 更新审批流程基本信息
        /// </summary>
        public async Task<ApprovalFlow> UpdateBasicInfoAsync(
            ApprovalFlow approvalFlow,
            string name,
            string description,
            string identity)
        {
            // 验证流程标识是否已被其他流程使用
            if (identity != approvalFlow.BasicInfo.Identity)
            {
                var existingFlow = await _approvalFlowRepository.FirstOrDefaultAsync(x => x.BasicInfo.Identity == identity && x.Id != approvalFlow.Id);
                if (existingFlow != null)
                {
                    throw new InvalidOperationException($"流程标识 {identity} 已被其他流程使用");
                }
            }

            // 创建新的基本信息对象
            var updatedBasicInfo = new FlowBasicInfo(
                approvalFlow.BasicInfo.Id,
                name,
                description,
                identity,
                approvalFlow.Id);

            // 更新审批流程
            approvalFlow = new ApprovalFlow(
                approvalFlow.Id,
                updatedBasicInfo,
                approvalFlow.FormId,
                approvalFlow.GraphId);

            return await _approvalFlowRepository.UpdateAsync(approvalFlow, autoSave: true);
        }

        /// <summary>
        /// 更新审批流程关联的表单
        /// </summary>
        public async Task<ApprovalFlow> UpdateFormAsync(
            ApprovalFlow approvalFlow,
            Guid formId)
        {
            // 验证表单是否存在
            var form = await _formRepository.FirstOrDefaultAsync(x => x.Id == formId);
            if (form == null)
            {
                throw new InvalidOperationException($"表单ID {formId} 不存在");
            }

            // 检查是否有正在运行的流程实例使用了此流程
            var hasRunningInstances = await _instanceRepository.AnyAsync(x => x.ApprovalFlowId == approvalFlow.Id);
            if (hasRunningInstances)
            {
                throw new InvalidOperationException("存在正在运行的流程实例，无法更改关联表单");
            }

            // 更新审批流程
            approvalFlow = new ApprovalFlow(
                approvalFlow.Id,
                approvalFlow.BasicInfo,
                formId,
                approvalFlow.GraphId);

            return await _approvalFlowRepository.UpdateAsync(approvalFlow, autoSave: true);
        }

        /// <summary>
        /// 更新审批流程关联的流程图
        /// </summary>
        public async Task<ApprovalFlow> UpdateGraphAsync(
            ApprovalFlow approvalFlow,
            Guid graphId)
        {
            // 验证流程图是否存在
            var graph = await _graphRepository.FirstOrDefaultAsync(x => x.Id == graphId);
            if (graph == null)
            {
                throw new InvalidOperationException($"流程图ID {graphId} 不存在");
            }

            // 检查是否有正在运行的流程实例使用了此流程
            var hasRunningInstances = await _instanceRepository.AnyAsync(x => x.ApprovalFlowId == approvalFlow.Id);
            if (hasRunningInstances)
            {
                throw new InvalidOperationException("存在正在运行的流程实例，无法更改关联流程图");
            }

            // 更新审批流程
            approvalFlow = new ApprovalFlow(
                approvalFlow.Id,
                approvalFlow.BasicInfo,
                approvalFlow.FormId,
                graphId);

            return await _approvalFlowRepository.UpdateAsync(approvalFlow, autoSave: true);
        }

        /// <summary>
        /// 验证审批流程是否可以删除
        /// </summary>
        public async Task<bool> CanDeleteAsync(ApprovalFlow approvalFlow)
        {
            // 检查是否有正在运行的流程实例
            var hasRunningInstances = await _instanceRepository.AnyAsync(x => x.ApprovalFlowId == approvalFlow.Id);
            if (hasRunningInstances)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证审批流程定义是否完整有效
        /// </summary>
        public async Task<bool> ValidateAsync(ApprovalFlow approvalFlow)
        {
            // 验证基本信息
            if (approvalFlow.BasicInfo == null || string.IsNullOrWhiteSpace(approvalFlow.BasicInfo.Name))
            {
                return false;
            }

            // 验证表单是否存在
            var form = await _formRepository.FirstOrDefaultAsync(x => x.Id == approvalFlow.FormId);
            if (form == null)
            {
                return false;
            }

            // 验证流程图是否存在
            var graph = await _graphRepository.FirstOrDefaultAsync(x => x.Id == approvalFlow.GraphId);
            if (graph == null)
            {
                return false;
            }

            // 验证流程图是否有效（至少有一个开始节点和结束节点）
            // 这里可以添加更复杂的验证逻辑，比如验证流程图的连通性等

            return true;
        }

        /// <summary>
        /// 获取审批流程的发起人权限
        /// </summary>
        public async Task<ICollection<StarterPermissionReference>> GetStarterPermissionsAsync(ApprovalFlow approvalFlow)
        {
            // 注意：这里需要根据实际的数据库设计来获取关联的发起人权限
            // 由于StarterPermissionReference是独立实体，需要通过查询获取
            // 这里返回空集合作为占位符
            return new List<StarterPermissionReference>();
        }

        /// <summary>
        /// 添加审批流程的发起人权限
        /// </summary>
        public async Task<ApprovalFlow> AddStarterPermissionAsync(
            ApprovalFlow approvalFlow,
            StarterPermissionReference starterPermission)
        {
            // 注意：由于StarterPermissionReference是独立实体，不是FlowBasicInfo的一部分
            // 这里需要通过仓储来处理关联关系
            // 实际实现可能需要根据具体的数据库设计来调整

            // 更新审批流程（实际上不需要更新ApprovalFlow本身）
            return approvalFlow;
        }

        /// <summary>
        /// 移除审批流程的发起人权限
        /// </summary>
        public async Task<ApprovalFlow> RemoveStarterPermissionAsync(
            ApprovalFlow approvalFlow,
            Guid starterPermissionId)
        {
            // 注意：由于StarterPermissionReference是独立实体，不是FlowBasicInfo的一部分
            // 这里需要通过仓储来处理删除关联关系
            // 实际实现可能需要根据具体的数据库设计来调整

            // 更新审批流程（实际上不需要更新ApprovalFlow本身）
            return approvalFlow;
        }
    }
}