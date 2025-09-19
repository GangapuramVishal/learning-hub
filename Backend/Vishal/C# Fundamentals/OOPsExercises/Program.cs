using System.Security.Principal;
using static OOPsExercises.InterfaceExamples;

namespace OOPsExercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            ------------------Encapsulation--------------------------
            // Creating an instance of BankAccount class
            BankAccount myaccount = new BankAccount("CBI1236", 1000);

            // Depositing and withdrawing money
            myaccount.Deposit(500);
            myaccount.WithDraw(200);


            // Attempting to access balance directly (which is not allowed due to encapsulation)
            //Console.WriteLine($"Current balance: {myaccount.Balance}"); // This will result in a compilation error

            // Getting balance using public method
            Console.WriteLine($"Current balance: {myaccount.CheckBalance()}");

            */

            /*
             --------------------- data Abstraction ------------------------
            //Creating instances of rectangle and circle
            Rectangle rectangle = new Rectangle { Length = 5, Width= 3};
            Circle circle = new Circle { Radius = 4 };

            // Calculating and printing area of rectangle and circle
            Console.WriteLine($"Area of rectangle: {rectangle.CalculateArea()}");
            Console.WriteLine($"Area of circle: {circle.CalculateArea()}");

            */

            /* 
             ------------------- Inheritance ---------------------------------
            // Single inheritance
            Dog dog = new Dog();
            dog.Eat();   // Inherited from Animal
            dog.Run(); // Inherited from Animal
            dog.Bark();  // Defined in Dog class

            Console.WriteLine();

            // Multilevel inheritance
            GoldenRetriver shepherd = new GoldenRetriver();
            shepherd.Eat();    // Inherited from Animal
            shepherd.Run();  // Inherited from Animal
            shepherd.Bark();   // Inherited from Dog
            shepherd.Plays();  // Defined in GoldenRetriver class

            Console.WriteLine();

            // Hierarchical inheritance
            Cat cat = new Cat();
            cat.Eat();    // Inherited from Animal
            cat.Run();  // Inherited from Animal
            cat.sound();   // Defined in Cat class
             
             */

            /*

            //------------------ Polymorphism ------------------------

            Calculator calculator = new Calculator();
            int sum1 = calculator.Add(12, 15); // Calls int Add(int a, int b)
            Console.WriteLine(sum1);
            double sum2 = calculator.Add(33.2, 2.33); // Calls double Add(double a, double b)
            Console.WriteLine(sum2);
            */
            //----------- Overriding--------------

            Vehicle myVehicle = new Vehicle();
            Vehicle myCar = new Car();
            Vehicle MyAutoRickshaw = new AutoRickshaw();

            myVehicle.StartEngine();
            myCar.StartEngine();
            MyAutoRickshaw.StartEngine();

            

            //------------------- Implementing Interfaces--------------------------------

            //IMediaPlayer videoplayer = new VideoPlayer();
            //IMediaPlayer musicplayer = new MusicPlayer();

            //videoplayer.Play();
            //videoplayer.Pause();
            //videoplayer.Stop();

            //musicplayer.Play();
            //musicplayer.Pause();
            //musicplayer.Stop();
        }
    }
}
