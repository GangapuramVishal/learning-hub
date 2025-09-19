
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.KeyVault;
using Microsoft.EntityFrameworkCore;

namespace GlobalErrorHandlingForAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container.
           //builder.Services.AddDbContext<ApplicationDbContext>(option =>
           //{
           //    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
           //});
    //------------------------------- KEY VAULT ------------------------------------------------------------------------

            var keyVaultURL = builder.Configuration.GetSection("ConnectionStrings:DbKeyValutUrl");
            //var Uri = new Uri("https://pingpong-sqlstring.vault.azure.net/");
            var Uri = new Uri(keyVaultURL.Value!.ToString());
            var crd = new DefaultAzureCredential();
            var secretClient = new SecretClient(Uri, crd);

            
            var connectionStringSecret = secretClient.GetSecret("Kv-SqlString");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionStringSecret.Value.Value.ToString());
            });

    //------------------------------- KEY VAULT ------------------------------------------------------------------------

            builder.Services.AddControllers();
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
