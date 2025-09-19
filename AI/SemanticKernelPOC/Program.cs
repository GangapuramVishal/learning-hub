
using Microsoft.SemanticKernel;

namespace SemanticKernelPOC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // --- Semantic Kernel registration (Azure OpenAI) ---
            var aoai = builder.Configuration.GetSection("AzureOpenAI");
            var endpoint = aoai["Endpoint"]!;
            var apiKey = aoai["ApiKey"]!;
            var deployment = aoai["DeploymentName"]!;

            builder.Services.AddSingleton(sp =>
            {
                var kernelBuilder = Kernel.CreateBuilder();
                kernelBuilder.AddAzureOpenAIChatCompletion(
                    deploymentName: deployment,
                    endpoint: endpoint,
                    apiKey: apiKey,
                    serviceId: "primary" // optional but helps if you add more models later
                );

                // Register our native plugins
                kernelBuilder.Plugins.Add(KernelPluginFactory.CreateFromType<Plugins.WeatherPlugin>("Weather"));
                kernelBuilder.Plugins.Add(KernelPluginFactory.CreateFromType<Plugins.MathPlugin>("Math"));
                kernelBuilder.Plugins.Add(KernelPluginFactory.CreateFromType<Plugins.TimePlugin>("Time"));
                kernelBuilder.Plugins.Add(KernelPluginFactory.CreateFromType<Plugins.FunFactsPlugin>("FunFacts"));

                return kernelBuilder.Build();
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
            // Simple health
            app.MapGet("/", () => "AI Plugins POC is running.");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
