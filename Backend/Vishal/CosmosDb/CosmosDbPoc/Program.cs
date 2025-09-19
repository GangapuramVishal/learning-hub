using CosmosDbPoc.DbContext;
using CosmosDbPoc.Interfaces;
using CosmosDbPoc;

var builder = WebApplication.CreateBuilder(args);

// Fetch Cosmos DB settings from appsettings.json
var cosmosDbSettings = builder.Configuration.GetSection("CosmosDb");
var connectionString = cosmosDbSettings["ConnectionString"];
var databaseName = cosmosDbSettings["DatabaseName"];

// Register CosmosDbContext and Repositories
builder.Services.AddSingleton<CosmosDbContext>(provider => new CosmosDbContext(connectionString, databaseName));
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Register UserRepository
builder.Services.AddScoped<IUserService, UserService>(); // Register UserService
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger and API pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
