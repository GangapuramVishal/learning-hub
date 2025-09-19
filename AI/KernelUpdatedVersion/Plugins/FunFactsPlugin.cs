using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace KernelUpdatedVersion.Plugins;

public class FunFactsPlugin
{
    private static readonly string[] TechFacts =
    {
        "The first 1GB hard drive (1980) weighed ~250 kg.",
        "The original iPod had a spinning hard drive.",
        "COBOL still runs core banking systems worldwide."
    };

    private static readonly Dictionary<string, string> AnimalFacts = new(StringComparer.OrdinalIgnoreCase)
    {
        ["cat"] = "Cats sleep 12–16 hours per day.",
        ["dog"] = "Dogs have ~300 million scent receptors.",
        ["elephant"] = "Elephants can recognize themselves in mirrors."
    };

    [KernelFunction, Description("Gets a random short tech fact")]
    public string GetTechFact()
        => TechFacts[Random.Shared.Next(TechFacts.Length)];

    [KernelFunction, Description("Gets a fun fact about an animal")]
    public string GetAnimalFact([Description("Animal name")] string animal)
        => AnimalFacts.TryGetValue(animal, out var fact)
            ? fact
            : $"No fact for '{animal}' in this POC.";
}
