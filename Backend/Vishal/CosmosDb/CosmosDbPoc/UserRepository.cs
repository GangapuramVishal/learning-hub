using CosmosDbPoc.DbContext;
using CosmosDbPoc.Interfaces;
using CosmosDbPoc.Domain;
using Microsoft.Azure.Cosmos;
using User = CosmosDbPoc.Domain.User;

namespace CosmosDbPoc
{
    public class UserRepository : IUserRepository
    {
        private readonly CosmosDbContext _context;

        public UserRepository(CosmosDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.UserContainer.CreateItemAsync(user, new PartitionKey(user.id.ToString()));
        }


        public async Task<User> GetUserAsync(Guid userId)
        {
            try
            {
                ItemResponse<User> response = await _context.UserContainer.ReadItemAsync<User>(userId.ToString(), new PartitionKey(userId.ToString()));
                return response.Resource;
            }
            catch (CosmosException)
            {
                return null;
            }
        }
    }
}
