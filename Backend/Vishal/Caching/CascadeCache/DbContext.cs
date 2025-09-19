using CascadeCache.Models;
using Microsoft.EntityFrameworkCore;

namespace CascadeCache
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1001,
                    FirstName = "G",
                    LastName = "Shankar",
                    Position = "Junior developer",
                    Department = "IT",
                    Salary = 100,
                    DateOfBirth = new DateOnly(1998, 5, 10)
                },
                new Employee
                {
                    Id = 1002,
                    FirstName = "G",
                    LastName = "Vishal",
                    Position = "Junior developer",
                    Department = "IT",
                    Salary = 100,
                    DateOfBirth = new DateOnly(1995, 5, 10)
                },
                new Employee
                {
                    Id = 1003,
                    FirstName = "G",
                    LastName = "Sreyanth",
                    Position = "Junior developer",
                    Department = "IT",
                    Salary = 100,
                    DateOfBirth = new DateOnly(2000, 5, 10)
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
