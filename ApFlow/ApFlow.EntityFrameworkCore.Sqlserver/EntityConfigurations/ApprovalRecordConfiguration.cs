using ApFlow.Domain.Models;
using ApFlow.Domain.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ApFlow.EntityFrameworkCore.Sqlserver.EntityConfigurations
{
    /// <summary>
    /// 审批记录实体配置
    /// </summary>
    public class ApprovalRecordConfiguration : IEntityTypeConfiguration<ApprovalRecord>
    {
        /// <summary>
        /// 配置ApprovalRecord实体的数据库映射
        /// </summary>
        /// <param name="builder">实体类型构建器</param>
        public void Configure(EntityTypeBuilder<ApprovalRecord> builder)
        {
            builder.ConfigureByConvention();
            // 设置表名
            builder.ToTable(ApFlowDbPrefix.TableNamePrefix + "ApprovalRecords");

            // 设置主键
            builder.HasKey(record => record.Id);

            // 配置属性
            builder.Property(record => record.Approver).IsRequired().HasMaxLength(256);
            builder.Property(record => record.Comment).HasMaxLength(1024);
            builder.Property(record => record.ApprovalTime).IsRequired();
            builder.Property(record => record.NodeActionId).IsRequired();
            builder.Property(record => record.Status).IsRequired();
            builder.Property(record => record.NodeDefinitionId).IsRequired().HasMaxLength(256);

            // 配置导航属性
            builder.HasOne(record => record.NodeAction)
                .WithOne()
                .HasForeignKey<ApprovalRecord>(record => record.NodeActionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}