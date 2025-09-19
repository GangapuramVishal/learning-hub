using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenClosedPrinciple
{
    internal class OCP
    {
        /* A software class or module should be "open for extension but closed for modification"
         * If we have written a class then it should be flexible enough that we should not change
         * it (closed for modification) until there are bugs but a new feature can be added 
         * (open for extension) by adding new code without modifying its existing code.
         * OCP promotes code stability and reduces the risk of introducing bugs when extending the system.
         */
    }

    public class Account
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Balance { get; set; }
    }

    public class SavingAccount : IAccount
    {
        public double CalculateInterest(Account account)
        {
            return account.Balance * 0.3;
        }
    }

    public class ZeroAccount : IAccount
    {
        public double CalculateInterest(Account account)
        {
            return account.Balance * 0.1;
        }
    }
    public class CurrentAccount : IAccount
    {
        public double CalculateInterest(Account account)
        {
            return account.Balance * 0.5;
        }
    }
}
