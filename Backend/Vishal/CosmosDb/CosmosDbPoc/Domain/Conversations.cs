namespace CosmosDbPoc.Domain
{
    public class Conversations
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ChatID { get; set; }
        public Chats Chats { get; set; } = new Chats();
        public string Prompt { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
