using ApprovalProcess.Share.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApprovalProcess.Repository
{
    public class ApprovalProcessDbContext : DbContext
    {
        public ApprovalProcessDbContext(DbContextOptions<ApprovalProcessDbContext> options)
            : base(options)
        {

        }

        public DbSet<EmployeeEntity> Employees { get; set; }

        public DbSet<ManagerEntity> Managers { get; set; }

        public DbSet<OrganizationEntity> Organizations { get; set; }

        public DbSet<PeEntity> Pes { get; set; }

        public DbSet<WorkflowEntity> Workflows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrganizationEntity>().HasKey(s => s.Id);
            modelBuilder.Entity<ManagerEntity>().HasKey(s => s.Id);
            modelBuilder.Entity<EmployeeEntity>().HasKey(s => s.Id);
            modelBuilder.Entity<PeEntity>().HasKey(s => s.Id);
            modelBuilder.Entity<WorkflowEntity>().HasKey(s => s.Id);

            modelBuilder.Entity<OrganizationEntity>()
                .HasMany<ManagerEntity>(s => s.Managers)
                .WithOne(s => s.Organization)
                .HasForeignKey(s => s.OrganizationId)
                .IsRequired();
        }
    }
}
