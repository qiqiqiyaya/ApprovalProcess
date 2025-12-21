using ApFlow.Domain.Models;
using ApFlow.Domain.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ApFlow.EntityFrameworkCore.Sqlserver.EntityConfigurations
{
    /// <summary>
    /// 表单实体配置
    /// </summary>
    public class FormConfiguration : IEntityTypeConfiguration<Form>
    {
        /// <summary>
        /// 配置Form实体的数据库映射
        /// </summary>
        /// <param name="builder">实体类型构建器</param>
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.ConfigureByConvention();
            // 设置表名
            builder.ToTable(ApFlowDbPrefix.TableNamePrefix + "Forms");

            // 设置主键
            builder.HasKey(form => form.Id);

            // 配置属性
            builder.Property(form => form.Data).IsRequired();
            builder.Property(form => form.CreationTime).IsRequired();
            builder.Property(form => form.IsDeleted).IsRequired();

            // 配置导航属性
            builder.HasMany(form => form.Fields)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}