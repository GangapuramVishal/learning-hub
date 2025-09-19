using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatHistoryDemo.Models
{
    public class Conversation
    {
        public int ConversationID { get; set; } // Unique per user
        public int UserID { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Title { get; set; }
        public ICollection<ChatMessage> Messages { get; set; }
    }
}
