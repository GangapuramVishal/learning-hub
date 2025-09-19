using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessGame
{
    public class Guess
    {
        public void display()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 100);
            Console.WriteLine($"Number is choosen by Computer \n ");
            Console.WriteLine("Ready");

            int userNumber;

            Console.WriteLine($"Enter the number");
            userNumber = int.Parse(Console.ReadLine());

            //int count = 0;


            if (userNumber > randomNumber)
            {
                Console.WriteLine("****WON****");
                //count++;

            }

            else if (userNumber < randomNumber)
            {
                Console.WriteLine("LOST");

            }
            else if (userNumber == randomNumber)
            {
                Console.WriteLine("DRAW");
            }
        }

    }

}
