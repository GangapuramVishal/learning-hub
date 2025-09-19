namespace LinqPractice
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            IEnumerable<int> evenNumbers = numbers.Where(x => x % 2 == 0).OrderByDescending(x => x).ToList();
            foreach (int even in evenNumbers)
            {
                Console.WriteLine(even);
            }

            IEnumerable<int> oddNumbers = from x in numbers
                                          where x % 2 != 0
                                          orderby x descending
                                          select x;
            foreach (int odd in oddNumbers)
            {
                Console.WriteLine(odd);
            }


            List<Employee> employee = new List<Employee>()
            {
                new Employee() {Id = 1, Name = "Bobby", age = 23},
                new Employee(){Id = 2, Name = "Chintu", age=25 }
            };

            IEnumerable<Employee> employees = from E in employee
                                              where E.Id == 1
                                              select E;
            foreach(Employee emp in employees)
            {
                Console.WriteLine($"{emp.Name} {emp.Id} {emp.age}");
            }


            var datasource = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    Name = "Bobby",
                    age = 23
                },new Employee()
                {
                    Id = 3,
                    Name = "Yash",
                    age = 23
                },new Employee()
                {
                    Id = 4,
                    Name = "Honey",
                    age = 23
                },new Employee()
                {
                    Id = 2,
                    Name = "Kav's",
                    age = 23
                }
            };
            var querysyntax = (from obj in datasource
                               where obj.Id >2
                               orderby obj.Name descending
                               select obj).ToList();
            foreach (Employee emp in querysyntax)
            {
                Console.WriteLine($"{emp.Id},{emp.Name},{emp.age}");
            }

            var methodsyntax = datasource.OrderByDescending(x => x.Id).ThenBy(x=>x.Name).ToList();
            foreach (Employee emp in methodsyntax)
            {
                Console.WriteLine($"{emp.Id},{emp.Name},{emp.age}");
            }

            var method = datasource.OrderBy(x => x.Name).ToList();

            Console.ReadLine();
        }
    }
}