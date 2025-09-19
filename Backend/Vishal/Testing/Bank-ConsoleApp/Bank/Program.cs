namespace Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating two bank accounts
            BankAccount account1 = new BankAccount(-10);
            BankAccount account2 = new BankAccount(500);

            // Display initial balances
            Console.WriteLine("Initial balances:");
            Console.WriteLine("Account 1 balance: $" + account1.Balance);
            Console.WriteLine("Account 2 balance: $" + account2.Balance);
            Console.WriteLine();

            // Performing some transactions
            account1.Withdraw(200);
            account2.Add(100);
            account1.TransferFundsTo(account2, 300);

            // Display updated balances
            Console.WriteLine("Updated balances after transactions:");
            Console.WriteLine("Account 1 balance: $" + account1.Balance);
            Console.WriteLine("Account 2 balance: $" + account2.Balance);

            Console.ReadLine();
        }
    }
}
