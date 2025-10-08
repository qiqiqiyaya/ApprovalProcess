using Microsoft.EntityFrameworkCore;
using OAApproval.Models;

namespace OAApproval.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApprovalTemplate> ApprovalTemplates { get; set; }
        public DbSet<ApprovalInstance> ApprovalInstances { get; set; }
        public DbSet<ApprovalTask> ApprovalTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApprovalInstance>()
                .HasOne(i => i.Template)
                .WithMany()
                .HasForeignKey(i => i.TemplateId);

            modelBuilder.Entity<ApprovalTask>()
                .HasOne(t => t.Instance)
                .WithMany(i => i.Tasks)
                .HasForeignKey(t => t.InstanceId);
        }
    }
}