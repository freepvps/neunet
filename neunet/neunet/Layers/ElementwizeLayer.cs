using System;
using System.Collections.Generic;
using System.Text;
using Neunet.Linear;

namespace Neunet
{
    public class ElementwizeLayer<T> : ILayer<T, T> where T : ILinearObject<T>
    {
        public Func<double, double> Function { get; }

        public ElementwizeLayer(Func<double, double> function)
        {
            Function = function;
        }

        public T Process(T input)
        {
            return input.Map(Function);
        }
    }
    public class ElementwizeLayer : ElementwizeLayer<Vector>
    {
        public ElementwizeLayer(Func<double, double> function) : base(function)
        {
        }
    }
}
