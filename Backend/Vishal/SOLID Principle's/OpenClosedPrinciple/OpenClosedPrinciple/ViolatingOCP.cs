using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenClosedPrinciple
{
    internal class ViolatingOCP
    {
    }
    //==========Code Violating OCP=======================

    //public class Account
    //{
    //    public string Name { get; set; }
    //    public string Address { get; set; }
    //    public string AccountType { get; set; }
    //    public double Balance { get; set; }

    //    public void CalculateInterest()
    //    {
    //        if (AccountType == "savings")
    //        {
    //            Console.WriteLine($"Interest calculated for savings account: {Balance * 0.3}");
    //        }
    //        else if (AccountType == "current")
    //        {
    //            Console.WriteLine($"Interest calculated for current account: {Balance * 0.5}");
    //        }
    //        else
    //        {
    //            Console.WriteLine("Invalid account type.");
    //        }
    //    }
    //}
}
//If we want to add calculate interest for other accounts then we need to modify the class by keeping else if.
//which is against the OCP


