using CosmosDbPoc.Domain;

namespace CosmosDbPoc.Interfaces
{
    public interface IChatService
    {
        Task CreateChatAsync(Chats chat);
        Task<Chats> GetChatAsync(Guid chatId);
        Task UpdateChatAsync(Guid chatId, string newTitle);
        Task DeleteChatAsync(Guid chatId);
        Task<List<Conversations>> GetConversationsForChatAsync(Guid chatId);
        Task CreateConversationAsync(Conversations conversation);
    }
}
