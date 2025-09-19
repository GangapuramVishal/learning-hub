using ChatHistoryDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChatHistoryDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly OpenAISettings _openAISettings;
        public ChatController(AppDbContext context, IOptions<OpenAISettings> openAISettings)
        {
            _context = context;
            _openAISettings = openAISettings.Value;
        }

        [HttpPost("start-conversation")]
        public async Task<IActionResult> StartConversation()
        {
            var userId = GetUserIdFromToken();

            var newConversation = new Conversation
            {
                UserID = userId,
                Timestamp = DateTime.UtcNow,
                Messages = new List<ChatMessage>()
            };

            _context.Conversations.Add(newConversation);
            await _context.SaveChangesAsync();

            return Ok(new { ConversationID = newConversation.ConversationID });
        }

        // Existing endpoint with integrated title generation
        [HttpPost("{conversationId}/store-chat")]
        public async Task<IActionResult> StoreChat(int conversationId, string prompt, string response)
        {
            var userId = GetUserIdFromToken();

            var conversation = await _context.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.ConversationID == conversationId && c.UserID == userId);

            if (conversation == null)
            {
                return NotFound("Conversation not found.");
            }

            // If this is the first message in the conversation, generate the title
            if (conversation.Messages.Count == 0)
            {
                conversation.Title = GenerateTitle(prompt);

                // Update the conversation entity with the new title
                _context.Conversations.Update(conversation);
                await _context.SaveChangesAsync();
            }

            var chatMessage = new ChatMessage
            {
                ConversationID = conversationId,
                Prompt = prompt,
                Response = response,
                Timestamp = DateTime.UtcNow
            };

            conversation.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();

            return Ok("Chat stored.");
        }

        private string GenerateTitle(string prompt)
        {
            return SummarizePrompt(prompt);
        }

        private string SummarizePrompt(string prompt)
        {
            // Split the prompt into words
            var words = prompt.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Exclude common words and select key phrases
            var keyPhrases = words.Where(w => !IsCommonWord(w))
                                  .Take(5); // Take the first 5 meaningful words or phrases

            // Join the key phrases to form the title
            return string.Join(' ', keyPhrases);
        }

        private bool IsCommonWord(string word)
        {
            // Define common words to exclude from the title
            var commonWords = new HashSet<string>
    {
        "the", "is", "in", "and", "to", "with", "a", "I'm", "that", "so", "all", "for", "at", "on", "are"
    };

            // Check if the word is common
            return commonWords.Contains(word.ToLower());
        }

        [HttpGet("get-recent-chats")]
        public IActionResult GetRecentChats()
        {
            var userId = GetUserIdFromToken();
            var oneWeekAgo = DateTime.UtcNow.AddDays(-7);
            var conversations = _context.Conversations
                 .Where(c => c.UserID == userId && c.Timestamp >= oneWeekAgo)
                 .OrderByDescending(c => c.Timestamp)
                 .Select(c => new
                 {
                     c.ConversationID,
                     c.Title,
                     c.Timestamp,
                     Messages = c.Messages
                         .OrderByDescending(m => m.Timestamp)
                         .Select(m => new
                         {
                             m.ChatMessageID,
                             m.Prompt,
                             m.Response,
                             m.Timestamp
                         })
                         .ToList()
                 })
                 .ToList();

            return Ok(conversations);
        }

        private int GetUserIdFromToken()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                throw new UnauthorizedAccessException("User identity is not set.");
            }

            var userIdClaim = claimsIdentity.FindFirst("UserID");
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User ID claim is missing.");
            }

            return int.Parse(userIdClaim.Value);
        }
    }
}
