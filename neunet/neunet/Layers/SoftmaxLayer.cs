using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Neunet.Linear;

namespace Neunet
{
    public class SoftmaxLayer<T> : ILayer<T, T>
        where T : ILinearObject<T>
    {
        public T Process(T input)
        {
            var summary = input.Select(x => Math.Exp(x)).Sum();
            var res = input.Map(x => Math.Exp(x) / summary);

            return res;
        }
    }
}
