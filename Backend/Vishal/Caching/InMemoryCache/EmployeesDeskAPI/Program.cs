
using EmployeesDataBase.DatabaseContext;
using EmployeesDataBase.Interfaces;
using EmployeesDataBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeesDeskAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddMemoryCache();
            builder.Services.AddLazyCache();
            builder.Services.AddDbContext<EmployeeDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbString"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
