using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqForService
{
    public class MockUserService : IUserService
    {
        public User GetUserById(int userId)
        {
            // Creating a mock user for testing
            // You can customize this according to your testing scenario
            if (userId == 1)
            {
                return new User
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john.doe@example.com"
                };
            }
            else
            {
                return null; // Simulating user not found scenario
            }
        }
    }
}
