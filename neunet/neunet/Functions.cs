using System;
using System.Collections.Generic;
using System.Text;

namespace Neunet
{
    public static class Functions
    {
        private static Dictionary<string, Func<double, double>> KnownFunctions = new Dictionary<string, Func<double, double>>
        {
            [nameof(LogisticSigmoid).ToLower()] = LogisticSigmoid,
            [nameof(Tanh).ToLower()] = Tanh,
            [nameof(Linear).ToLower()] = Linear,
            [nameof(Ramp).ToLower()] = Ramp
        };

        public static double LogisticSigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));
        public static double Tanh(double x) => x;
        public static double Linear(double x) => x;
        public static double Ramp(double x) => Math.Max(x, 0);

        public static Func<double, double> GetFunction(string name)
        {
            KnownFunctions.TryGetValue(name.ToLower(), out Func<double, double> result);
            return result;
        }
    }
}
