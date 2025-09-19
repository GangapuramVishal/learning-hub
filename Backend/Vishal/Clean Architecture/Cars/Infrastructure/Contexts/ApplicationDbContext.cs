using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        }

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Image> Images => Set<Image>();   //It's an alternative way of defining a property getter
    }
}
