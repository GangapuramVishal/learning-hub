using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount_Management
{
    public class Account
    {

        //private fields to store the account details
        //private string AccountNumber;
        //public double Balance;
        private string filePath = "accounts.txt";
        //public double Amount;

        //Constructor 
        //public Account(string AccountNumber)
        //{
        //this.AccountNumber = AccountNumber;

        //}
        NewUserDetails obj = new NewUserDetails();

        public void Deposit()
        {

            Console.WriteLine("Enter Account Number:");
            string accountNumberInput = Console.ReadLine();

            Console.WriteLine("Enter PIN:");
            int pinInput;
            while (!int.TryParse(Console.ReadLine(), out pinInput))
            {
                Console.WriteLine("Invalid PIN. Please enter a valid PIN:");
            }

            // Check if the account details exist in the file
            bool accountExists = CheckAccountExists(accountNumberInput, pinInput.ToString());

            if (accountExists)
            {
                Console.WriteLine("Enter Amount to Deposit:");
                double depositAmount;
                while (!double.TryParse(Console.ReadLine(), out depositAmount))
                {
                    Console.WriteLine("Invalid amount. Please enter a valid amount:");
                }
                 obj.Balance += depositAmount;
                var num = obj.Balance;

                Console.WriteLine($"Deposit of {depositAmount} successful.");
                Console.WriteLine($"Updated Balance: {obj.Balance}");
            }
            else
            {
                Console.WriteLine("Invalid Account Number or PIN.");
            }
        }
        private bool CheckAccountExists(string accountNumber, string pin)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    if (line.Contains(accountNumber))
                    {
                        // Account number exists, now check if the PIN is correct
                        while ((line = reader.ReadLine()) != null && !line.Contains("Account ID:"))
                        {
                            if (line.Contains(pin))
                            {
                                // PIN matches
                                return true;
                            }
                        }
                        // PIN not found for the account
                        return false;
                    }
                }
                // Account not found
                return false;
            }
        }


        public void WithDrawAmount()
        {
            Console.WriteLine("Enter Account Number:");
            string accountNumberInput = Console.ReadLine();

            Console.WriteLine("Enter PIN:");
            //int pinInput = int.Parse(Console.ReadLine());
            int pinInput;

            //Console.WriteLine("Enter Amount to Withdraw:");
            //double withdrawAmount = double.Parse(Console.ReadLine());
            while (!int.TryParse(Console.ReadLine(), out pinInput))
            {
                Console.WriteLine("Invalid PIN. Please enter a valid PIN:");
            }

            // Check if the account details exist in the file
            bool accountExists = CheckAccountExists(accountNumberInput, pinInput.ToString());

            if (accountExists)
            {
                Console.WriteLine("Enter Amount to Withdraw:");
                double withdrawAmount;
                while (!double.TryParse(Console.ReadLine(), out withdrawAmount))
                {
                    Console.WriteLine("Invalid amount. Please enter a valid amount:");
                }
                obj.Balance -= withdrawAmount;
                Console.WriteLine($"Withdrawal of {withdrawAmount} successful.");
                Console.WriteLine($"Updated Balance: {obj.Balance}");
            }
            else
            {
                Console.WriteLine("Invalid Account Number or PIN.");
            }





              //  if (accountNumberInput == obj.AccountId && pinInput == obj.Pin)
            //{
              //  if (withdrawAmount <= Balance)
             //   {
             //       Balance -= withdrawAmount;
              //      Console.WriteLine($"Withdrawal of {withdrawAmount} successful.");
              //      Console.WriteLine($"Updated Balance: {Balance}");
              //  }
              //  else
              //  {
             //       Console.WriteLine("Insufficient balance.");
             //   }
           // }
           // else
           // {
           //     Console.WriteLine("Invalid Account Number or PIN.");
           // }
        }
        public void CheckBalance()
        {
            Console.WriteLine("Enter Account Number:");
            string accountNumberInput = Console.ReadLine();

            Console.WriteLine("Enter PIN:");
            int pinInput;
            while (!int.TryParse(Console.ReadLine(), out pinInput))
            {
                Console.WriteLine("Invalid PIN. Please enter a valid PIN:");
            }

            // Check if the account details exist in the file
            bool accountExists = CheckAccountExists(accountNumberInput, pinInput.ToString());

            if (accountExists)
            {
                Console.WriteLine($"Available Balance: {obj.Balance}");
            }

            else
            {
                Console.WriteLine("Invalid Account Number or PIN.");
            }
        }
    }
}
