using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ProductAPI.Data;
using ProductAPI.Model;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductAPIContext _context;
        private readonly IDistributedCache _cache;
        public ProductsController(ProductAPIContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            //var cacheKey = "products";
            // Check if the data is in the cache
            var cachedProducts = await _cache.GetStringAsync("cacheKey");

            if (cachedProducts != null)
            {
                // If cached data is available, set a custom response header
                Response.Headers.Add("X-Cache", "Data retrived from cache");

                // If cached data is available, return it
                var products = JsonConvert.DeserializeObject<List<Product>>(cachedProducts);
                return Ok(products);
            }
            else
            {
                // If data is not cached, fetch it from the database
                var products = await _context.Product.ToListAsync();
                // Serialize the data and store it in the cache with absolute and sliding expirations
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30), // Absolute expiration time (30 sec from now)
                    SlidingExpiration = TimeSpan.FromSeconds(15) // Sliding expiration (15 sec)
                };
                // Serialize the data and store it in the cache
                var serializedProducts = JsonConvert.SerializeObject(products);
                await _cache.SetStringAsync("cacheKey", serializedProducts,options);

                // Set a custom response header indicating that the data is not from cache
                Response.Headers.Add("X-Cache", "data retrived from database");

                return Ok(products);
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
