using AutoMapper;
using ApFlow.Application.Contracts.ApprovalFlows.Dtos;
using ApFlow.Domain.Models;

namespace ApFlow.Application
{
    /// <summary>
    /// AutoMapper配置文件，定义实体与DTO之间的映射关系
    /// </summary>
    public class ApFlowApplicationAutoMapperProfile : Profile
    {
        public ApFlowApplicationAutoMapperProfile()
        {
            // 审批流程映射
            CreateMap<ApprovalFlow, ApprovalFlowDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BasicInfo.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.BasicInfo.Description))
                .ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.BasicInfo.Identity));

            CreateMap<ApprovalFlow, ApprovalFlowDetailDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BasicInfo.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.BasicInfo.Description))
                .ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.BasicInfo.Identity));

            // 发起人权限映射
            CreateMap<StarterPermissionReference, StarterPermissionDto>()
                .ForMember(dest => dest.PermissionType, opt => opt.MapFrom(src => "Reference")) // 默认值
                .ForMember(dest => dest.PermissionValue, opt => opt.MapFrom(src => src.StarterPermissionId.ToString()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.StarterPermission != null ? src.StarterPermission.Name : ""));

            CreateMap<StarterPermission, StarterPermissionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PermissionType, opt => opt.MapFrom(src => "Permission"))
                .ForMember(dest => dest.PermissionValue, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Name));

            // 表单映射
            CreateMap<Form, FormDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BasicInfo.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.BasicInfo.Description));

            // 流程图映射
            CreateMap<Graph, GraphDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BasicInfo.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.BasicInfo.Description));
        }
    }
}