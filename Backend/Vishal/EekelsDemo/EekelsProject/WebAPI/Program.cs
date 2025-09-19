//using Application.Services;
using Application.Interfaces;
using Application.Service;
using Application.Validators;
using Auth0.AspNetCore.Authentication;
using Domain.SignupLoginEntities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var auth0Domain = builder.Configuration["Auth0:Domain"];
            var auth0Audience = builder.Configuration["Auth0:Audience"];

            // Authentication and Authorization
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

            // Application Insights
            builder.Services.AddApplicationInsightsTelemetry();

            // Key Vault Configuration 
            /*
            var keyVaultURL = builder.Configuration.GetSection("ConnectionStrings:DbKeyValutUrl");
            var uri = new Uri(keyVaultURL.Value!.ToString());
            var credential = new DefaultAzureCredential();
            var secretClient = new SecretClient(uri, credential);
            var connectionStringSecret = secretClient.GetSecret("Kv-SqlString");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionStringSecret.Value.Value.ToString());
            });
            */

            // Registering Services
            builder.Services.AddControllers()
                            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserSignupRequestValidator>());

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<IUserService, UserService>();

            // Swagger Configuration
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
