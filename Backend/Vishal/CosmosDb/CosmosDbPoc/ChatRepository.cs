using CosmosDbPoc.DbContext;
using CosmosDbPoc.Domain;
using CosmosDbPoc.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CosmosDbPoc
{
    public class ChatRepository : IChatRepository
    {
        private readonly CosmosDbContext _context;

        public ChatRepository(CosmosDbContext context)
        {
            _context = context;
        }

        public async Task CreateChatAsync(Chats chat)
        {
            await _context.ChatContainer.CreateItemAsync(chat, new PartitionKey(chat.UserID.ToString()));
        }

        public async Task<Chats> GetChatAsync(Guid chatId)
        {
            try
            {
                ItemResponse<Chats> response = await _context.ChatContainer.ReadItemAsync<Chats>(chatId.ToString(), new PartitionKey(chatId.ToString()));
                return response.Resource;
            }
            catch (CosmosException)
            {
                return null;
            }
        }

        public async Task UpdateChatAsync(Chats chat)
        {
            await _context.ChatContainer.UpsertItemAsync(chat, new PartitionKey(chat.UserID.ToString()));
        }

        public async Task DeleteChatAsync(Guid chatId)
        {
            await _context.ChatContainer.DeleteItemAsync<Chats>(chatId.ToString(), new PartitionKey(chatId.ToString()));
        }

        public async Task<List<Conversations>> GetConversationsForChatAsync(Guid chatId)
        {
            var query = _context.ConversationContainer.GetItemQueryIterator<Conversations>(
                new QueryDefinition("SELECT * FROM c WHERE c.ChatID = @chatId").WithParameter("@chatId", chatId));

            List<Conversations> conversations = new List<Conversations>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                conversations.AddRange(response);
            }

            return conversations;
        }

        public async Task CreateConversationAsync(Conversations conversation)
        {
            await _context.ConversationContainer.CreateItemAsync(conversation, new PartitionKey(conversation.ChatID.ToString()));
        }
    }
}
