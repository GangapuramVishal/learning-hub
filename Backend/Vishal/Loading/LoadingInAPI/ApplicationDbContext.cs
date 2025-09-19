using LoadingInAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoadingInAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Sample Customers
            var customers = new List<Customer>
    {
        new Customer { CustomerId = 1, Name = "John Doe" },
        new Customer { CustomerId = 2, Name = "Jane Smith" }
    };

            // Sample Orders
            var orders = new List<Order>
    {
        new Order { OrderId = 1, OrderNumber = "ORD001", CustomerId = 1 },
        new Order { OrderId = 2, OrderNumber = "ORD002", CustomerId = 1 },
        new Order { OrderId = 3, OrderNumber = "ORD003", CustomerId = 2 }
    };

            modelBuilder.Entity<Customer>().HasData(customers);
            modelBuilder.Entity<Order>().HasData(orders);
        }
    }
}
