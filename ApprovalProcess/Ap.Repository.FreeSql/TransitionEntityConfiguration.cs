using Ap.Core.Share.Entities;
using FreeSql.Extensions.EfCoreFluentApi;

namespace Ap.Repository.FreeSql
{
    public class TransitionEntityConfiguration : IEntityTypeConfiguration<TransitionEntity>
    {
        public void Configure(EfCoreTableFluent<TransitionEntity> model)
        {
            model.ToTable("Ap_Transition").HasKey(s => s.Id);

            model.Property(s => s.Id).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.Trigger).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.DtState).HasColumnType("TEXT").IsRequired();
            model.Property(s => s.StateSettingsId).HasColumnType("TEXT").IsRequired();

            model.HasOne(s => s.StateSettings).HasForeignKey(s => s.StateSettingsId);
        }
    }
}
