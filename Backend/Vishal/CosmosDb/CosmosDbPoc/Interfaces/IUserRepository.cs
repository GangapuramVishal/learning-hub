using CosmosDbPoc.Domain;

namespace CosmosDbPoc.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserAsync(Guid userId);
    }
}
