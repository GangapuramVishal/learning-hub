using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Records_Classes
{
    public class PersonAsClass
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PersonAsClass(string firstName, string lastName) 
        {
            FirstName = firstName;
            LastName = lastName;
        }
        //public override string ToString() => $"{FirstName} {LastName}";
    }

    //Inheritance
    public class Employee : PersonAsClass
    {
        public int EmployeeId { get; set; }

        public Employee(string firstName, string lastName, int employeeId)
            : base(firstName, lastName)
        {
            EmployeeId = employeeId;
        }
    }
}
