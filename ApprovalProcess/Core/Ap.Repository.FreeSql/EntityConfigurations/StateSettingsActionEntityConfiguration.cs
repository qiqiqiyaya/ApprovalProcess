using Ap.Core.Share.Entities;
using FreeSql.Extensions.EfCoreFluentApi;

namespace Ap.Repository.FreeSql.EntityConfigurations
{
    public class StateSettingsActionEntityConfiguration : IEntityTypeConfiguration<StateSettingsActionEntity>
    {
        public void Configure(EfCoreTableFluent<StateSettingsActionEntity> model)
        {
            model.ToTable("Ap_StateSettingsAction").HasKey(s => s.Id);

            model.Property(s => s.Id).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.StateSettingsId).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.ExecutableActionId).HasColumnType("TEXT").IsRequired();

            model.HasOne(s => s.StateSettings).HasForeignKey(s => s.StateSettingsId);
        }
    }
}
