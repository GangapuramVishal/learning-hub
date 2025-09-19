using CosmosDbPoc.Domain;
using CosmosDbPoc.Interfaces;

namespace CosmosDbPoc
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(User user)
        {
            await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            return await _userRepository.GetUserAsync(userId);
        }

    }
}
