using CosmosDbPoc.Domain;

namespace CosmosDbPoc.Interfaces
{
    public interface IChatRepository
    {
        Task CreateChatAsync(Chats chat);
        Task<Chats> GetChatAsync(Guid chatId);
        Task UpdateChatAsync(Chats chat);
        Task DeleteChatAsync(Guid chatId);
        Task<List<Conversations>> GetConversationsForChatAsync(Guid chatId);
        Task CreateConversationAsync(Conversations conversation);
    }
}
