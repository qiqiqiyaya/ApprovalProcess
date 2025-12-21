using ApFlow.Domain.Models;
using ApFlow.Domain.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ApFlow.EntityFrameworkCore.Sqlserver.EntityConfigurations
{
    /// <summary>
    /// 审批流程实例实体配置
    /// </summary>
    public class ApprovalFlowInstanceConfiguration : IEntityTypeConfiguration<ApprovalFlowInstance>
    {
        /// <summary>
        /// 配置ApprovalFlowInstance实体的数据库映射
        /// </summary>
        /// <param name="builder">实体类型构建器</param>
        public void Configure(EntityTypeBuilder<ApprovalFlowInstance> builder)
        {
            builder.ConfigureByConvention();
            // 设置表名
            builder.ToTable(ApFlowDbPrefix.TableNamePrefix + "ApprovalFlowInstances");

            // 设置主键
            builder.HasKey(instance => instance.Id);

            // 配置属性
            builder.Property(instance => instance.Starter).IsRequired().HasMaxLength(256);
            builder.Property(instance => instance.FormDataId).IsRequired();
            builder.Property(instance => instance.ApprovalFlowId).IsRequired();
            builder.Property(instance => instance.Status).IsRequired();
            builder.Property(instance => instance.CurrentNodeId).IsRequired();
            builder.Property(instance => instance.CreationTime).IsRequired();
            builder.Property(instance => instance.IsDeleted).IsRequired();

            // 配置导航属性
            builder.HasOne(instance => instance.FormData)
                .WithOne()
                .HasForeignKey<ApprovalFlowInstance>(instance => instance.FormDataId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // 配置导航属性
            builder.HasOne<ApprovalFlow>()
                .WithMany()
                .HasForeignKey(instance => instance.ApprovalFlowId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Node>()
                .WithMany()
                .HasForeignKey(instance => instance.CurrentNodeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(instance => instance.Records)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}