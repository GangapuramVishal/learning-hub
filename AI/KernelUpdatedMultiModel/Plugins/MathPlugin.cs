using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace KernelUpdatedMultiModel.Plugins
{
    public class MathPlugin
    {
        [KernelFunction, Description("Adds two numbers")]
        public double Add(double a, double b) => a + b;

        [KernelFunction, Description("Multiplies two numbers")]
        public double Multiply(double a, double b) => a * b;

        [KernelFunction, Description("Calculates average of numbers")]
        public double Average([Description("Numbers to average")] double[] values) =>
            values is { Length: > 0 } ? values.Average() : 0d;
    }
}
