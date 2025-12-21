using ApFlow.Domain.Models;
using ApFlow.Domain.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ApFlow.EntityFrameworkCore.Sqlserver.EntityConfigurations
{
    /// <summary>
    /// 表单字段值配置类
    /// </summary>
    public class FormFieldValueConfiguration : IEntityTypeConfiguration<FormFieldValue>
    {
        /// <summary>
        /// 配置FormFieldValue实体的数据库映射
        /// </summary>
        /// <param name="builder">实体类型构建器</param>
        public void Configure(EntityTypeBuilder<FormFieldValue> builder)
        {
            builder.ConfigureByConvention();
            // 设置表名
            builder.ToTable(ApFlowDbPrefix.TableNamePrefix + "FormFieldValues");

            // 设置主键
            builder.HasKey(value => value.Id);

            // 配置属性
            builder.Property(value => value.Type).IsRequired().HasMaxLength(50);
            builder.Property(value => value.Value).IsRequired().HasMaxLength(500);
            builder.Property(value => value.FormFieldId).IsRequired().HasMaxLength(100);

            // 配置导航属性
            builder.HasOne(value => value.FormField)
                .WithMany()
                .HasForeignKey(value => value.FormFieldId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}