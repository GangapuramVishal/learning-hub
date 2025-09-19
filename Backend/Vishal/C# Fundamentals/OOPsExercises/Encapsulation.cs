using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OOPsExercises
{
    internal class Encapsulation
    {
        /* Encapsulation in C# is the bundling of data (attributes) and methods (functions) that operate on the data into a single unit, typically called a class. 
         * It hides the internal state of an object from the outside world and only exposes the necessary functionalities through methods.
         * This helps in achieving data abstraction and data hiding, which are essential principles of object-oriented programming. */
         
        /* Consider a scenario where you are tasked with designing a simple banking system in C#. You decide to implement a BankAccount 
           class to represent individual bank accounts. Your BankAccount class contains private instance variables such as accountNumber,
           balance, and ownerName, along with methods like Deposit() and Withdraw() to manipulate the account balance.
        */
    }

    //class contains Fields and Methods
    public class BankAccount
    {
        //private fields to store the account details
        private string AccountNumber;
        private decimal Balance;

        //Constructor 
        public BankAccount(string AccountNumber, decimal InitialBalance)
        {
            this.AccountNumber = AccountNumber;
            this.Balance = InitialBalance;
            
        }
        // Public method to deposit money into the account
        public void Deposit(decimal Amount)
        {
            if (Amount > 0)
            {
                Balance += Amount;
                Console.WriteLine($"{Amount} Credited Succefull");
                Console.WriteLine($"Total Available Balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Invalid amount for deposit");
            }
            
        }
        // Public method to withdraw money from the account
        public void WithDraw(decimal Amount)
        {
            if(Amount>0 && Amount <= Balance)
            {
                Balance -= Amount;
                Console.WriteLine($"{Amount} Debited Succefull");
                Console.WriteLine($"Total Available Balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient Amount!" );
            }
        }
        public decimal CheckBalance()
        {
            return Balance;

        }
    }
}
