using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatHistoryDemo.Models
{
    public class ChatMessage
    {
        public int ChatMessageID { get; set; } // Unique per conversation
        public int ConversationID { get; set; }
        public Conversation Conversation { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
