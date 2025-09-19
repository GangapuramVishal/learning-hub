using Microsoft.EntityFrameworkCore;
using Moq_ProductAPI_Testing.Data;
using Moq_ProductAPI_Testing.Models;

namespace Moq_ProductAPI_Testing.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _dbContext;
        public ProductService(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetProductById(int productId)
        {
            var product = await _dbContext.MoqProductsForTestig.Where(p=>p.ProductId == productId).Select(n=>n.ProductName).FirstOrDefaultAsync();
            return product;
        }

        public async Task<Product> GetProductDetails(int productId)
        {
            var allProducts = await _dbContext.MoqProductsForTestig.FirstOrDefaultAsync(a=>a.ProductId == productId);
            return allProducts;
        }

        public async Task<int> CreateProduct(CreateProductModel model)
        {
            var product = new Product
            {
                ProductName = model.ProductName,
                ProductCategory = model.ProductCategory
            };

            _dbContext.MoqProductsForTestig.Add(product);
            await _dbContext.SaveChangesAsync();

            return product.ProductId;
        }
    }
}
