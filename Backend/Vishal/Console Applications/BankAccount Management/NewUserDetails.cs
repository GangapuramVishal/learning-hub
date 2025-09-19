using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount_Management
{
    public class NewUserDetails
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public long PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? AccountId { get; set; }
        public int Pin { get; set; }
        public double Balance = 0;
    }
}
