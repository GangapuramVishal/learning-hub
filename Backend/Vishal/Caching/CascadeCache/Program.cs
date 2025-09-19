using CascadeCache.Handlers;
using CascadeCache.Interfaces;
using CascadeCache.Services;
using Microsoft.EntityFrameworkCore;

namespace CascadeCache
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddHttpClient<IExternalApiService, ExternalApiService>();

            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<IExternalApiService, ExternalApiService>();

            builder.Services.AddTransient<CacheEmployeeHandler>();
            builder.Services.AddTransient<DatabaseEmployeeHandler>();
            builder.Services.AddTransient<ExternalApiEmployeeHandler>();

            builder.Services.AddScoped<IEmployeeService, GetEmployeeByIdService>(provider =>
            {
                var cacheHandler = provider.GetRequiredService<CacheEmployeeHandler>();
                var dbHandler = provider.GetRequiredService<DatabaseEmployeeHandler>();
                var apiHandler = provider.GetRequiredService<ExternalApiEmployeeHandler>();

                cacheHandler.SetNextHandler(dbHandler);
                dbHandler.SetNextHandler(apiHandler);

                return new GetEmployeeByIdService(cacheHandler);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
