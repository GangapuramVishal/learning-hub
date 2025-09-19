using CosmosDbPoc.Domain;

namespace CosmosDbPoc.Interfaces
{
    public interface IConversationRepository
    {
        Task CreateConversationAsync(Conversations conversation);
        Task<List<Conversations>> GetConversationsForChatAsync(Guid chatId);
    }
}
