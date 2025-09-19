using CosmosDB.Interfaces;
using CosmosDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace CosmosDB
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<UserCreationResponse> CreateUserAsync(string email)
        {
            // Check if the user already exists by email
            var existingUser = await _chatRepository.GetUserByEmailAsync(email);

            if (existingUser != null)
            {
                // User exists, return the user ID and a message
                return new UserCreationResponse
                {
                    UserId = existingUser.id,
                    Message = "User already exists"
                };
            }

            // If user doesn't exist, create a new user
            var newUser = new Models.User
            {
                id = Guid.NewGuid(),
                Email = email
            };

            var createdUser = await _chatRepository.CreateUserAsync(newUser);

            return new UserCreationResponse
            {
                UserId = createdUser.id,
                Message = "User created successfully"
            };
        }

        public async Task<Models.User> GetUserByEmailAsync(string email)
        {
            return await _chatRepository.GetUserByEmailAsync(email);
        }

        public async Task<IEnumerable<Chats>> GetChatsByUserIdAsync(Guid userId)
        {
            return await _chatRepository.GetChatsByUserIdAsync(userId);
        }

        public async Task<Chats> CreateNewChatAsync(Guid userId, string prompt, string response)
        {
            var newChat = new Chats
            {
                UserID = userId,
                Timestamp = DateTime.UtcNow,
                Messages = new List<Conversations>
                {
                    new Conversations
                    {
                        Prompt = prompt,
                        Response = response,
                        Timestamp = DateTime.UtcNow
                    }
                }
            };

            newChat.Title = "Hii new title";
            //newChat.Title = await GenerateTitleWithOpenAI(prompt);
            //_logger.LogInformation("Generated title for the chat: {Title}", newChat.Title);
            var createdChat = await _chatRepository.CreateChatAsync(newChat);
            foreach (var conversation in createdChat.Messages)
            {
                conversation.ChatID = createdChat.id;  // Set the ChatID to the new Chat's ID
            }

            //_logger.LogInformation("New chat saved successfully for user ID: {UserId}", userId);
            await _chatRepository.UpdateChatAsync(createdChat);
            return createdChat;
        }


        public async Task<Conversations> CreateConversationAsync(Conversations conversation)
        {
            return await _chatRepository.CreateConversationAsync(conversation);
        }
        public async Task<Chats> GetChatAsync(Guid chatId)
        {
            return await _chatRepository.GetChatAsync(chatId);
        }

        public async Task<Conversations> GetConversationAsync(Guid conversationId)
        {
            return await _chatRepository.GetConversationAsync(conversationId);
        }

    }
}
