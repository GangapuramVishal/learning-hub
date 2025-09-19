namespace LINQOperations2
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of LinqOperations
            LinqOperations linqOps = new LinqOperations();

            // Test Quantifiers
            Console.WriteLine("Quantifiers - All, Any, Contains");
            Console.WriteLine($"All products have price greater than $500: {linqOps.AllExample(500)}");
            Console.WriteLine($"Any product has price equal to $399.99: {linqOps.AnyExample(399.99)}");
            Console.WriteLine($"Product list contains 'Tablet': {linqOps.ContainsExample("Tablet")}");

            // Test Elements
            Console.WriteLine("\nElementAt Example:");
            linqOps.ElementAtExample(2);
            Console.WriteLine();

            Console.WriteLine("ElementAtOrDefault Example:");
            linqOps.ElementAtOrDefaultExample(3);
            Console.WriteLine();

            Console.WriteLine("First Example:");
            linqOps.FirstExample();
            Console.WriteLine();

            Console.WriteLine("FirstOrDefault Example:");
            linqOps.FirstOrDefaultExample();
            Console.WriteLine();

            Console.WriteLine("Last Example:");
            linqOps.LastExample();
            Console.WriteLine();

            Console.WriteLine("LastOrDefault Example:");
            linqOps.LastOrDefaultExample();
            Console.WriteLine();

            Console.WriteLine("Single Example:");
            linqOps.SingleExample(399.99);
            Console.WriteLine();

            Console.WriteLine("SingleOrDefault Example:");
            linqOps.SingleOrDefaultExample(999.99);
            Console.WriteLine();


            // Test Concatenation
            Console.WriteLine("\nConcatenation - Concat");
            var additionalProducts = new Product[] 
            {
                new Product() { ProductID = 6, ProductName = "Keyboard", Price = 49.99 },
                new Product() { ProductID = 7, ProductName = "Mouse", Price = 29.99 }
            };
            var concatenatedProducts = linqOps.ConcatExample(additionalProducts);
            Console.WriteLine("Concatenated Products:");
            foreach (var product in concatenatedProducts)
            {
                Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Price: ${product.Price}");
            }

            // Test Conversion
            Console.WriteLine("\nConversion - ToArray and ToDictionary");
            var toArrayResult = linqOps.ToArrayExample();
            var toDictionaryResult = linqOps.ToDictionaryExample();

            Console.WriteLine("\nConverted Array:");
            foreach (var product in toArrayResult)
            {
                Console.WriteLine($"[ProductID: {product.ProductID}, ProductName: {product.ProductName}, Price: {product.Price}]");
            }

            Console.WriteLine("\nConverted Dictionary:");
            foreach (var kvp in toDictionaryResult)
            {
                Console.WriteLine($"{{Key: {kvp.Key}, Value: [ProductID: {kvp.Value.ProductID}, ProductName: {kvp.Value.ProductName}, Price: {kvp.Value.Price}]}}");
            }
        }
    }
}















// Test Elements
//Console.WriteLine("\nElements - ElementAt, ElementAtOrDefault, First, FirstOrDefault, Last, LastOrDefault, Single, SingleOrDefault");
//Console.WriteLine($"Product at index 2: {linqOps.ElementAtExample(2)?.ProductName}");
//Console.WriteLine($"Product at index 2 or default: {linqOps.ElementAtOrDefaultExample(2)?.ProductName}");
//Console.WriteLine($"First product: {linqOps.FirstExample()?.ProductName}");
//Console.WriteLine($"First product or default: {linqOps.FirstOrDefaultExample()?.ProductName}");
//Console.WriteLine($"Last product: {linqOps.LastExample()?.ProductName}");
//Console.WriteLine($"Last product or default: {linqOps.LastOrDefaultExample()?.ProductName}");
//Console.WriteLine($"Single product with price $399.99: {linqOps.SingleExample(399.99)?.ProductName}");
//Console.WriteLine($"Single product with price $399.99 or default: {linqOps.SingleOrDefaultExample(399.99)?.ProductName}");