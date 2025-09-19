using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQOperations2
{
    public class LinqOperations
    {
        private List<Product> productList;

        public LinqOperations()
        {
            // Initialize the list
            productList = new List<Product> 
            {
                new Product() { ProductID = 1, ProductName = "Laptop", Price = 999.99 },
                new Product() { ProductID = 2, ProductName = "Smartphone", Price = 699.99 },
                new Product() { ProductID = 3, ProductName = "Tablet", Price = 399.99 },
                new Product() { ProductID = 4, ProductName = "Headphones", Price = 149.99 },
                new Product() { ProductID = 5, ProductName = "Monitor", Price = 299.99 }
            };
        }

        // Quantifiers
        public bool AllExample(double price)
        {
            // Query syntax
            var allQuery = from p in productList
                           where p.Price > price
                           select p;

            // Method syntax
            var allMethod = productList.All(p => p.Price > price);

            return allQuery.Count() == productList.Count && allMethod;
        }

        public bool AnyExample(double price)
        {
            // Query syntax
            var anyQuery = from p in productList
                           where p.Price == price
                           select p;

            // Method syntax
            var anyMethod = productList.Any(p => p.Price == price);

            return anyQuery.Any() == anyMethod;
        }

        public bool ContainsExample(string productName)
        {
            // Query syntax
            var containsQuery = from p in productList
                                where p.ProductName == productName
                                select p;

            // Method syntax
            var containsMethod = productList.Any(p => p.ProductName == productName);

            return containsQuery.Any() == containsMethod;
        }

        // Elements
        public Product ElementAtExample(int index)
        {
            // Query syntax
            var elementAtQuery = (from p in productList
                                  select p).ElementAt(index);

            // Method syntax
            var elementAtMethod = productList.ElementAt(index);

            Console.WriteLine($"ElementAt (Query Syntax): {elementAtQuery.ProductName}");
            Console.WriteLine($"ElementAt (Method Syntax): {elementAtMethod.ProductName}");

            return elementAtQuery;
        }

        public Product ElementAtOrDefaultExample(int index)
        {
            // Query syntax
            var elementAtOrDefaultQuery = (from p in productList
                                           select p).ElementAtOrDefault(index);

            // Method syntax
            var elementAtOrDefaultMethod = productList.ElementAtOrDefault(index);

            Console.WriteLine($"ElementAtOrDefault (Query Syntax): {elementAtOrDefaultQuery?.ProductName}");
            Console.WriteLine($"ElementAtOrDefault (Method Syntax): {elementAtOrDefaultMethod?.ProductName}");

            return elementAtOrDefaultQuery;
        }

        public Product FirstExample()
        {
            // Query syntax
            var firstQuery = (from p in productList
                              select p).First();

            // Method syntax
            var firstMethod = productList.First();

            Console.WriteLine($"First (Query Syntax): {firstQuery.ProductName}");
            Console.WriteLine($"First (Method Syntax): {firstMethod.ProductName}");

            return firstQuery;
        }

        public Product FirstOrDefaultExample()
        {
            // Query syntax
            var firstOrDefaultQuery = (from p in productList
                                       select p).FirstOrDefault();

            // Method syntax
            var firstOrDefaultMethod = productList.FirstOrDefault();

            Console.WriteLine($"FirstOrDefault (Query Syntax): {firstOrDefaultQuery?.ProductName}");
            Console.WriteLine($"FirstOrDefault (Method Syntax): {firstOrDefaultMethod?.ProductName}");

            return firstOrDefaultQuery;
        }

        public Product LastExample()
        {
            // Query syntax
            var lastQuery = (from p in productList
                             select p).Last();

            // Method syntax
            var lastMethod = productList.Last();

            Console.WriteLine($"Last (Query Syntax): {lastQuery.ProductName}");
            Console.WriteLine($"Last (Method Syntax): {lastMethod.ProductName}");

            return lastQuery;
        }

        public Product LastOrDefaultExample()
        {
            // Query syntax
            var lastOrDefaultQuery = (from p in productList
                                      select p).LastOrDefault();

            // Method syntax
            var lastOrDefaultMethod = productList.LastOrDefault();

            Console.WriteLine($"LastOrDefault (Query Syntax): {lastOrDefaultQuery?.ProductName}");
            Console.WriteLine($"LastOrDefault (Method Syntax): {lastOrDefaultMethod?.ProductName}");

            return lastOrDefaultQuery;
        }

        public Product SingleExample(double price)
        {
            // Query syntax
            var singleQuery = (from p in productList
                               where p.Price == price
                               select p).Single();

            // Method syntax
            var singleMethod = productList.Single(p => p.Price == price);

            Console.WriteLine($"Single (Query Syntax): {singleQuery.ProductName}");
            Console.WriteLine($"Single (Method Syntax): {singleMethod.ProductName}");

            return singleQuery;
        }

        public Product SingleOrDefaultExample(double price)
        {
            // Query syntax
            var singleOrDefaultQuery = (from p in productList
                                        where p.Price == price
                                        select p).SingleOrDefault();

            // Method syntax
            var singleOrDefaultMethod = productList.SingleOrDefault(p => p.Price == price);

            Console.WriteLine($"SingleOrDefault (Query Syntax): {singleOrDefaultQuery?.ProductName}");
            Console.WriteLine($"SingleOrDefault (Method Syntax): {singleOrDefaultMethod?.ProductName}");

            return singleOrDefaultQuery;
        }

        // Concatenation
        public IEnumerable<Product> ConcatExample(IEnumerable<Product> otherProducts)
            {
                // Query syntax
                var concatQuery = from p in productList
                                  select p;

                // Method syntax
                var concatMethod = productList.Concat(otherProducts);

                return concatMethod;
            }

        // Conversion
        public Product[] ToArrayExample()
        {
            // Query syntax
            var toArrayQuery = (from p in productList
                                select p).ToArray();

            // Method syntax
            var toArrayMethod = productList.ToArray();

            return toArrayQuery;
        }

        public Dictionary<int, Product> ToDictionaryExample()
        {
            // Query syntax
            var toDictionaryQuery = (from p in productList
                                     select p).ToDictionary(p => p.ProductID);

            // Method syntax
            var toDictionaryMethod = productList.ToDictionary(p => p.ProductID);

            return toDictionaryQuery;
        }
    }
}
