using Microsoft.Azure.Cosmos;

namespace CosmosDB
{
    public class CosmosDbContext
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;
        private Database _database;

        public CosmosDbContext(IConfiguration configuration)
        {
            _cosmosClient = new CosmosClient(configuration["CosmosDb:AccountEndpoint"], configuration["CosmosDb:AccountKey"]);
            _databaseName = configuration["CosmosDb:DatabaseName"];
        }

        public Database GetDatabase()
        {
            if (_database == null)
            {
                _database = _cosmosClient.GetDatabase(_databaseName);
            }
            return _database;
        }
    }

}
