using FreeSql.Extensions.EfCoreFluentApi;
using Sm.Share.Entities;

namespace Sm.Repository.FreeSql.EntityConfigurations
{
    public class StateSettingsEntityConfiguration : IEntityTypeConfiguration<StateSettingsEntity>
    {
        public void Configure(EfCoreTableFluent<StateSettingsEntity> model)
        {
            model.ToTable("Ap_StateSettings").HasKey(s => s.Id);

            model.Property(s => s.Id).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.State).HasColumnType("TEXT").IsRequired();

            model.Property(s => s.StateMachineId).HasColumnType("TEXT").IsRequired();
            model.HasOne(s => s.StateMachine).HasForeignKey(s => s.StateMachineId);

            model.HasMany(s => s.Transitions).HasForeignKey(s => s.StateSettingsId);
            model.HasMany(s => s.Actions).HasForeignKey(s => s.StateSettingsId);
        }
    }
}
