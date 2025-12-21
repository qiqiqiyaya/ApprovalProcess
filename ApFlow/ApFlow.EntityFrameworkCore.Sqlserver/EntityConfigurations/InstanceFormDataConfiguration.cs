using ApFlow.Domain.Models;
using ApFlow.Domain.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ApFlow.EntityFrameworkCore.Sqlserver.EntityConfigurations
{
    /// <summary>
    /// 实例表单数据实体配置
    /// </summary>
    public class InstanceFormDataConfiguration : IEntityTypeConfiguration<InstanceFormData>
    {
        /// <summary>
        /// 配置InstanceFormData实体的数据库映射
        /// </summary>
        /// <param name="builder">实体类型构建器</param>
        public void Configure(EntityTypeBuilder<InstanceFormData> builder)
        {
            builder.ConfigureByConvention();
            // 设置表名（注意：InstanceFormData是复杂类型，通常作为ApprovalFlowInstance的一部分存储）
            builder.ToTable(ApFlowDbPrefix.TableNamePrefix + "InstanceFormDatas");

            // 设置主键
            builder.HasKey(formData => formData.Id);

            // 配置属性
            builder.Property(formData => formData.FormId).IsRequired();

            // 配置导航属性
            builder.HasOne<Form>()
                .WithMany()
                .HasForeignKey(formData => formData.FormId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(formData => formData.FieldValues)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}