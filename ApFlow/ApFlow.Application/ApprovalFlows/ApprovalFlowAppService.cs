using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ApFlow.Application.ApprovalFlows
{
    using ApFlow.Application.Contracts.ApprovalFlows;
    using ApFlow.Application.Contracts.ApprovalFlows.Dtos;
    using ApFlow.Domain.Models;
    using ApFlow.Domain.Services;

    /// <summary>
    /// 审批流程应用服务实现
    /// </summary>
    public class ApprovalFlowAppService : CrudAppService<
        ApprovalFlow,
        ApprovalFlowDto,
        Guid,
        GetApprovalFlowListDto,
        CreateApprovalFlowDto,
        UpdateApprovalFlowDto>, IApprovalFlowAppService
    {
        private readonly IApprovalFlowDomainService _approvalFlowDomainService;
        private readonly IRepository<Form, Guid> _formRepository;
        private readonly IRepository<Graph, Guid> _graphRepository;
        private readonly IRepository<StarterPermission, Guid> _starterPermissionRepository;

        public ApprovalFlowAppService(
            IRepository<ApprovalFlow, Guid> repository,
            IApprovalFlowDomainService approvalFlowDomainService,
            IRepository<Form, Guid> formRepository,
            IRepository<Graph, Guid> graphRepository,
            IRepository<StarterPermission, Guid> starterPermissionRepository)
            : base(repository)
        {
            _approvalFlowDomainService = approvalFlowDomainService;
            _formRepository = formRepository;
            _graphRepository = graphRepository;
            _starterPermissionRepository = starterPermissionRepository;
        }

        /// <summary>
        /// 创建审批流程
        /// </summary>
        public override async Task<ApprovalFlowDto> CreateAsync(CreateApprovalFlowDto input)
        {
            try
            {
                var approvalFlow = await _approvalFlowDomainService.CreateAsync(
                    input.Name,
                    input.Description,
                    input.Identity,
                    input.FormId,
                    input.GraphId);

                return await MapToGetOutputDtoAsync(approvalFlow);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 更新审批流程
        /// </summary>
        public override async Task<ApprovalFlowDto> UpdateAsync(Guid id, UpdateApprovalFlowDto input)
        {
            try
            {
                var approvalFlow = await Repository.GetAsync(id);
                var updatedApprovalFlow = await _approvalFlowDomainService.UpdateBasicInfoAsync(
                    approvalFlow,
                    input.Name,
                    input.Description,
                    input.Identity);

                return await MapToGetOutputDtoAsync(updatedApprovalFlow);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 删除审批流程
        /// </summary>
        public override async Task DeleteAsync(Guid id)
        {
            try
            {
                var approvalFlow = await Repository.GetAsync(id);
                var canDelete = await _approvalFlowDomainService.CanDeleteAsync(approvalFlow);

                if (!canDelete)
                {
                    throw new UserFriendlyException("存在正在运行的流程实例，无法删除该审批流程");
                }

                await Repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 获取审批流程详细信息
        /// </summary>
        public async Task<ApprovalFlowDetailDto> GetDetailAsync(Guid id)
        {
            try
            {
                var approvalFlow = await Repository.GetAsync(id);
                var detailDto = ObjectMapper.Map<ApprovalFlow, ApprovalFlowDetailDto>(approvalFlow);

                // 获取关联的表单信息
                if (approvalFlow.FormId != Guid.Empty)
                {
                    var form = await _formRepository.FirstOrDefaultAsync(x => x.Id == approvalFlow.FormId);
                    if (form != null)
                    {
                        detailDto.Form = ObjectMapper.Map<Form, FormDto>(form);
                    }
                }

                // 获取关联的流程图信息
                if (approvalFlow.GraphId != Guid.Empty)
                {
                    var graph = await _graphRepository.FirstOrDefaultAsync(x => x.Id == approvalFlow.GraphId);
                    if (graph != null)
                    {
                        detailDto.Graph = ObjectMapper.Map<Graph, GraphDto>(graph);
                    }
                }

                // 获取发起人权限
                var starterPermissions = await _approvalFlowDomainService.GetStarterPermissionsAsync(approvalFlow);
                detailDto.StarterPermissions = ObjectMapper.Map<ICollection<StarterPermissionReference>, List<StarterPermissionDto>>(starterPermissions);

                return detailDto;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 更新审批流程关联的表单
        /// </summary>
        public async Task<ApprovalFlowDto> UpdateFormAsync(Guid id, UpdateApprovalFlowFormDto input)
        {
            try
            {
                var approvalFlow = await Repository.GetAsync(id);
                var updatedApprovalFlow = await _approvalFlowDomainService.UpdateFormAsync(approvalFlow, input.FormId);

                return await MapToGetOutputDtoAsync(updatedApprovalFlow);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 更新审批流程关联的流程图
        /// </summary>
        public async Task<ApprovalFlowDto> UpdateGraphAsync(Guid id, UpdateApprovalFlowGraphDto input)
        {
            try
            {
                var approvalFlow = await Repository.GetAsync(id);
                var updatedApprovalFlow = await _approvalFlowDomainService.UpdateGraphAsync(approvalFlow, input.GraphId);

                return await MapToGetOutputDtoAsync(updatedApprovalFlow);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 验证审批流程定义是否完整有效
        /// </summary>
        public async Task<bool> ValidateAsync(Guid id)
        {
            try
            {
                var approvalFlow = await Repository.GetAsync(id);
                return await _approvalFlowDomainService.ValidateAsync(approvalFlow);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 获取审批流程的发起人权限
        /// </summary>
        public async Task<List<StarterPermissionDto>> GetStarterPermissionsAsync(Guid id)
        {
            try
            {
                var approvalFlow = await Repository.GetAsync(id);
                var starterPermissions = await _approvalFlowDomainService.GetStarterPermissionsAsync(approvalFlow);

                return ObjectMapper.Map<ICollection<StarterPermissionReference>, List<StarterPermissionDto>>(starterPermissions);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 添加审批流程的发起人权限
        /// </summary>
        public async Task<ApprovalFlowDto> AddStarterPermissionAsync(Guid id, StarterPermissionDto input)
        {
            try
            {
                var approvalFlow = await Repository.GetAsync(id);

                // 创建发起人权限
                var starterPermissionId = GuidGenerator.Create();
                var starterPermission = new StarterPermission(
                    starterPermissionId,
                    input.Description ?? input.PermissionValue);

                // 创建发起人权限引用
                var starterPermissionReferenceId = GuidGenerator.Create();
                var starterPermissionReference = new StarterPermissionReference(
                    starterPermissionReferenceId,
                    starterPermissionId);

                // 保存发起人权限
                await _starterPermissionRepository.InsertAsync(starterPermission, autoSave: true);

                // 通过领域服务处理关联关系
                await _approvalFlowDomainService.AddStarterPermissionAsync(approvalFlow, starterPermissionReference);

                return await MapToGetOutputDtoAsync(approvalFlow);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 移除审批流程的发起人权限
        /// </summary>
        public async Task<ApprovalFlowDto> RemoveStarterPermissionAsync(Guid id, Guid permissionId)
        {
            try
            {
                var approvalFlow = await Repository.GetAsync(id);

                // 通过领域服务处理移除关联关系
                await _approvalFlowDomainService.RemoveStarterPermissionAsync(approvalFlow, permissionId);

                return await MapToGetOutputDtoAsync(approvalFlow);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 获取可用的表单列表
        /// </summary>
        public async Task<List<FormDto>> GetAvailableFormsAsync()
        {
            try
            {
                var forms = await _formRepository.GetListAsync();
                return ObjectMapper.Map<List<Form>, List<FormDto>>(forms);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 获取可用的流程图列表
        /// </summary>
        public async Task<List<GraphDto>> GetAvailableGraphsAsync()
        {
            try
            {
                var graphs = await _graphRepository.GetListAsync();
                return ObjectMapper.Map<List<Graph>, List<GraphDto>>(graphs);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 获取审批流程列表
        /// </summary>
        protected override async Task<PagedResultDto<ApprovalFlowDto>> GetListAsync(GetApprovalFlowListDto input)
        {
            try
            {
                // 获取所有审批流程
                var approvalFlows = await Repository.GetListAsync();

                // 应用过滤条件
                var query = approvalFlows.AsQueryable();

                if (!string.IsNullOrWhiteSpace(input.Name))
                {
                    query = query.Where(x => x.BasicInfo.Name.Contains(input.Name));
                }

                if (!string.IsNullOrWhiteSpace(input.Identity))
                {
                    query = query.Where(x => x.BasicInfo.Identity.Contains(input.Identity));
                }

                if (input.FormId.HasValue)
                {
                    query = query.Where(x => x.FormId == input.FormId.Value);
                }

                if (input.GraphId.HasValue)
                {
                    query = query.Where(x => x.GraphId == input.GraphId.Value);
                }

                // 排序
                if (!string.IsNullOrWhiteSpace(input.Sorting))
                {
                    // 简单排序实现
                    if (input.Sorting.Contains("Name"))
                    {
                        query = input.Sorting.Contains("desc")
                            ? query.OrderByDescending(x => x.BasicInfo.Name)
                            : query.OrderBy(x => x.BasicInfo.Name);
                    }
                    else
                    {
                        query = query.OrderBy(x => x.BasicInfo.Name);
                    }
                }
                else
                {
                    query = query.OrderBy(x => x.BasicInfo.Name);
                }

                // 分页
                var totalCount = query.Count();
                var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

                return new PagedResultDto<ApprovalFlowDto>(
                    totalCount,
                    ObjectMapper.Map<List<ApprovalFlow>, List<ApprovalFlowDto>>(items));
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 映射到输出DTO
        /// </summary>
        protected override async Task<ApprovalFlowDto> MapToGetOutputDtoAsync(ApprovalFlow entity)
        {
            var dto = await base.MapToGetOutputDtoAsync(entity);

            // 设置基本信息
            dto.Name = entity.BasicInfo.Name;
            dto.Description = entity.BasicInfo.Description;
            dto.Identity = entity.BasicInfo.Identity;

            return dto;
        }
    }
}