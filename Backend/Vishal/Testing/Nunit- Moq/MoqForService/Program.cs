namespace MoqForService
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Creating a mock user service for testing
            IUserService mockUserService = new MockUserService();

            // Creating UserManager instance with mock user service
            UserManager userManager = new UserManager(mockUserService);

            // Testing GetUser method
            int userId = 2; // Assuming the user ID to be tested
            User user = userManager.GetUser(userId);

            if (user != null)
            {
                Console.WriteLine($"User found with ID: {user.Id}");
                Console.WriteLine($"User Name: {user.Name}");
                Console.WriteLine($"User Email: {user.Email}");
            }
            else
            {
                Console.WriteLine("User not found");
            }
        }
    }   
}
