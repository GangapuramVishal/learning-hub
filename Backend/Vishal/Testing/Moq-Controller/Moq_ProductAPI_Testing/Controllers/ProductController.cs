using Microsoft.AspNetCore.Mvc;
using Moq_ProductAPI_Testing.Services;
using Moq_ProductAPI_Testing.Models;

namespace Moq_ProductAPI_Testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController (IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetProductById")]
        public async Task<string> GetProductById(int productId)
        {
            var result = await _productService.GetProductById(productId);
            return result;
        }

        [HttpGet(nameof(GetProductDetails))]
        public async Task<Product> GetProductDetails(int productId)
        {
            var result = await _productService.GetProductDetails(productId);
            return result;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var productId = await _productService.CreateProduct(model);
                return Ok(productId);
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }
    }
}
