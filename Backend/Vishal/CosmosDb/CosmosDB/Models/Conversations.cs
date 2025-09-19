namespace CosmosDB.Models
{
    public class Conversations
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ChatID { get; set; }
        public string Prompt { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
