using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversionPrinciple
{
    internal class Dog : IAnimal
    {
        //low-level module
        public void MakeSound()
        {
            Console.WriteLine("Barking......");
        }
    }
}


//Dog and Cat classes represent low-level modules. They encapsulate the specific behavior of individual animals, such as making sounds.