using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EekelsSignupLogin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var auth0Domain = builder.Configuration["Auth0:Domain"];
            var auth0Audience = builder.Configuration["Auth0:Audience"];
            
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{auth0Domain}/";
                    options.Audience = auth0Audience;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = $"https://{auth0Domain}/",
                        ValidAudience = auth0Audience,
                        ValidateLifetime = true
                    };
                });
            builder.Services.AddAuthorization();
            builder.Services
                .AddAuth0WebAppAuthentication(options =>
                {
                    options.Domain = auth0Domain;
                    options.ClientId = builder.Configuration["Auth0:ClientId"];
                    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
                });



            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();



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

            app.UseAuthentication();  // Add authentication middleware
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
