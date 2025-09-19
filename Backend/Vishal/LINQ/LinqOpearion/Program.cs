using System.Collections;

namespace LinqOpearion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // List of Product objects
            List<Product> products = new List<Product>
            {
            new Product { ProductID = 1, Name = "Laptop", Price = 1000, Category = "Electronics" },
            new Product { ProductID = 2, Name = "Chair", Price = 250, Category = "Furniture" },
            new Product { ProductID = 3, Name = "TV", Price = 800, Category = "Electronics" },
            new Product { ProductID = 4, Name = "Table", Price = 300, Category = "Furniture" },
            new Product { ProductID = 5, Name = "Phone", Price = 700, Category = "Electronics" }
            };


            Operations linqOperations = new Operations(products);

            //linqOperations.FilterUsingWhere();
            //linqOperations.Sort();
            //linqOperations.Group();
            //linqOperations.Join();
            //linqOperations.Projection();
            //linqOperations.Aggregate();
            //linqOperations.Elements();

            linqOperations.CountOfProduct();
        }
    }
}
