using ApFlow.Domain.Models;
using ApFlow.Domain.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ApFlow.EntityFrameworkCore.Sqlserver.EntityConfigurations
{
    /// <summary>
    /// 表单字段实体配置
    /// </summary>
    public class FormFieldConfiguration : IEntityTypeConfiguration<FormField>
    {
        /// <summary>
        /// 配置FormField实体的数据库映射
        /// </summary>
        /// <param name="builder">实体类型构建器</param>
        public void Configure(EntityTypeBuilder<FormField> builder)
        {
            builder.ConfigureByConvention();
            // 设置表名
            builder.ToTable(ApFlowDbPrefix.TableNamePrefix + "FormFields");

            // 设置主键
            builder.HasKey(field => field.Id);

            // 配置属性
            builder.Property(field => field.Key).IsRequired().HasMaxLength(256);
            builder.Property(field => field.DisplayName).IsRequired().HasMaxLength(256);
        }
    }
}