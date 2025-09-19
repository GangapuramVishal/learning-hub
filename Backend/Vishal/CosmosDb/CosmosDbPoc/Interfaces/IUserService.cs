using CosmosDbPoc.Domain;

namespace CosmosDbPoc.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserAsync(Guid userId);
    }
}
