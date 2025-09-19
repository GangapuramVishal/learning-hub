using Microsoft.Azure.Cosmos;

namespace CosmosDbPoc.DbContext
{
    public class CosmosDbContext
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Database _database;
        private readonly Container _userContainer;
        private readonly Container _chatContainer;
        private readonly Container _conversationContainer;

        public CosmosDbContext(string connectionString, string databaseName)
        {
            _cosmosClient = new CosmosClient(connectionString);
            _database = _cosmosClient.GetDatabase(databaseName);
            _userContainer = _database.GetContainer("User");
            _chatContainer = _database.GetContainer("Chats");
            _conversationContainer = _database.GetContainer("Conversations");
        }

        public Container UserContainer => _userContainer;
        public Container ChatContainer => _chatContainer;
        public Container ConversationContainer => _conversationContainer;
    }
}
