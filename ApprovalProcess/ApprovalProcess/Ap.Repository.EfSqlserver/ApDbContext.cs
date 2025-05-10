using Ap.Share.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ap.Repository.EfSqlserver
{
	public class ApDbContext : DbContext
	{
		public ApDbContext()
		{

		}
		public ApDbContext(DbContextOptions<ApDbContext> options)
			: base(options)
		{

		}

		public DbSet<UserEntity> Users { get; set; }

		public DbSet<OrganizationEntity> Organizations { get; set; }

		public DbSet<ManagerEntity> Managers { get; set; }

		public DbSet<TriggeredRecordEntity> TriggeredRecords { get; set; }

		public DbSet<NextApproverEntity> NextApprovers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserEntity>(model =>
			{
				model.ToTable("Ap_User").HasKey(s => s.Id);
				model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
				model.Property(s => s.Name).HasColumnType("nvarchar(200)").IsRequired();
			});

			modelBuilder.Entity<OrganizationEntity>(model =>
			{
				model.ToTable("Ap_Organization").HasKey(s => s.Id);
				model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
				model.Property(s => s.ParentId).HasColumnType("varchar(50)");

				model.HasMany(s => s.Managers).WithOne(s => s.Organization).HasForeignKey(s => s.OrganizationId);
			});

			modelBuilder.Entity<ManagerEntity>(model =>
			{
				model.ToTable("Ap_Manager").HasKey(s => s.Id);
				model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
				model.Property(s => s.EmployeeId).HasColumnType("varchar(50)").IsRequired();
				model.Property(s => s.OrganizationId).HasColumnType("varchar(50)").IsRequired();

				model.HasOne(s => s.Organization).WithMany(s => s.Managers).HasForeignKey(s => s.OrganizationId);
			});

			modelBuilder.Entity<TriggeredRecordEntity>(model =>
			{
				model.ToTable("Ap_TriggeredRecord").HasKey(s => s.Id);
				model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
				model.Property(s => s.StateMachineId).HasColumnType("varchar(50)").IsRequired();
				model.Property(s => s.CurrentState).HasColumnType("nvarchar(100)").IsRequired();
			});

			modelBuilder.Entity<NextApproverEntity>(model =>
			{
				model.ToTable("Ap_NextApprover").HasKey(s => s.Id);
				model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
				model.Property(s => s.ApproverId).HasColumnType("varchar(50)").IsRequired();
				model.Property(s => s.Definition).HasColumnType("nvarchar(100)").IsRequired();
				model.Property(s => s.TriggeredRecordId).HasColumnType("varchar(50)").IsRequired();
			});

			modelBuilder.Entity<TriggeredRecordEntity>(model =>
			{
				model.ToTable("Ap_TriggeredRecord").HasKey(s => s.Id);
				model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
				model.Property(s => s.StateMachineId).HasColumnType("varchar(50)").IsRequired();
				model.Property(s => s.SourceState).HasColumnType("nvarchar(100)").IsRequired();
				model.Property(s => s.DtState).HasColumnType("nvarchar(100)").IsRequired();
				model.Property(s => s.Trigger).HasColumnType("nvarchar(100)").IsRequired();
				model.Property(s => s.CurrentState).HasColumnType("nvarchar(100)").IsRequired();
				model.Property(s => s.CreateTime).HasColumnType("datetime").IsRequired();
			});
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ApDb;User Id=sa;Password=123;");
		}
	}
}
