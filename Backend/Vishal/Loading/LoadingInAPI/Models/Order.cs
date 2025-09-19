using System.Text.Json.Serialization;

namespace LoadingInAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; } // Foreign key property

        [JsonIgnore] // Ignore Customer navigation property to break the cycle
        public Customer Customer { get; set; } // Navigation property
    }
}
