using Ap.Core.Share.Entities;
using FreeSql.Extensions.EfCoreFluentApi;

namespace Ap.Repository.FreeSql
{
    public class StateSettingsEntityConfiguration : IEntityTypeConfiguration<StateSettingsEntity>
    {
        public void Configure(EfCoreTableFluent<StateSettingsEntity> model)
        {
            model.ToTable("Ap_StateSettings").HasKey(s => s.Id);

            model.Property(s => s.Id).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.State).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.ActionListJson).HasColumnType("TEXT").IsRequired();

            model.Property(s => s.StateMachineId).HasColumnType("TEXT").IsRequired();
            model.HasOne(s => s.StateMachine).HasForeignKey(s => s.StateMachineId);

            model.HasMany(s => s.Transitions).HasForeignKey(s => s.StateSettingsId);
        }
    }
}
