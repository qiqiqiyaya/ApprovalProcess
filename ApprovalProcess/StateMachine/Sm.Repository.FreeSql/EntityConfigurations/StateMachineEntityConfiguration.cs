using FreeSql.Extensions.EfCoreFluentApi;
using Sm.Share.Entities;

namespace Sm.Repository.FreeSql.EntityConfigurations
{
    public class StateMachineEntityConfiguration : IEntityTypeConfiguration<StateMachineEntity>
    {
        public void Configure(EfCoreTableFluent<StateMachineEntity> model)
        {
            model.ToTable("Ap_StateMachine").HasKey(s => s.Id);

            model.Property(s => s.Id).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.InitialState).HasColumnType("TEXT").IsRequired();
            //model.Property(s => s.CurrentState).HasColumnType("TEXT").IsRequired();

            model.HasMany(s => s.StateSettings)
                .WithOne(s => s.StateMachine).HasForeignKey(s => s.StateMachineId);
        }
    }
}
