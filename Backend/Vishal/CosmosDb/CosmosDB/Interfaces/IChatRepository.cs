using CosmosDB.Models;

namespace CosmosDB.Interfaces
{
    public interface IChatRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<Chats>> GetChatsByUserIdAsync(Guid userId);
        Task<Chats> CreateChatAsync(Chats chat);
        Task<Conversations> CreateConversationAsync(Conversations conversation);
        Task<Chats> GetChatAsync(Guid chatId);
        Task<Conversations> GetConversationAsync(Guid conversationId);
        Task<User> CreateUserAsync (User user);
        Task UpdateChatAsync(Chats chat);
    }
}
