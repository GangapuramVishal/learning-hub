namespace LiskovSubstitutionPrinciple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee();
            PermanentEmployee permanentEmployee = new PermanentEmployee();
            ContractEmployee contractEmployee = new ContractEmployee();
            Intern intern = new Intern();

            Console.WriteLine("Regular Employee:");
            Console.WriteLine("Salary: " + employee.CalculateSalary());
            //Console.WriteLine("Bonus: " + employee.CalculateBonus());


            Console.WriteLine("Permanent Employee: ");
            Console.WriteLine("Salary: " + permanentEmployee.CalculateSalary());
            Console.WriteLine("Bonus: " + permanentEmployee.CalculateBonus());
            
            Console.WriteLine("Contract Employee: ");
            Console.WriteLine("Salary: " + contractEmployee.CalculateSalary());
            Console.WriteLine("Bonus: " + contractEmployee.CalculateBonus()); 
            

            Console.WriteLine("Intern: ");
            Console.WriteLine("Salary: " + intern.CalculateSalary());
            

            ////=============== violating LSP================

            //Employee employee = new Employee();
            //PermanentEmployee permanentEmployee = new PermanentEmployee();
            //Intern intern = new Intern();

            //Console.WriteLine($"Salary: {employee.CalculateSalary()}");
            //Console.WriteLine($"Bonus: {employee.CalculateBonus()}");

            //Console.WriteLine($"Salary: {permanentEmployee.CalculateSalary()}");
            //Console.WriteLine($"Bonus: {permanentEmployee.CalculateBonus()}");

            //Console.WriteLine($"Salary: {intern.CalculateSalary()}");

            ////Intern class should not have access to bonus method but in this he is accessing 
            //Console.WriteLine($"Bonus: {intern.CalculateBonus()}");
        }

    }
}

/* while inheritance provides the mechanism for code reuse and establishing class hierarchies, LSP ensures that the relationships 
 * between base and derived classes maintain program correctness and consistency. In essence, LSP complements inheritance by 
 * guiding how inheritance should be applied to maintain the integrity of the software design.
 */