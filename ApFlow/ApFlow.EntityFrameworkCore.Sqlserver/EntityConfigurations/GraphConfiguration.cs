using ApFlow.Domain.Models;
using ApFlow.Domain.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ApFlow.EntityFrameworkCore.Sqlserver.EntityConfigurations
{
    /// <summary>
    /// 审批流程图实体配置
    /// </summary>
    public class GraphConfiguration : IEntityTypeConfiguration<Graph>
    {
        /// <summary>
        /// 配置Graph实体的数据库映射
        /// </summary>
        /// <param name="builder">实体类型构建器</param>
        public void Configure(EntityTypeBuilder<Graph> builder)
        {
            builder.ConfigureByConvention();
            // 设置表名
            builder.ToTable(ApFlowDbPrefix.TableNamePrefix + "Graphs");

            // 设置主键
            builder.HasKey(graph => graph.Id);

            // 配置属性
            builder.Property(graph => graph.CreationTime).IsRequired();
            builder.Property(graph => graph.IsDeleted).IsRequired();

            // 配置导航属性
            builder.HasMany(graph => graph.Nodes)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(graph => graph.Flows)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}