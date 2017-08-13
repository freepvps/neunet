using System;
using System.Collections.Generic;
using System.Text;
using Neunet.Linear;

namespace Neunet
{
    public class ElementwizeLayer<T, TFunc> : ILayer<T, T> where T : IMap<T, TFunc>
    {
        public Func<TFunc, TFunc> Function { get; }

        public ElementwizeLayer(Func<TFunc, TFunc> function)
        {
            Function = function;
        }

        public T Process(T input)
        {
            return input.Map(Function);
        }
    }
    public class ElementwizeLayer<T> : ElementwizeLayer<T, T>
        where T : IMap<T, T>
    {
        public ElementwizeLayer(Func<T, T> function) : base(function)
        {
        }
    }
}
