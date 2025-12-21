using ApFlow.Domain.Models;
using ApFlow.Domain.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ApFlow.EntityFrameworkCore.Sqlserver.EntityConfigurations
{
    /// <summary>
    /// 审批流程实体配置
    /// </summary>
    public class ApprovalFlowConfiguration : IEntityTypeConfiguration<ApprovalFlow>
    {
        /// <summary>
        /// 配置ApprovalFlow实体的数据库映射
        /// </summary>
        /// <param name="builder">实体类型构建器</param>
        public void Configure(EntityTypeBuilder<ApprovalFlow> builder)
        {
            builder.ConfigureByConvention();

            // 设置表名
            builder.ToTable(ApFlowDbPrefix.TableNamePrefix + "ApprovalFlows");

            // 设置主键
            builder.HasKey(flow => flow.Id);

            // 配置属性
            builder.Property(flow => flow.CreationTime).IsRequired();
            builder.Property(flow => flow.IsDeleted).IsRequired();

            // 配置导航属性
            builder.HasOne<Graph>()
                .WithMany()
                .HasForeignKey(flow => flow.GraphId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Form>()
                .WithMany()
                .HasForeignKey(flow => flow.FormId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}