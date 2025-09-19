namespace CosmosDbPoc.Domain
{
    public class Chats
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Title { get; set; }
        public ICollection<Conversations> Messages { get; set; } = new List<Conversations>();
    }
}