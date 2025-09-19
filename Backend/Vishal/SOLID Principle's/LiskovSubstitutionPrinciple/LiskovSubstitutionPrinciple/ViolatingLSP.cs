using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiskovSubstitutionPrinciple
{
    //public class ViolatingLSP
    //{
    //}
    ////Base Class/ super class
    //public class Employee
    //{
    //    public virtual int CalculateSalary()
    //    {
    //        return 400000;
    //    }
    //    public virtual int CalculateBonus()
    //    {
    //        return 5000;
    //    }
    //}
    ////Derived Class
    //public class PermanentEmployee : Employee
    //{
    //    public override int CalculateSalary()
    //    {
    //        return 600000;
    //    }
    //}
    ////Intern should not have Bonus
    //public class Intern : Employee
    //{
    //    public override int CalculateSalary()
    //    {
    //        return 12000;
    //    }
    //    //by implementing this method it will break our Program so this is wrong
    //    //public override int CalculateBonus()
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    //}
}
