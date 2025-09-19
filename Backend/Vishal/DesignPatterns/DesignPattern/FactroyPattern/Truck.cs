using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactroyMethod
{
    /// <summary>
    /// A 'ConcreteProduct' class for Truck that inherits from Vehicle.
    /// </summary>
    public class Truck : Vehicle
    {
        public override void Drive()
        {
            Console.WriteLine("Driving a truck with heavy load!");
        }
    }
}
