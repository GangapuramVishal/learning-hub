using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversionPrinciple
{
    /// Dependency inversion principle states that high-level modules should not depend on low-level modules. Both should depend on abstractions.
    /// Abstractions should not depend on details. Details should depend on abstractions.
    /// </summary>
    internal class Cat : IAnimal
    {
        //low-level module
        public void MakeSound()
        {
            Console.WriteLine("Meowwww!!!!");
        }
    }
}
