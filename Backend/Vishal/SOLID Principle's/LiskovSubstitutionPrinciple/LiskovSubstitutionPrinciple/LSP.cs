using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiskovSubstitutionPrinciple
{
    internal class LSP
    {
        /* The object of child class must be able to replace the 
         * object of the parent class without breaking the application.
         * 
         * All the base class methods must be applicable for derived classes also.
         * 
         */
    }

    public class Employee
    {
        public virtual int CalculateSalary()
        {
            return 400000;
        }
    }

    public class PermanentEmployee : Employee, IEmployee
    {
        //public override int CalculateSalary()
        //{
        //    return 600000;
        //}
        public int CalculateBonus()
        {
            return 10000;
        }
    }

    public class ContractEmployee: Employee, IEmployee
    {
        public int CalculateBonus()
        {
            return 5000;
        }
    }
    //Intern should not have Bonus
    public class Intern : Employee
    {
        public override int CalculateSalary()
        {
            return 12000;
        }
    }

}
