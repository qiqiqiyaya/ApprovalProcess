using Microsoft.EntityFrameworkCore;
using Sm.Share.Entities;

namespace Sm.Repository.EfSqlserver
{
    public class SmDbContext : DbContext
    {
        public SmDbContext()
        {
        }

        public SmDbContext(DbContextOptions<SmDbContext> options)
            : base(options)
        {

        }

        public DbSet<ExecutableActionEntity> ExecutableActions { get; set; }

        public DbSet<StateMachineEntity> StateMachines { get; set; }

        public DbSet<StateSettingsActionEntity> StateSettingsActions { get; set; }

        public DbSet<StateSettingsEntity> StateSettings { get; set; }

        public DbSet<TransitionEntity> Transitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StateMachineEntity>(model =>
            {
                model.ToTable("Sm_StateMachine").HasKey(s => s.Id);
                model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
                model.Property(s => s.InitialState).HasColumnType("nvarchar(100)").IsRequired();
                //model.Property(s => s.CurrentState).HasColumnType("nvarchar(100)").IsRequired();

                model.HasMany(s => s.StateSettings).WithOne(s => s.StateMachine).HasForeignKey(s => s.StateMachineId);
            });

            modelBuilder.Entity<StateSettingsEntity>(model =>
            {
                model.ToTable("Sm_StateSettings").HasKey(s => s.Id);
                model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
                model.Property(s => s.State).HasColumnType("nvarchar(100)").IsRequired();
                model.Property(s => s.StateMachineId).HasColumnType("varchar(50)").IsRequired();

                model.HasOne(s => s.StateMachine).WithMany(s => s.StateSettings).HasForeignKey(s => s.StateMachineId);
                model.HasMany(s => s.Transitions).WithOne(s => s.StateSettings).HasForeignKey(s => s.StateSettingsId);
                model.HasMany(s => s.Actions).WithOne(s => s.StateSettings).HasForeignKey(s => s.StateSettingsId);
            });

            modelBuilder.Entity<TransitionEntity>(model =>
            {
                model.ToTable("Sm_Transition").HasKey(s => s.Id);

                model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
                model.Property(s => s.Trigger).HasColumnType("nvarchar(100)").IsRequired();
                model.Property(s => s.DtState).HasColumnType("nvarchar(100)").IsRequired();
                model.Property(s => s.StateSettingsId).HasColumnType("varchar(50)").IsRequired();

                model.HasOne(s => s.StateSettings).WithMany(s => s.Transitions).HasForeignKey(s => s.StateSettingsId);
            });

            modelBuilder.Entity<StateSettingsActionEntity>(model =>
            {
                model.ToTable("Sm_StateSettingsAction").HasKey(s => s.Id);

                model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
                model.Property(s => s.StateSettingsId).HasColumnType("varchar(50)").IsRequired();
                model.Property(s => s.ExecutableActionId).HasColumnType("varchar(50)").IsRequired();
                model.Property(s => s.Configuration).HasColumnType("nvarchar(max)");
                model.Property(s => s.ConfigurationType).HasColumnType("nvarchar(200)");

                model.HasOne(s => s.StateSettings).WithMany(s => s.Actions).HasForeignKey(s => s.StateSettingsId);
            });

            modelBuilder.Entity<ExecutableActionEntity>(model =>
            {
                model.ToTable("Sm_ExecutableAction").HasKey(s => s.Id);
                model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
                model.Property(s => s.Name).HasColumnType("nvarchar(100)").IsRequired();
                model.Property(s => s.Description).HasColumnType("nvarchar(100)").IsRequired();
                model.Property(s => s.ActionType).HasColumnType("nvarchar(200)").IsRequired();
                model.Property(s => s.EventType).HasColumnType("int").IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ApDb;User Id=sa;Password=123;");
        }
    }
}
