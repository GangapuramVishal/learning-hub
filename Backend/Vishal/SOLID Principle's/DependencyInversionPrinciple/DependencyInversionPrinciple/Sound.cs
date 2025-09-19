using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversionPrinciple
{
    internal class Sound                       // high level module
    {
        //The sound class can make the animals produce their sounds without needing to know the specific type of animal.
        public void MakeSound(IAnimal animal)
        {
            animal.MakeSound();
        }
    }
}
