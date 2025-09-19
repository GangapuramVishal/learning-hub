using CosmosDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDB.Interfaces
{
    public interface IChatService
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<Chats>> GetChatsByUserIdAsync(Guid userId);
        Task<Chats> CreateNewChatAsync(Guid userId, string prompt, string response);
        Task<Conversations> CreateConversationAsync(Conversations conversation);
        Task<Chats> GetChatAsync(Guid chatId);
        Task<Conversations> GetConversationAsync(Guid conversationId);
        Task<UserCreationResponse> CreateUserAsync(string email);
    }
}