using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatHistoryDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConversationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}/conversations")]
        public IActionResult GetConversationsAndMessages(int userId)
        {
            var conversations = _context.Conversations
                .Include(c => c.Messages)
                .Where(c => c.UserID == userId)
                .OrderByDescending(c => c.Timestamp)
                .ToList();

            var response = conversations.Select(c => new
            {
                userConversationID = c.ConversationID,
                timestamp = c.Timestamp,
                title = c.Title,
                messages = c.Messages.Select(m => new
                {
                    userChatMessageID = m.ChatMessageID,
                    prompt = m.Prompt,
                    response = m.Response,
                    timestamp = m.Timestamp
                }).ToList()
            }).ToList();

            return Ok(response);
        }
    }
}
