using System.Net;

namespace ProductAPI.Model
{
    public class Product
    {
        internal List<Product>? products;

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Price { get; set; }
        public int Quantity { get; set; }
    }
}
