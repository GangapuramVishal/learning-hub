using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LinqOpearion
{
    public class Operations
    {
        //This field will hold the list of Product objects
        private List<Product> products;

        public Operations(List<Product> products)
        {
            this.products = products;
        }

        //Filtering
        public void FilterUsingWhere()
        { 
            //where - filter using condition
            //Query syntax
            Console.WriteLine("Filtering Products with Price greater than 400:");
            var querySyntaxResult = from product in products
                                    where product.Price > 400
                                    select product;
            Console.WriteLine("Using Query Syntax");
            PrintProducts(querySyntaxResult);

            //Method Syntax
            Console.WriteLine("Filtering Products with Price less than 400:");
            var methodSyntaxResult = products.Where(product => product.Price < 400);
            Console.WriteLine("Using Method Syntax");
            PrintProducts(methodSyntaxResult);
        }
        //Sorting
        public void Sort()
        {
            Console.WriteLine("Sorting Products by Name:");
            Console.WriteLine("Using Query Syntax");
            //Query Syntax
            var querySyntaxResult = from product in products
                                    orderby product.Name ascending
                                    select product;
            PrintProducts(querySyntaxResult);

            //Method syntax 
            var methodSyntaxResult = products.OrderBy(product => product.Name);
            Console.WriteLine("Using Method Syntax");
            PrintProducts(methodSyntaxResult);
        }
        //Grouping
        public void Group()
        {
            Console.WriteLine("Grouping Products by Category");
            //Query Syntax
            var querySyntaxResult = from product in products
                                    group product by product.Category into productGroup
                                    select productGroup;
            Console.WriteLine("Using Query Syntax");
            foreach (var group in querySyntaxResult)
            {
                Console.WriteLine($"Category: {group.Key}");  //key is a category 
                PrintProducts(group);
            }
            //Method syntax
            var methodSyntaxResult = products.GroupBy(product => product.Category);
            Console.WriteLine("Using Method Syntax");
            foreach(var group in methodSyntaxResult)
            {
                Console.WriteLine($"Category: {group.Key}");
                PrintProducts(group);
            }
        }
        //Joining
        public void Join()
        {
            Console.WriteLine("Joining Products by id");
            List<int> categoryIds = new List<int> { 1, 3 }; 
            //Query syntax
            var querySyntaxResult = from product in products 
                                    join id in categoryIds on product.ProductID equals id
                                    select product;
            Console.WriteLine("Using Query Syntax");
            PrintProducts(querySyntaxResult);

            //method sntax 
            Console.WriteLine("Using Method Syntax");
            var methodSyntaxResult = products.Join(categoryIds,
                                                    product => product.ProductID,
                                                    id => id, (product, id) => product);
            Console.WriteLine("Joining Products with Category IDs 1, 3:");
            
            PrintProducts(methodSyntaxResult);
        }

        // Projection using
        public void Projection()
        {
            Console.WriteLine("projecting Products");
            // Query Syntax
            var querySyntaxResult = from product in products
                                    select new { product.Name, product.Price };

            // Method Syntax
            var methodSyntaxResult = products.Select(product => new { product.Name, product.Price });
            Console.WriteLine("Using Query Syntax");
            Console.WriteLine("Projection - Name and Price of Products:");
            foreach (var product in querySyntaxResult)
            {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");
            }
            Console.WriteLine("Using Method Syntax");
            foreach (var product in methodSyntaxResult)
            {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");
            }
        }

        // Aggregation
        public void Aggregate()
        {
            Console.WriteLine("Aggregate functions on Products");
            // Query Syntax
            var querySyntaxResult = (from product in products
                                     select product.Price).Average();

            // Method Syntax
            var methodSyntaxResult = products.Select(product => product.Price).Max();

            Console.WriteLine("Using Query Syntax");
            Console.WriteLine($"Average Price of Products: {querySyntaxResult}");
            Console.WriteLine("Using Method Syntax");
            Console.WriteLine($"Max Price of Products: {methodSyntaxResult}");
        }

        // Elements 
        public void Elements()
        {
            Console.WriteLine("Elements functions on Products");
            // Query Syntax
            var querySyntaxResult = (from product in products
                                     select product).FirstOrDefault();

            // Method Syntax
            var methodSyntaxResult = products.LastOrDefault();

            Console.WriteLine("First Product:");
            Console.WriteLine("Using Query Syntax");
            PrintProduct(querySyntaxResult);
            Console.WriteLine("Using Method Syntax");
            Console.WriteLine("Last Product:");
            PrintProduct(methodSyntaxResult);
        }

        public void MultiOperation()
        {
            var result = products
                .Where(n => n.Price > 400)
                .OrderBy(n => n.Price)
                .Sum(n => n.Price);
            Console.WriteLine($"Sum of Products: {result}");
        }
        public void MultiOperations()
        {
            // Filtering products with price greater than 400
            var Products = products.Where(product => product.Price > 400)
                                           .OrderByDescending(product => product.Price)
                                           .GroupBy(product => product.Category);
            // Printing filtered, sorted, and grouped products
            Console.WriteLine("\nMultioperation Filtered, Sorted, and Grouped Products:");
            foreach (var group in Products)
            {
                Console.WriteLine("Category: " + group.Key);
                PrintProducts(group);

                // Calculating and printing the average price of products within each group
                var averagePrice = group.Average(product => product.Price);
                Console.WriteLine("Average Price of " + group.Key + " Products: " + averagePrice);
            }
        }

            //Helper method to print list of products
            private void PrintProducts(IEnumerable<Product> products)
            {
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductID}, Name: {product.Name}, Price: {product.Price}, Category: {product.Category}");
                }
                Console.WriteLine();
            }

        
        // Helper method to print a single product
        private void PrintProduct(Product product)
        {
            if (product != null)
            {
                Console.WriteLine($"ID: {product.ProductID}, Name: {product.Name}, Price: {product.Price}, Category: {product.Category}");
                Console.WriteLine();
            }
        }
        public void CountOfProduct()
        {
            var count = from p in products
                        group p by p.Category into grpctr
                        select new
                        {
                            count = grpctr.Count(),
                            name = grpctr.Key
                        };
            foreach (var p in count)
            {
                Console.WriteLine($"Category: {p.name}, Count: {p.count}");
            }

            var counts = products
                .GroupBy(p => p.Category)
                .Select(grpctr => new
                {
                    count = grpctr.Count(),
                    name = grpctr.Key
                });

            foreach (var p in counts)
            {
                Console.WriteLine($"Category: {p.name}, Count: {p.count}");
            }
        }
    }
}
