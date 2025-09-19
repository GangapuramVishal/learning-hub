using CosmosDbPoc.Domain;
using CosmosDbPoc.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDbPoc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateChatAsync([FromBody] Chats chat)
        {
            await _chatService.CreateChatAsync(chat);
            return Ok();
        }

        [HttpGet("get/{chatId}")]
        public async Task<IActionResult> GetChatAsync(Guid chatId)
        {
            var chat = await _chatService.GetChatAsync(chatId);
            if (chat == null) return NotFound();
            return Ok(chat);
        }

        [HttpPut("update/{chatId}")]
        public async Task<IActionResult> UpdateChatAsync(Guid chatId, [FromBody] string newTitle)
        {
            await _chatService.UpdateChatAsync(chatId, newTitle);
            return Ok();
        }

        [HttpDelete("delete/{chatId}")]
        public async Task<IActionResult> DeleteChatAsync(Guid chatId)
        {
            await _chatService.DeleteChatAsync(chatId);
            return Ok();
        }

        [HttpGet("conversations/{chatId}")]
        public async Task<IActionResult> GetConversationsAsync(Guid chatId)
        {
            var conversations = await _chatService.GetConversationsForChatAsync(chatId);
            return Ok(conversations);
        }

        [HttpPost("conversation/create")]
        public async Task<IActionResult> CreateConversationAsync([FromBody] Conversations conversation)
        {
            await _chatService.CreateConversationAsync(conversation);
            return Ok();
        }
    }
}
