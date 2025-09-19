using Microsoft.EntityFrameworkCore;
using StudentWebAPI.Models;

namespace StudentWebAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StudentEntities> StudentRegistration { get; set; }
    }
}
