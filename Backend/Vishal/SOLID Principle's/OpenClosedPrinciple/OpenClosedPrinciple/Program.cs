using Microsoft.Win32.SafeHandles;

namespace OpenClosedPrinciple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Code Violating OCP
            //Account savingsAccount = new Account()
            //{
            //    Name = "Bobby",
            //    Address = "Durga Nagar",
            //    AccountType = "savings",
            //    Balance = 2000
            //};

            //savingsAccount.CalculateInterest();

            //Code following OCP

            Account details = new Account()
            {
                Name = "Bobby",
                Address = "Durga Nagar",
                Balance = 2000
            };
            // Creating instances of different account types
            IAccount saving = new SavingAccount();
            IAccount zeroAccount = new ZeroAccount();

            Console.WriteLine($"interest for savings account {saving.CalculateInterest(details)}");
            Console.WriteLine($"interest for zero account {zeroAccount.CalculateInterest(details)}");
        }
    }
}
