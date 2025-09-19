using System.ComponentModel.DataAnnotations;

namespace Moq_ProductAPI_Testing.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; } 
        public string? ProductCategory { get; set; }
    }
}
