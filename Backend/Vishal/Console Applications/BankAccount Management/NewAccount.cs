using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount_Management
{
    public class NewAccount
    {
        NewUserDetails NewUserDetails = new NewUserDetails();
        private string filePath = "accounts.txt";
        public void CreateAccount()
        {
            Console.WriteLine("Enter First Name:");
            NewUserDetails.FirstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name:");
            NewUserDetails.LastName = Console.ReadLine();

            NewUserDetails.Age = ReadValidAge();


            NewUserDetails.PhoneNumber = ReadValidPhoneNumber();

            Console.WriteLine("Enter Address:");
            NewUserDetails.Address = Console.ReadLine();

            Console.WriteLine($"Balance:{NewUserDetails.Balance} ");

            
           

            // Generating unique account ID
            string uniquePart = $"{NewUserDetails.FirstName.Substring(0, 2)}{NewUserDetails.LastName.Substring(0, 2)}{NewUserDetails.PhoneNumber.ToString().Substring(6, 4)}";
            Console.WriteLine("******Account Created Successfully!*******");
            Console.WriteLine("------------------------------------------");
            NewUserDetails.AccountId = uniquePart.ToUpper(); // Convert to uppercase for consistency
            Console.WriteLine($"Account Id :{NewUserDetails.AccountId}");
            GeneratePin();
            SaveAccountToFile();
            Console.WriteLine("------------------------------------------");
        }
        private void SaveAccountToFile()
        {
            // Open or create the file and append the new account details
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine($"Account ID: {NewUserDetails.AccountId}");
                writer.WriteLine($"Password is: {NewUserDetails.Pin}");
                writer.WriteLine($"First Name: {NewUserDetails.FirstName}");
                writer.WriteLine($"Last Name: {NewUserDetails.LastName}");
                writer.WriteLine($"Age: {NewUserDetails.Age}");
                writer.WriteLine($"Phone Number: {NewUserDetails.PhoneNumber}");
                writer.WriteLine($"Address: {NewUserDetails.Address}");
                writer.WriteLine($"Balance: {NewUserDetails.Balance}");


                writer.WriteLine(); // Empty line to separate accounts
            }
        }

        public void DisplayAccountDetails()
        {
            Console.WriteLine("\nAccount Details:");
            Console.WriteLine($"First Name: {NewUserDetails.FirstName}");
            Console.WriteLine($"Last Name: {NewUserDetails.LastName}");
            Console.WriteLine($"Age: {NewUserDetails.Age}");
            Console.WriteLine($"Phone Number: {NewUserDetails.PhoneNumber}");
            Console.WriteLine($"Address: {NewUserDetails.Address}");
            Console.WriteLine($"Account ID: {NewUserDetails.AccountId}");
            Console.WriteLine($"Balance: {NewUserDetails.Balance}");
        }
        private int ReadValidAge()
        {
            int age;
            while (true)
            {
                Console.WriteLine("Enter Age:");
                if (int.TryParse(Console.ReadLine(), out age) && age > 18)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Age must be above 18. Please enter a valid age.");
                }
            }
            return age;
        }

        private long ReadValidPhoneNumber()
        {
            long phoneNumber;
            while (true)
            {
                Console.WriteLine("Enter Phone Number (10 digits):");
                string phoneInput = Console.ReadLine();
                if (phoneInput.Length == 10 && long.TryParse(phoneInput, out phoneNumber))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid phone number. Please enter a 10-digit numeric phone number.");
                }
            }

            return phoneNumber;
        }

        private void GeneratePin()
        {
            // Extracting first two and last two digits from phone number
            int firstTwoDigits = (int)(NewUserDetails.PhoneNumber / 100000000); // First two digits
            int lastTwoDigits = (int)(NewUserDetails.PhoneNumber % 100);   // Last two digits

            // Combining first two and last two digits to generate PIN
            NewUserDetails.Pin = firstTwoDigits * 100 + lastTwoDigits;
            var generatepin = NewUserDetails.Pin;
            Console.WriteLine($"Your'e Password is :{generatepin}");

        }


    }
}

