using Moq_ProductAPI_Testing.Models;

namespace Moq_ProductAPI_Testing.Services
{
    public interface IProductService
    {
        Task<string> GetProductById(int  productId);

        Task<Product> GetProductDetails(int productId);

        Task<int> CreateProduct(CreateProductModel model);
    }
}
