using FreeSql.Extensions.EfCoreFluentApi;
using Sm.Share.Entities;

namespace Sm.Repository.FreeSql.EntityConfigurations
{
    public class ExecutableActionEntityConfiguration : IEntityTypeConfiguration<ExecutableActionEntity>
    {
        public void Configure(EfCoreTableFluent<ExecutableActionEntity> model)
        {
            model.ToTable("Ap_ExecutableAction").HasKey(s => s.Id);
            model.Property(s => s.Id).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.Name).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.Description).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.EventType).HasColumnType("INTEGER").IsRequired();
        }
    }
}
