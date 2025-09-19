using CosmosDbPoc.Domain;
using CosmosDbPoc.Interfaces;

namespace CosmosDbPoc
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IConversationRepository _conversationRepository;

        public ChatService(IChatRepository chatRepository, IConversationRepository conversationRepository)
        {
            _chatRepository = chatRepository;
            _conversationRepository = conversationRepository;
        }

        public async Task CreateChatAsync(Chats chat)
        {
            await _chatRepository.CreateChatAsync(chat);
        }

        public async Task<Chats> GetChatAsync(Guid chatId)
        {
            return await _chatRepository.GetChatAsync(chatId);
        }

        public async Task UpdateChatAsync(Guid chatId, string newTitle)
        {
            var chat = await _chatRepository.GetChatAsync(chatId);
            if (chat != null)
            {
                chat.Title = newTitle;
                await _chatRepository.UpdateChatAsync(chat);
            }
        }

        public async Task DeleteChatAsync(Guid chatId)
        {
            await _chatRepository.DeleteChatAsync(chatId);
        }

        public async Task<List<Conversations>> GetConversationsForChatAsync(Guid chatId)
        {
            return await _conversationRepository.GetConversationsForChatAsync(chatId);
        }

        public async Task CreateConversationAsync(Conversations conversation)
        {
            await _conversationRepository.CreateConversationAsync(conversation);
        }
    }
}
