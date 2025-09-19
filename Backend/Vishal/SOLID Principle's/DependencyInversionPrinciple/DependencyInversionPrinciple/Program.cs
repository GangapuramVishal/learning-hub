using DependencyInversionPrinciple;

namespace DependencyInversionPrinciple
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Sound sound = new Sound();

            IAnimal dogAnimal = new Dog();
            IAnimal catAnimal = new Cat();

            sound.MakeSound(dogAnimal);

            sound.MakeSound(catAnimal);


            // Here we are performing Dependency Inversion because the method in Sound class is depending on interface.
        }
    }
}

