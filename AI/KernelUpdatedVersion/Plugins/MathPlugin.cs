using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace KernelUpdatedVersion.Plugins;

public class MathPlugin
{
    [KernelFunction, Description("Adds two numbers")]
    public double Add([Description("First number")] double a,
                      [Description("Second number")] double b) => a + b;

    [KernelFunction, Description("Multiplies two numbers")]
    public double Multiply([Description("First number")] double a,
                           [Description("Second number")] double b) => a * b;

    [KernelFunction, Description("Calculates the average of an array of numbers")]
    public double Average([Description("Numbers to average")] double[] values)
        => values is { Length: > 0 } ? values.Average() : 0.0;
}
