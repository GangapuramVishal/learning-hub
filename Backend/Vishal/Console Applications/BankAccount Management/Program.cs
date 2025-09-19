namespace BankAccount_Management
{
    public class Program
    {
        static void Main(string[] args)
        {
            NewAccount user = new NewAccount();

            Account myaccount = new Account();

            //user.CreateAccount();
            //user.DisplayAccountDetails();


            for (; ; )
            {
                Console.WriteLine("Choose one of the below options\n");
                /*Console.WriteLine(pick);*/
                Console.WriteLine("-----------------------------------------------------------------------------------");
                Console.WriteLine("1.New Acoount\t2.Deposite\t3.Check Balance \t4.Withdraw\t5.Exit");
                Console.WriteLine("-----------------------------------------------------------------------------------");
                var ch = int.Parse(Console.ReadLine());

                switch (ch)
                {
                    case 1:
                        {
                            /* Console.WriteLine("First Option");*/
                            user.CreateAccount();
                            break;
                        }
                    case 2:
                        {
                            myaccount.Deposit();
                            /*Console.WriteLine("Second option selcted");*/
                            break;
                        }
                    case 3:
                        {
                            myaccount.CheckBalance();
                            break;

                        }

                    case 4:
                        {
                            myaccount.WithDrawAmount();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("enter a valid option");
                            break;
                        }
                }
            }

        }
    }
}
