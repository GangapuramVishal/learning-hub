using LoadingInAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LoadingInAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoadingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoadingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("eager")]
        public IActionResult GetCustomersWithOrdersEager()
        {
            var customersWithOrders = _context.Customers.Include(c => c.Orders).ToList();
            return Ok(customersWithOrders);
        }

        [HttpGet("lazy")]
        public IActionResult GetCustomersLazy()
        {
            List<Customer> customers = _context.Customers.ToList();
            foreach(var customer in customers)
            {
                customer.Orders = _context.Orders.Where( c => c.CustomerId == customer.CustomerId ).ToList();
            }
            return Ok(customers);
            //var customers = _context.Customers.ToList();
            //// Accessing Orders navigation property of each customer will trigger lazy loading
            //foreach (var customer in customers)
            //{
            //    var orders = customer.Orders; // This will trigger lazy loading
            //                                  // Now orders for each customer are loaded (if not loaded already)
            //                                  // You can access and work with orders here
            //}

            //return Ok(customers);
        }

        [HttpGet("explicit")]
        public IActionResult GetCustomersWithOrdersExplicit()
        {
            var customers = _context.Customers.ToList();
            foreach (var customer in customers)
            {
                _context.Entry(customer).Collection(c => c.Orders).Load();
            }
            return Ok(customers);
        }
    }
}
