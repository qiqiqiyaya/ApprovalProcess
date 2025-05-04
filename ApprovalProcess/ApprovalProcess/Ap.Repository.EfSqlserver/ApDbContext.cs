using Ap.Share.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ap.Repository.EfSqlserver
{
    public class ApDbContext : DbContext
    {
        public DbSet<EmployeeEntity> Employees { get; set; }

        public DbSet<OrganizationEntity> Organizations { get; set; }

        public DbSet<ManagerEntity> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeEntity>(model =>
            {
                model.ToTable("Ap_Employee").HasKey(s => s.Id);
                model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
            });

            modelBuilder.Entity<OrganizationEntity>(model =>
            {
                model.ToTable("Ap_Organization").HasKey(s => s.Id);
                model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
                model.Property(s => s.ParentId).HasColumnType("varchar(50)");

                model.HasMany(s => s.Managers).WithOne(s => s.OrganizationEntity).HasForeignKey(s => s.OrganizationId);
            });

            modelBuilder.Entity<ManagerEntity>(model =>
            {
                model.ToTable("Ap_Manager").HasKey(s => s.Id);
                model.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
                model.Property(s => s.EmployeeId).HasColumnType("varchar(50)").IsRequired();
                model.Property(s => s.OrganizationId).HasColumnType("varchar(50)").IsRequired();

                model.HasOne(s => s.OrganizationEntity).WithMany(s => s.Managers).HasForeignKey(s => s.OrganizationId);
            });
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //	optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ApDb;User Id=sa;Password=123;");
        //}
    }
}
