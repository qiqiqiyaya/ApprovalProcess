using Ap.Core.Share.Entities;
using FreeSql.Extensions.EfCoreFluentApi;

namespace Ap.Repository.FreeSql
{
    public class ExecutableActionEntityConfiguration : IEntityTypeConfiguration<ExecutableActionEntity>
    {
        public void Configure(EfCoreTableFluent<ExecutableActionEntity> model)
        {
            model.ToTable("Ap_ExecutableAction").HasKey(s => s.Id);
            model.Property(s => s.Id).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.Name).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.Description).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.Type).HasColumnType("INTEGER").IsRequired();
        }
    }
}
