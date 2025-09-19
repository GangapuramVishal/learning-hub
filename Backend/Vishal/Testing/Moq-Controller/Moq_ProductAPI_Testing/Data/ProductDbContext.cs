using Microsoft.EntityFrameworkCore;
using Moq_ProductAPI_Testing.Models;

namespace Moq_ProductAPI_Testing.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Product> MoqProductsForTestig {  get; set; }
    }
}
