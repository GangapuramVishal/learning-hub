using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Segregation_Principle
{
    internal class ViolatingISP
    {
    }

    //public class Car : IVehicleViolating
    //{
    //    public void Start()
    //    {
    //        Console.WriteLine("Car Engin Started ");
    //    }

    //    public void Drive()
    //    {
    //        Console.WriteLine("Car Started Moving");
    //    }

    //    //we are forcing Fly method to implement
    //    //if we remove Fly method the interface can't work
    //    //Normal car can't fly
    //    public void Fly()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //public class FlyingCar : IVehicleViolating
    //{
    //        public void Start()
    //        {
    //            Console.WriteLine("Car Engin Started ");
    //        }

    //        public void Drive()
    //        {
    //            Console.WriteLine("Car Started Moving");
    //        }

    //        public void Fly()
    //        {
    //            Console.WriteLine("Car Started Flying ");
    //        }
    //}
}
