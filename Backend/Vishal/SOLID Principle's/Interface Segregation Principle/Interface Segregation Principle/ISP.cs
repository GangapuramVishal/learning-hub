using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Segregation_Principle
{
    internal class ISP
    {
        /* The Interface Segregation(ISP) states that a class should not 
         * be forced to implement interfaces that it does not use.
         * 
         * It is better to have multiple smaller interfaces than larger interfaces.
         */
    }

    public class Car : IVehicle
    {
        public void Start()
        {
            Console.WriteLine("Car Engin Started ");
        }

        public void Drive()
        {
            Console.WriteLine("Car Started Moving");
        }
    }
    public class FlyingCar : IVehicle,IFlyVehicle
    {
        public void Start()
        {
            Console.WriteLine("Car Engin Started ");
        }

        public void Drive()
        {
            Console.WriteLine("Car Started Moving");
        }
        public void Fly()
        {
            Console.WriteLine("Car Started Flying ");
        }
    }
}
