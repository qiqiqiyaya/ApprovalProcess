using ApFlow.Domain.Models;
using ApFlow.Domain.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ApFlow.EntityFrameworkCore.Sqlserver.EntityConfigurations
{
    /// <summary>
    /// 审批节点实体配置
    /// </summary>
    public class NodeConfiguration : IEntityTypeConfiguration<Node>
    {
        /// <summary>
        /// 配置Node实体的数据库映射
        /// </summary>
        /// <param name="builder">实体类型构建器</param>
        public void Configure(EntityTypeBuilder<Node> builder)
        {
            builder.ConfigureByConvention();
            // 设置表名
            builder.ToTable(ApFlowDbPrefix.TableNamePrefix + "Nodes");

            // 设置主键
            builder.HasKey(node => node.Id);

            // 配置属性
            builder.Property(node => node.NodeType).IsRequired();

            // 配置Approvers复杂类型
            builder.OwnsOne(node => node.Approvers, approvers =>
            {
                approvers.Property(a => a.Type).IsRequired();
                approvers.Property(a => a.EmptyApproverHandling).IsRequired();
                approvers.Property(a => a.ApprovalMode).IsRequired();
            });

            // 配置导航属性
            builder.HasMany(node => node.Actions)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(node => node.FormPermissions)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // 配置入边和出边的导航属性
            builder.HasMany(node => node.InFlows)
                .WithOne()
                .HasForeignKey(flow => flow.TargetNodeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(node => node.OutFlows)
                .WithOne()
                .HasForeignKey(flow => flow.SourceNodeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}