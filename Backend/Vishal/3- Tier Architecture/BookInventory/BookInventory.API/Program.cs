using BookInventorty.DAL.Data;
using BookInventorty.DAL.Repositorys;
using BookInventory.API.AzSQLConnection;
using BookInventory.BLL.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
namespace BookInventory.API
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<BookDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
            });
            //builder.Services.AddDbContext<BookDbContext>(option =>
            //{
            //    var SqlConnection = new SqlConnection(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
            //    {
            //        SqlConnection.AccessToken = AzServiceTokenProvider.GetAccessToken(builder.Configuration);
            //    }

            //    option.UseSqlServer(SqlConnection);
            //});

            builder.Services.AddControllers();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddAutoMapper(typeof(MappingConfig));
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
        }
    }
}
