using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"#*_GUESS & TEST_*#\n\n");
            Console.WriteLine($"WELCOME TO THE GAME\n\n");

            Guess guess = new Guess();
            for(int i = 0; i <= 3; i++)
            {
                guess.display();
                Console.WriteLine("______________________________\n\n");
            }
            Console.ReadLine();
        }
    }
}
