using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPIForSpecflowTesting.Models;

namespace ProductAPIForSpecflowTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 10.00M },
            new Product { Id = 2, Name = "Product2", Price = 20.00M }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProduct()
        {
            return Ok(products);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            product.Id = products.Count +1;
            products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
    }
}
