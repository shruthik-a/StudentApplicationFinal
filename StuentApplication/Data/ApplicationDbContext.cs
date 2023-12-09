// ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using StuentApplication.Model;

namespace StuentApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configure one-to-one relationship
        //    modelBuilder.Entity<Student>()
        //        .HasOne(s => s.StudentAddress)
        //        .WithOne(sa => sa.Student)
        //        .HasForeignKey<StudentAddress>(sa => sa.StudentId);
        //}
    }
}
