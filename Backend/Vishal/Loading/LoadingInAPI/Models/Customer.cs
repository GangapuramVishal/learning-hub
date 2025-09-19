namespace LoadingInAPI.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>(); // Initialize Orders collection in the constructor
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}






// there's a circular reference in the object graph being
// serialized, causing the JSON serializer to enter an
// infinite loop. This usually happens when you have bidirectional
// navigation properties in your entity classes, such as when
// Customer has a collection of Order and Order has a reference to
// Customer.

//By adding [JsonIgnore] attribute to the Customer property in
//the Order class, you're instructing the JSON serializer to
//ignore this property during serialization, preventing the circular reference issue.