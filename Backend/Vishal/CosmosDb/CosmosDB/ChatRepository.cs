using CosmosDB.Interfaces;
using CosmosDB.Models;
using Microsoft.Azure.Cosmos;

namespace CosmosDB
{
    public class ChatRepository : IChatRepository
    {
        private readonly CosmosDbContext _context;

        public ChatRepository(CosmosDbContext context)
        {
            _context = context;
        }

        public async Task<CosmosDB.Models.User> CreateUserAsync(CosmosDB.Models.User user)
        {
            var container = _context.GetDatabase().GetContainer("Users");
            var response = await container.CreateItemAsync(user, new PartitionKey(user.id.ToString()));
            return response.Resource;
        }

        public async Task<CosmosDB.Models.User> GetUserByEmailAsync(string email)
        {
            var container = _context.GetDatabase().GetContainer("Users");
            var query = container.GetItemQueryIterator<CosmosDB.Models.User>($"SELECT * FROM c WHERE c.Email = '{email}'");
            var response = await query.ReadNextAsync();
            return response.FirstOrDefault();
        }

        public async Task<IEnumerable<Chats>> GetChatsByUserIdAsync(Guid userId)
        {
            var container = _context.GetDatabase().GetContainer("Chats");
            var query = container.GetItemQueryIterator<Chats>($"SELECT * FROM c WHERE c.UserID = '{userId}'");
            var response = await query.ReadNextAsync();
            return response.ToList();
        }

        public async Task<Chats> CreateChatAsync(Chats chat)
        {
            var container = _context.GetDatabase().GetContainer("Chats");

            chat.id = Guid.NewGuid();

            var response = await container.CreateItemAsync(chat, new PartitionKey(chat.UserID.ToString()));

            return response.Resource;
        }

        public async Task UpdateChatAsync(Chats chat)
        {
            var container = _context.GetDatabase().GetContainer("Chats");

            var response = await container.UpsertItemAsync(chat, new PartitionKey(chat.UserID.ToString()));
        }

        public async Task<Conversations> CreateConversationAsync(Conversations conversation)
        {
            var container = _context.GetDatabase().GetContainer("Conversations");
            var response = await container.CreateItemAsync(conversation);
            return response.Resource;
        }

        public async Task<Chats> GetChatAsync(Guid chatId)
        {
            var container = _context.GetDatabase().GetContainer("Chats");
            var response = await container.ReadItemAsync<Chats>(chatId.ToString(), new PartitionKey(chatId.ToString()));
            return response.Resource;
        }

        public async Task<Conversations> GetConversationAsync(Guid conversationId)
        {
            var container = _context.GetDatabase().GetContainer("Conversations");
            var response = await container.ReadItemAsync<Conversations>(conversationId.ToString(), new PartitionKey(conversationId.ToString()));
            return response.Resource;
        }
    }
}
