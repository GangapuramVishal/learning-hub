namespace CosmosDB
{
    public class CreateChatRequest
    {
        public Guid UserId { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }
    }
}
