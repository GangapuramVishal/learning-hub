using CosmosDB.Interfaces;
using CosmosDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("user")]
        public async Task<ActionResult<UserCreationResponse>> CreateUser([FromBody] UserCreationRequest request)
        {
            var response = await _chatService.CreateUserAsync(request.Email);

            if (response == null)
            {
                return BadRequest("Error creating user.");
            }

            return Ok(response);
        }

        [HttpGet("user/email/{email}")]
        public async Task<ActionResult<object>> GetUserByEmail(string email)
        {
            var user = await _chatService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(new { userId = user.id });
        }


        [HttpGet("chats/{userId}")]
        public async Task<ActionResult<IEnumerable<Chats>>> GetChatsByUserId(Guid userId)
        {
            var chats = await _chatService.GetChatsByUserIdAsync(userId);
            return Ok(chats);
        }

        [HttpPost("chat")]
        public async Task<ActionResult<Chats>> CreateNewChat([FromBody] CreateChatRequest request)
        {
            if (request == null || Guid.Empty == request.UserId || string.IsNullOrWhiteSpace(request.Prompt) || string.IsNullOrWhiteSpace(request.Response))
            {
                return BadRequest("Invalid request body.");
            }

            // Call service to create a new chat
            var newChat = await _chatService.CreateNewChatAsync(request.UserId, request.Prompt, request.Response);

            // Return created chat
            return CreatedAtAction(nameof(GetChatById), new { chatId = newChat.id }, newChat);
        }


        [HttpGet("chat/{chatId}")]
        public async Task<ActionResult<Chats>> GetChatById(Guid chatId)
        {
            var chat = await _chatService.GetChatAsync(chatId);
            if (chat == null)
            {
                return NotFound();
            }
            return Ok(chat);
        }

        [HttpGet("conversation/{conversationId}")]
        public async Task<ActionResult<Conversations>> GetConversationById(Guid conversationId)
        {
            var conversation = await _chatService.GetConversationAsync(conversationId);
            if (conversation == null)
            {
                return NotFound();
            }
            return Ok(conversation);
        }
    }

}
