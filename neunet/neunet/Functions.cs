using System;
using System.Collections.Generic;
using System.Text;
using Neunet.Linear;
using System.Linq;

namespace Neunet
{
    public class Functions
    {
        private static Dictionary<string, Func<double, double>> KnownDoubleFunctions = new Dictionary<string, Func<double, double>>
        {
            [nameof(LogisticSigmoid).ToLower()] = LogisticSigmoid,
            [nameof(Tanh).ToLower()] = Tanh,
            [nameof(Linear).ToLower()] = Linear,
            [nameof(Ramp).ToLower()] = Ramp
        };
        private static Dictionary<string, Func<Vector, Vector>> KnownVectorFunctions = new Dictionary<string, Func<Vector, Vector>>
        {
            [nameof(AbsNorm).ToLower()] = AbsNorm,
            [nameof(Norm).ToLower()] = Norm
        };
        private static Dictionary<string, Func<Matrix, Matrix>> KnownMatrixFunctions = new Dictionary<string, Func<Matrix, Matrix>>
        {
            [nameof(AbsNorm).ToLower()] = AbsNorm,
            [nameof(Norm).ToLower()] = Norm
        };

        public static double LogisticSigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));
        public static double Tanh(double x) => x;
        public static double Linear(double x) => x;
        public static double Ramp(double x) => Math.Max(x, 0);

        public static T AbsNorm<T>(T value)
            where T : ILinearObject<T>, ILinearMultiplicative<double, T>
        {
            var summary = value.Select(Math.Abs).Sum();
            return value.Product(1 / summary);
        }
        public static T Norm<T>(T value)
            where T : ILinearObject<T>, ILinearMultiplicative<double, T>
        {
            var summary = value.Select(x => x * x).Sum();
            return value.Product(1 / Math.Sqrt(summary));
        }

        public static Func<Vector, Vector> ToVectorFunction(Func<double, double> function)
        {
            if (function == null) return null;
            return x => x.Map(function);
        }
        public static Func<Matrix, Matrix> ToMatrixFunction(Func<Vector, Vector> function)
        {
            if (function == null) return null;
            return x => x.Map(function);
        }

        public static Func<double, double> GetDoubleFunction(string name)
        {
            KnownDoubleFunctions.TryGetValue(name.ToLower(), out Func<double, double> result);
            return result;
        }
        public static Func<Vector, Vector> GetVectorFunction(string name)
        {
            KnownVectorFunctions.TryGetValue(name.ToLower(), out Func<Vector, Vector> result);
            if (result == null)
            {
                result = ToVectorFunction(GetDoubleFunction(name));
            }

            return result;
        }
        public static Func<Matrix, Matrix> GetMatrixFunction(string name)
        {
            KnownMatrixFunctions.TryGetValue(name.ToLower(), out Func<Matrix, Matrix> result);
            if (result == null)
            {
                result = ToMatrixFunction(GetVectorFunction(name));
            }

            return result;
        }
    }
}
