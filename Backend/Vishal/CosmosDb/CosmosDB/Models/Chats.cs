namespace CosmosDB.Models
{
    public class Chats
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Title { get; set; }
        public ICollection<Conversations> Messages { get; set; } = new List<Conversations>();
    }
}
