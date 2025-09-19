using Microsoft.EntityFrameworkCore;
using TalentTrack_DAL.Entities;

namespace TalentTrack_DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.EmployeeID)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PersonalEmail)
                .IsUnique();

            // Configure foreign key relationship for ManagerID
            modelBuilder.Entity<User>()
                .HasOne(u => u.Manager)  // Using the Manager navigation property here
                .WithMany()  // A user can have many reports, but no navigation property for Manager in `User` entity
                .HasForeignKey(u => u.ManagerID)
                .OnDelete(DeleteBehavior.SetNull);  // Delete the reference but not the user if the manager is deleted

        }
    }
}
