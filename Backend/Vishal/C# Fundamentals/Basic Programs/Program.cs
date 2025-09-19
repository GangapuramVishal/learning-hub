namespace Basic_Programs
{
    public class Program
    {

        // method declaration
        static int addNumbers()
        {
            int sum = 5 + 14;
            return sum;
           

        }


        static void Main(string[] args)
        {
            //creating a class object
            //Class1 sum = new Class1();
            //int total = sum.AddTwoNumbers(30, 20);
            //Console.WriteLine($"The total sum is: {total}");

            //PrimeNumbers primeNumbers = new PrimeNumbers();
            //primeNumbers.PrintPrimeNumbers();

            //Class1 dog = new Class1();
            //dog.bark();
            //dog.breed = "bullDog";
            //Console.WriteLine(dog.breed);

            //Class1 e1 = new Class1();
            //Console.WriteLine("Employee 1");
            //e1.Employee("Coding");
            //e1.name= "Vishal";
            //Console.WriteLine($"Name: {e1.name}");


            // call method 
            //int sum = addNumbers();
            //Console.WriteLine(sum);
            //Console.ReadLine();

            //MethodOverLoading
            MethodOverLoading obj = new MethodOverLoading();
            obj.AddNumbers(30, 20);
            obj.AddNumbers(20, 30, 40);

        }
    }
}
    

