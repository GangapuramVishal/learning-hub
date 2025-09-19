using CosmosDbPoc.DbContext;
using CosmosDbPoc.Domain;
using CosmosDbPoc.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CosmosDbPoc
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly CosmosDbContext _context;

        public ConversationRepository(CosmosDbContext context)
        {
            _context = context;
        }

        public async Task CreateConversationAsync(Conversations conversation)
        {
            await _context.ConversationContainer.CreateItemAsync(conversation, new PartitionKey(conversation.ChatID.ToString()));
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
    }
}
