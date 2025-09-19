
using KernelUpdatedVersion.Plugins;
using Microsoft.SemanticKernel;

namespace KernelUpdatedVersion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Config
            var aoai = builder.Configuration.GetSection("AzureOpenAI");
            var endpoint = aoai["Endpoint"]!;
            var apiKey = aoai["ApiKey"]!;
            var deployment = aoai["DeploymentName"]!;

            // (A) Register a READY-TO-USE kernel with ALL plugins (for Auto mode)
            builder.Services.AddSingleton(sp =>
            {
                var kb = Kernel.CreateBuilder();
                kb.AddAzureOpenAIChatCompletion(deployment, endpoint, apiKey, serviceId: "primary");

                kb.Plugins.Add(KernelPluginFactory.CreateFromType<WeatherPlugin>("Weather"));
                kb.Plugins.Add(KernelPluginFactory.CreateFromType<MathPlugin>("Math"));
                kb.Plugins.Add(KernelPluginFactory.CreateFromType<TimePlugin>("Time"));
                kb.Plugins.Add(KernelPluginFactory.CreateFromType<FunFactsPlugin>("FunFacts"));

                return kb.Build();
            });

            // (B) Also register a lightweight factory so we can build
            // per-request kernels with a *subset* of plugins (whitelisting)
            builder.Services.AddSingleton<Func<IEnumerable<string>, Kernel>>(sp => allowedPlugins =>
            {
                var kb = Kernel.CreateBuilder();
                kb.AddAzureOpenAIChatCompletion(deployment, endpoint, apiKey, serviceId: "primary");

                // register only what’s requested
                foreach (var name in allowedPlugins.Select(n => n.Trim()).Where(n => !string.IsNullOrEmpty(n)))
                {
                    switch (name.ToLowerInvariant())
                    {
                        case "weather": kb.Plugins.Add(KernelPluginFactory.CreateFromType<WeatherPlugin>("Weather")); break;
                        case "math": kb.Plugins.Add(KernelPluginFactory.CreateFromType<MathPlugin>("Math")); break;
                        case "time": kb.Plugins.Add(KernelPluginFactory.CreateFromType<TimePlugin>("Time")); break;
                        case "funfacts": kb.Plugins.Add(KernelPluginFactory.CreateFromType<FunFactsPlugin>("FunFacts")); break;
                    }
                }

                return kb.Build();
            });

            // (C) And a factory for a text-only kernel (no plugins)
            builder.Services.AddSingleton<Func<Kernel>>(() =>
            {
                var kb = Kernel.CreateBuilder();
                kb.AddAzureOpenAIChatCompletion(deployment, endpoint, apiKey, serviceId: "primary");
                return kb.Build();
            });

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
