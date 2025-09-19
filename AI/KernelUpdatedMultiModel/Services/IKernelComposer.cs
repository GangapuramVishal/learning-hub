using KernelUpdatedMultiModel.Plugins;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;

namespace KernelUpdatedMultiModel.Services
{
    public interface IKernelComposer
    {
        Kernel Build(string provider, IEnumerable<string> allowedPlugins); 
        Kernel BuildTextOnly(string provider);                              
    }

    public sealed class KernelComposer(IConfiguration config) : IKernelComposer
    {
        public Kernel Build(string provider, IEnumerable<string> allowedPlugins)
        {
            var kb = Kernel.CreateBuilder();
            RegisterProviders(kb, config);

            foreach (var name in Normalize(allowedPlugins))
            {
                switch (name)
                {
                    case "weather": kb.Plugins.Add(KernelPluginFactory.CreateFromType<WeatherPlugin>("Weather")); break;
                    case "math": kb.Plugins.Add(KernelPluginFactory.CreateFromType<MathPlugin>("Math")); break;
                    case "time": kb.Plugins.Add(KernelPluginFactory.CreateFromType<TimePlugin>("Time")); break;
                    case "funfacts": kb.Plugins.Add(KernelPluginFactory.CreateFromType<FunFactsPlugin>("FunFacts")); break;
                }
            }

            var kernel = kb.Build();
            _ = kernel.GetRequiredService<IChatCompletionService>(provider); 
            return kernel;
        }

        public Kernel BuildTextOnly(string provider)
        {
            var kb = Kernel.CreateBuilder();
            RegisterProviders(kb, config);

            var kernel = kb.Build();
            _ = kernel.GetRequiredService<IChatCompletionService>(provider);
            return kernel;
        }

        private static IEnumerable<string> Normalize(IEnumerable<string> names) =>
            (names ?? Array.Empty<string>())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim().ToLowerInvariant())
                .Distinct();

        private static void RegisterProviders(IKernelBuilder kb, IConfiguration config)
        {
            // --- Azure OpenAI (serviceId: "aoai") ---
            var aoai = config.GetSection("AzureOpenAI");
            var endpoint = aoai["Endpoint"];
            var apiKey = aoai["ApiKey"];
            var deployment = aoai["DeploymentName"];
            if (!string.IsNullOrWhiteSpace(endpoint) &&
                !string.IsNullOrWhiteSpace(apiKey) &&
                !string.IsNullOrWhiteSpace(deployment))
            {
                kb.AddAzureOpenAIChatCompletion(
                    deploymentName: deployment!,
                    endpoint: endpoint!,
                    apiKey: apiKey!,
                    serviceId: "aoai"
                );
            }

            // --- Google Gemini (serviceId: "gemini") ---
            var g = config.GetSection("Gemini");
            var gKey = g["ApiKey"];
            var gModel = g["ModelId"];
#pragma warning disable SKEXP0070
            if (!string.IsNullOrWhiteSpace(gKey) && !string.IsNullOrWhiteSpace(gModel))
            {
                kb.AddGoogleAIGeminiChatCompletion(
                    modelId: gModel!,
                    apiKey: gKey!,
                    serviceId: "gemini"
                );
            }
#pragma warning restore SKEXP0070
        }
    }
}
