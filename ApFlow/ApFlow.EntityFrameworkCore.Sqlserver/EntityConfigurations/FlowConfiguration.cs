using ApFlow.Domain.Models;
using ApFlow.Domain.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ApFlow.EntityFrameworkCore.Sqlserver.EntityConfigurations
{
    /// <summary>
    /// 流程连线实体配置
    /// </summary>
    public class FlowConfiguration : IEntityTypeConfiguration<Flow>
    {
        /// <summary>
        /// 配置Flow实体的数据库映射
        /// </summary>
        /// <param name="builder">实体类型构建器</param>
        public void Configure(EntityTypeBuilder<Flow> builder)
        {
            builder.ConfigureByConvention();
            // 设置表名
            builder.ToTable(ApFlowDbPrefix.TableNamePrefix + "Flows");

            // 设置主键
            builder.HasKey(flow => flow.Id);

            // 配置属性
            builder.Property(flow => flow.Direction).IsRequired();
            builder.Property(flow => flow.Condition).IsRequired();
            builder.Property(flow => flow.SourceNodeId).IsRequired();
            builder.Property(flow => flow.TargetNodeId).IsRequired();
            builder.Property(flow => flow.GraphId).IsRequired();

            // 配置导航属性
            builder.HasOne(flow => flow.Graph)
                .WithMany(graph => graph.Flows)
                .HasForeignKey(flow => flow.GraphId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // 配置源节点和目标节点的关系
            builder.HasOne<Node>()
                .WithMany(node => node.OutFlows)
                .HasForeignKey(flow => flow.SourceNodeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Node>()
                .WithMany(node => node.InFlows)
                .HasForeignKey(flow => flow.TargetNodeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}