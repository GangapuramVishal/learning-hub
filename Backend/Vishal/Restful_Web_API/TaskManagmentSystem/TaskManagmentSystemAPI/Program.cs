using TaskManagmentSystemAPI;
using Microsoft.EntityFrameworkCore;
using TaskManagmentSystemAPI.Data;
using TaskManagmentSystemAPI.Repository;
using TaskManagmentSystemAPI.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Azure.Core;


var builder = WebApplication.CreateBuilder(args);    //logging registration

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    var SqlConnection = new SqlConnection(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
    {
        SqlConnection.AccessToken = AzServiceTokenProvider.GetAccessToken(builder.Configuration);
    }

    option.UseSqlServer(SqlConnection);
});
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers(option =>
{

}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();      // this is to support xml
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
